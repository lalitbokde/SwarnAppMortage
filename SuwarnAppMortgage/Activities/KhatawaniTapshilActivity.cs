using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using SQLite;
using SuwarnAppMortgage.Adapter;

namespace SuwarnAppMortgage.Activities
{
    [Activity(Label = "खतावणी तपशील")]
    public class KhatawaniTapshilActivity : AppCompatActivity
    {

        Button btnJama, btnNave, btnTotal;
        ListView mListView;
        AutoCompleteTextView Search;
        Double total = 0;
        String STotal;
        List<KhatawaniTapshilNaveJama> Result = new List<KhatawaniTapshilNaveJama>();
        List<KhatawaniTapshilNaveJama> ResultCustomerName;

        string dbPath = "/storage/emulated/0/JewelleryDB.db";


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.KhatawaniTapshillayout);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetTitle(Resource.String.app_name);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            btnJama = FindViewById<Button>(Resource.Id.btnKPJma);
            btnTotal = FindViewById<Button>(Resource.Id.btnTotalAmount);
            btnNave = FindViewById<Button>(Resource.Id.btnKPNave);
            Search = FindViewById<AutoCompleteTextView>(Resource.Id.autoKTSearch);
            mListView = FindViewById<ListView>(Resource.Id.listViewKhatawaniList);

