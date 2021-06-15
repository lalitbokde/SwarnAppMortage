using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Text;
using Android.Support.V7.App;
using System.IO;
using SuwarnAppMortgage.Adapter;
using SQLite;

namespace SuwarnAppMortgage.Activities
{
    [Activity(Label = "Release The Mortgage")]
    public class GirviSodvaActivity : AppCompatActivity
    {
        SettingMobileApp _ObjSettingMobileAppModel = new SettingMobileApp();
        GirviMaster GirviNO = new GirviMaster();
        KhatawaniTapshilNaveJama item = new KhatawaniTapshilNaveJama();
        String Message, MessageFull, Calculation, EkunDiwas, Tday, Interset, TotalAmount;
        AutoCompleteTextView Search;
        ListView mListView;
        List<GirviMaster> Result = new List<GirviMaster>();
        List<KhatawaniTapshilNaveJama> ResultCustomerName;
        string dbPath = "/storage/emulated/0/JewelleryDB.db";
        string path = Application.Context.FilesDir.Path;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.GirviSodvalayout);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetTitle(Resource.String.app_name);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);


            Search = FindViewById<AutoCompleteTextView>(Resource.Id.autoGSSearch);
            mListView = FindViewById<ListView>(Resource.Id.listViewKhatawaniList);
            CustomerNameSearch();
            Search.ItemClick += Search_ItemClick;
            mListView.ItemClick += MListView_ItemClick;

            var pathToDatabase = Path.Combine(path, "JewelleryMortgageLocalDB.db");
            var db = new SQLiteConnection(pathToDatabase);
            SettingMobileApp data = db.Query<SettingMobileApp>("Select * from SettingMobileApp where SrNo = '1'").FirstOrDefault();
        }


        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
          

            List<KhatawaniTapshilNaveJama> ResultNew;
            try
            {
                var db = new SQLiteConnection(dbPath);
                GirviNO = Result.ElementAt(e.Position);

                string strGirviNO = GirviNO.receipt_no.ToString();
                var data = db.Query<KhatawaniTapshilNaveJama>("SELECT CM.khatawani_No, GM.GirviRecordNo, GIM.metal_type, GIM.item_type, GIM.Total_Quantity, GIM.gross_wt, GIM.net_wt, GIM.fine_wt, GM.Amount, GM.Date_of_deposit, CM.FullName, CM.Contact_No, CM.Address,GM.Status FROM customer_master AS CM INNER JOIN GirviMaster AS GM ON CM.khatawani_No = GM.khatawani_No INNER JOIN GirviItemMaster AS GIM ON GM.GirviRecordNo = GIM.GirviNo where GM.receipt_no = '" + strGirviNO.Trim() +"'").ToList();

                ResultNew = data;
                item = ResultNew.ElementAt(0);

                try
                {
                    Tday = DateTime.Now.ToString("dd/MM/yyyy");
                    if (GirviNO.Date_of_deposit.ToString() != "" && Tday != "")
                    {
                        if (GirviNO.Status.ToString().Trim() == "Release")
                        {
                            EkunDiwas = (((Convert.ToDateTime(GirviNO.withdraw_release_date.ToString()) - Convert.ToDateTime(GirviNO.Date_of_deposit.ToString())).TotalDays) + _ObjSettingMobileAppModel.CalculationDaysDifference).ToString();
                        }
                        else
                        {
                            EkunDiwas = (((Convert.ToDateTime(Tday) - Convert.ToDateTime(GirviNO.Date_of_deposit.ToString())).TotalDays) + _ObjSettingMobileAppModel.CalculationDaysDifference).ToString();
                        }
                    }
                }
                catch { }

                Interset = InterestAmount(GirviNO.Amount.ToString(), GirviNO.interset_rate.ToString(), EkunDiwas).ToString();
                TotalAmount = (Convert.ToDouble(Interset) + Convert.ToDouble(GirviNO.Amount.ToString())).ToString();
                

                Message = (item.metal_type + " " + item.item_type + " (" + item.Total_Quantity + "nos.) " + "\n" +
               "Gross weight : " + item.gross_wt + " gm " + "\n" +
               "Net weight : " + item.net_wt + " gm " + "\n" +
               "Fine weight : " + item.fine_wt + " gm ") == null ? "" : (item.metal_type + " " + item.item_type + " (" + item.Total_Quantity + "nos.) " + "\n" +
               "Gross weight : " + item.gross_wt + " gm " + "\n" +
               "Net weight : " + item.net_wt + " gm " + "\n" +
               "Fine weight : " + item.fine_wt + " gm ").ToString();

                Calculation = ("Mortgage amount : " + GirviNO.Amount.ToString() + "\n"+
                    "Percentage of interest : " + GirviNO.interset_rate.ToString()+ "\n"+
                    "Total days : " + EkunDiwas + "\n"+
                    "Interest amount : " + Interset +"\n"+
                    "The total amount to be paid : " + TotalAmount).ToString();

                MessageFull = Message + "\n"+
                              "\n" +
                              "\n" +
                              "\n" +
                              Calculation;
            }
            catch
            {

            }
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
            if (GirviNO.Status.ToString().Trim() == "Release")
            {
                alert.SetTitle(Html.FromHtml("<font color='#EC407A'>Mortgage release details</font>"));
            }
            else
            {
                alert.SetTitle(Html.FromHtml("<font color='#EC407A'>Mortgage details</font>"));
            }
            // alert.SetTitle("Confirm Start Audit");
            alert.SetMessage(MessageFull);
            alert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                return;
            });

            Dialog dialog = alert.Create();
            dialog.Show();
        }


        public object InterestAmount(string Amount, string InterestRate, string noofdays)
        {
            decimal intAmount = 0;
            try
            {
                int amt = Convert.ToInt32(Amount);
                decimal rate = Convert.ToDecimal(Convert.ToDecimal(InterestRate));
                decimal dys = Convert.ToDecimal(Convert.ToInt32(noofdays));
                intAmount = (amt * (rate * 12) * dys) / (100 * 365);
                //}
            }
            catch { }

            return Convert.ToString(roundup(Convert.ToString(intAmount)));

        }


        public string roundup(string value)
        {
            if (value != "")
            {

                double number = Convert.ToDouble(value);
                number = Math.Round(number);

                return Convert.ToString(number);
            }
            return value;

        }

        private void Search_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try
            {
                var db = new SQLiteConnection(dbPath);

                var data1 = db.Query<customer_master>("Select khatawani_No from customer_master where FullName = '" + Search.Text.Trim().ToString() + "'").FirstOrDefault();

                var data = db.Query<GirviMaster>("Select * from GirviMaster where khatawani_No = '" + data1.khatawani_No.ToString() + "'").ToList();

                Result = data;
                mListView.Adapter = new GirviSodvaAdapter(this, Result);
            }
            catch { }
        }

        public void CustomerNameSearch()
        {

            try
            {
                var db = new SQLiteConnection(dbPath);

                var data = db.Query<KhatawaniTapshilNaveJama>("SELECT FullName FROM customer_master").ToList();

                ResultCustomerName = data;

                var CustomerNameString = new string[ResultCustomerName.Count];
                int i = 0;
                foreach (var item in ResultCustomerName)
                {
                    CustomerNameString[i] = item.FullName;
                    i++;
                }

                ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, CustomerNameString);
                Search.Adapter = adapter;
            }
            catch (Exception e)
            {
                String E = e.ToString();
            }
        }
        public void LoadData()
        {
            try
            {
                //var db = new SQLiteConnection(dbPath);

                //var data = db.Query<GirviMaster>("Select * from GirviMaster").ToList();

                //Result = data;
                //mListView.Adapter = new GirviSodvaAdapter(this, Result);


                var pathToDatabase = Path.Combine(path, "JewelleryMortgageLocalDB.db");
                var dbSettingMobileApp = new SQLiteConnection(pathToDatabase);
                SettingMobileApp DaysAddOrRemove = dbSettingMobileApp.Query<SettingMobileApp>("Select * from SettingMobileApp where SrNo = '1'").FirstOrDefault();
                if (DaysAddOrRemove != null)
                {
                    _ObjSettingMobileAppModel.CalculationDaysDifference = DaysAddOrRemove.CalculationDaysDifference;
                }
                else
                {
                    _ObjSettingMobileAppModel.CalculationDaysDifference = 0;
                }
            }
            catch (Exception e)
            {
                String E = e.ToString();
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnResume()
        {
            SupportActionBar.SetTitle(Resource.String.girvi_sodva);
            LoadData();
            base.OnResume();
        }
    }
}