            btnJama.Click += BtnJama_Click;
            btnNave.Click += BtnNave_Click;
            Search.AfterTextChanged += Search_AfterTextChanged;
            //LoadData();
            CustomerNameSearch();
        }

        private void Search_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            List<KhatawaniTapshilNaveJama> Result;
            try
            {
                total = 0;
                var db = new SQLiteConnection(dbPath);

                var data = db.Query<KhatawaniTapshilNaveJama>("SELECT CM.khatawani_No, GM.GirviRecordNo, GIM.metal_type, GIM.item_type, GIM.Total_Quantity, GIM.gross_wt, GIM.net_wt, GIM.fine_wt, GM.Amount, GM.Date_of_deposit, CM.FullName, CM.Contact_No, CM.Address,GM.Status FROM customer_master AS CM INNER JOIN GirviMaster AS GM ON CM.khatawani_No = GM.khatawani_No INNER JOIN GirviItemMaster AS GIM ON GM.GirviRecordNo = GIM.GirviNo where GM.Status = 'unchange' and CM.FullName = '"+Search.Text.Trim().ToString()+"'").ToList();

                Result = data;
                mListView.Adapter = new KhatawaniTapshilAdapter(this, Result);
                
                for (int i = 0; i < Result.Count; i++)
                {
                    STotal = Result[i].Amount.ToString();
                    total = total + Convert.ToDouble(STotal);
                }

            }
            catch 
            {
                
            }

            btnTotal.Text = "0.0";
            btnTotal.Text = Convert.ToString(total);
        }
        public void CustomerNameSearch()
        {
            
            try
            {
                total = 0;
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

                ArrayAdapter adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, CustomerNameString);
                Search.Adapter = adapter;
            }
            catch (Exception e)
            {
                String E = e.ToString();
            }
        }



        private void BtnNave_Click(object sender, EventArgs e)
        {
            List<KhatawaniTapshilNaveJama> Result;
            try
            {
                total = 0;
                var db = new SQLiteConnection(dbPath);

                var data = db.Query<KhatawaniTapshilNaveJama>("SELECT CM.khatawani_No, GM.GirviRecordNo, GIM.metal_type, GIM.item_type, GIM.Total_Quantity, GIM.gross_wt, GIM.net_wt, GIM.fine_wt, GM.Amount, GM.Date_of_deposit, CM.FullName, CM.Contact_No, CM.Address,GM.Status FROM customer_master AS CM INNER JOIN GirviMaster AS GM ON CM.khatawani_No = GM.khatawani_No INNER JOIN GirviItemMaster AS GIM ON GM.GirviRecordNo = GIM.GirviNo where GM.Status = 'unchange' and CM.FullName = '" + Search.Text.Trim().ToString() + "'").ToList();

                Result = data;
                mListView.Adapter = new KhatawaniTapshilAdapter(this, Result);
                //SELECT khatawani_No, FullName, Contact_No, Address, occupation, cast, Address2 FROM customer_master Order by case IsNumeric(khatawani_No) when 1 then replicate('0', 100 - Len(khatawani_No))+khatawani_No else khatawani_No end


                for (int i = 0; i < Result.Count; i++)
                {
                    STotal = Result[i].Amount.ToString();
                    total = total + Convert.ToDouble(STotal);
                }

            }
            catch
            {

            }

            btnTotal.Text = "0.0";
            btnTotal.Text = Convert.ToString(total);
        }

        private void BtnJama_Click(object sender, EventArgs e)
        {
            List<KhatawaniTapshilNaveJama> Result;
            try
            {
                total = 0;
                var db = new SQLiteConnection(dbPath);

                var data = db.Query<KhatawaniTapshilNaveJama>("SELECT CM.khatawani_No, GM.GirviRecordNo, GIM.metal_type, GIM.item_type, GIM.Total_Quantity, GIM.gross_wt, GIM.net_wt, GIM.fine_wt, GM.Amount, GM.Date_of_deposit, CM.FullName, CM.Contact_No, CM.Address,GM.Status FROM customer_master AS CM INNER JOIN GirviMaster AS GM ON CM.khatawani_No = GM.khatawani_No INNER JOIN GirviItemMaster AS GIM ON GM.GirviRecordNo = GIM.GirviNo where GM.Status = 'Release' and CM.FullName = '" + Search.Text.Trim().ToString() + "'").ToList();

                Result = data;
                mListView.Adapter = new KhatawaniTapshilAdapter(this, Result);
                //SELECT khatawani_No, FullName, Contact_No, Address, occupation, cast, Address2 FROM customer_master Order by case IsNumeric(khatawani_No) when 1 then replicate('0', 100 - Len(khatawani_No))+khatawani_No else khatawani_No end


                for (int i = 0; i < Result.Count; i++)
                {
                    STotal = Result[i].Amount.ToString();
                    total = total + Convert.ToDouble(STotal);
                }

            }
            catch
            {

            }

            btnTotal.Text = "0.0";
            btnTotal.Text = Convert.ToString(total);
        }

       
        public void LoadData()
        {
            List<KhatawaniTapshilNaveJama> Result;
            try
            {
                total = 0;
                var db = new SQLiteConnection(dbPath);

                var data = db.Query<KhatawaniTapshilNaveJama>("SELECT     CM.khatawani_No, GM.GirviRecordNo, GIM.metal_type, GIM.item_type, GIM.Total_Quantity, GIM.gross_wt, GIM.net_wt, GIM.fine_wt, GM.Amount, GM.Date_of_deposit, CM.FullName, CM.Contact_No, CM.Address,GM.Status FROM customer_master AS CM INNER JOIN GirviMaster AS GM ON CM.khatawani_No = GM.khatawani_No INNER JOIN GirviItemMaster AS GIM ON GM.GirviRecordNo = GIM.GirviNo ").ToList();

                Result = data;
                mListView.Adapter = new KhatawaniTapshilAdapter(this, Result);
                //SELECT khatawani_No, FullName, Contact_No, Address, occupation, cast, Address2 FROM customer_master Order by case IsNumeric(khatawani_No) when 1 then replicate('0', 100 - Len(khatawani_No))+khatawani_No else khatawani_No end


                for(int i=0;i< Result.Count;i++)
                {
                    STotal = Result[i].Amount.ToString();
                    total = total + Convert.ToDouble(STotal);
                }
                
            }
            catch (Exception e)
            {
                String E = e.ToString();
            }

            btnTotal.Text = "0.0";
            btnTotal.Text = Convert.ToString(total);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnResume()
        {
            SupportActionBar.SetTitle(Resource.String.khatawani_tapashil);
            base.OnResume();
        }
    }
}