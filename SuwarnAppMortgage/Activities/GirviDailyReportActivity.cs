
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;
using SQLite;
using SuwarnAppMortgage.Adapter;
using Java.Text;
using Android.Text;
using Android.Support.V7.App;
using System.IO;

namespace SuwarnAppMortgage
{
    [Activity(Label = "Mortgage Daily Report")]
    public class GirviDailyReportActivity : AppCompatActivity
    {
        String d1, d2;
        ListView mListView;
        List<customer_master> Result = new List<customer_master>();
        customer_master ContactNo = new customer_master();
        Vibrator myVib;
        string filename = "/storage/emulated/0/JewelleryDB.db";
        TextView txtSurvatichiTarikh, txtShewatchiTarikh;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GirviDailyReportlayout);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetTitle(Resource.String.app_name);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            myVib = (Vibrator)this.GetSystemService(VibratorService);

            txtSurvatichiTarikh = FindViewById<TextView>(Resource.Id.txtSurwatichiTarikh);
            txtShewatchiTarikh = FindViewById<TextView>(Resource.Id.txtShewatchiTarikh);
            mListView = FindViewById<ListView>(Resource.Id.listViewGirviDailyReport);

            txtSurvatichiTarikh.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtShewatchiTarikh.Text = DateTime.Now.ToString("dd/MM/yyyy");


            txtShewatchiTarikh.Click += TxtShewatchiTarikh_Click;
            txtSurvatichiTarikh.Click += TxtSurvatichiTarikh_Click;

            LoadData();

            txtShewatchiTarikh.AfterTextChanged += TxtShewatchiTarikh_AfterTextChanged;
            txtSurvatichiTarikh.AfterTextChanged += TxtSurvatichiTarikh_AfterTextChanged;
            mListView.ItemClick += MListView_ItemClick;
            mListView.ItemLongClick += MListView_ItemLongClick;

           
            
        }

        private void MListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Call(e.Position);
        }


        public void Call(int Position)
        {
            ContactNo = Result.ElementAt(Position);

            String sContactNo = ContactNo.Contact_No.ToString();
            try
            {
                if (sContactNo != null)
                {
                    if (sContactNo != "")
                    {
                        Double isContactNo = Convert.ToDouble(sContactNo);
                        if (isContactNo > 9)
                        {
                            string telphone = sContactNo.Trim();
                            Intent phone = new Intent(Intent.ActionCall, Android.Net.Uri.Parse(string.Format("tel:{0}", telphone)));
                            StartActivity(phone);
                        }
                        else
                        {
                            myVib.Vibrate(100);
                            Toast.MakeText(this, "Mobile number is incorrect.", ToastLength.Short).Show();
                            return;
                        }
                    }
                    else
                    {
                        myVib.Vibrate(100);
                        Toast.MakeText(this, "Mobile number not available.", ToastLength.Short).Show();
                        return;
                    }
                }
                else
                {
                    myVib.Vibrate(100);
                    Toast.MakeText(this, "Mobile number not available.", ToastLength.Short).Show();
                    return;
                }
            }

            catch { }
        }


        customer_master GirviNO = new customer_master();
        KhatawaniTapshilNaveJama item = new KhatawaniTapshilNaveJama();
        String Message, ForworDetails, FullMessage;
        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);

            List<KhatawaniTapshilNaveJama> ResultNew;
            try
            {
                var db = new SQLiteConnection(filename);
                GirviNO = Result.ElementAt(e.Position);

                String str = GirviNO.receipt_no.ToString();
                var data = db.Query<KhatawaniTapshilNaveJama>("SELECT CM.khatawani_No, GM.GirviRecordNo, GIM.metal_type, GIM.item_type, GIM.Total_Quantity, GIM.gross_wt, GIM.net_wt, GIM.fine_wt, GM.Amount, GM.Date_of_deposit, GM.forwardstatus, GM.Forwarded_to, GM.Date_of_Forward, GM.forwardamount, CM.FullName, CM.Contact_No, CM.Address,GM.Status FROM customer_master AS CM INNER JOIN GirviMaster AS GM ON CM.khatawani_No = GM.khatawani_No INNER JOIN GirviItemMaster AS GIM ON GM.GirviRecordNo = GIM.GirviNo where GM.Status = 'unchange' and GM.receipt_no = '" + str + "'").ToList();

                ResultNew = data;
                item = ResultNew.ElementAt(0);
            }
            catch { }

            Message = (item.metal_type + " " + item.item_type + " (" + item.Total_Quantity + "nos.) " + "\n" +
              "Gross weight : " + item.gross_wt + " gm " + "\n" +
              "Net weight : " + item.net_wt + " gm " + "\n" +
              "Fine weight : " + item.fine_wt + " gm ") == null ? "" : (item.metal_type + " " + item.item_type + " (" + item.Total_Quantity + "nos.) " + "\n" +
              "Gross weight : " + item.gross_wt + " gm " + "\n" +
              "Net weight : " + item.net_wt + " gm " + "\n" +
              "Fine weight : " + item.fine_wt + " gm ").ToString();

            if (item.forwardstatus == "Forward")
            {
                ForworDetails = ("Forward To : " + item.Forwarded_to + "\n" +
                  "Forward Date : " + item.Date_of_Forward + "\n" +
                  "Forward Amount : " + item.forwardamount) == null ? "" : ("Forward To : " + item.Forwarded_to + "\n" +
                  "Forward Date : " + item.Date_of_Forward + "\n" +
                  "Forward Amount : " + item.forwardamount).ToString();

                FullMessage = Message + "\n" +
                            "\n" +
                            "\n" +
                             "\n" +
            "***Girvi Forward Details***" + "\n" +
                            "\n" +
                            ForworDetails;
            }
            else
            {
                FullMessage = Message;
            }
            alert.SetTitle(Html.FromHtml("<font color='#EC407A'>Mortgage Details</font>"));
            alert.SetMessage(FullMessage);
            alert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                return;
            });

            Dialog dialog = alert.Create();
            dialog.Show();
        }

        private void TxtSurvatichiTarikh_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            DatewiseLoadDate();
        }

        private void TxtShewatchiTarikh_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            DatewiseLoadDate();
        }

        private void TxtSurvatichiTarikh_Click(object sender, EventArgs e)
        {

            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                txtSurvatichiTarikh.Text = time.ToString("dd-MM-yyyy");
            });

            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void TxtShewatchiTarikh_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                txtShewatchiTarikh.Text = time.ToString("dd-MM-yyyy");
            });

            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }


        public void DatewiseLoadDate()
        {

            SimpleDateFormat myFormat = new SimpleDateFormat("yyyy-MM-dd");
            SimpleDateFormat fromUser = new SimpleDateFormat("dd-MM-yyyy");

            d1 = txtSurvatichiTarikh.Text;
            d2 = txtShewatchiTarikh.Text;


            d1 = d1.Replace("/", "-");
            d2 = d2.Replace("/", "-");
            String[] data = d2.Split('-');
            d2 = Convert.ToString(Convert.ToInt32(data[0]) + 1) + "-" + data[1]
                    + "-" + data[2];
            String tstart = "", tend = "";
            try
            {
                tstart = myFormat.Format(fromUser.Parse(d1));
                tend = myFormat.Format(fromUser.Parse(d2));


                
                var db = new SQLiteConnection(filename);

                var data1 = db.Query<customer_master>("SELECT  customer_master.FullName, customer_master.Address,customer_master.Contact_No,GirviMaster.receipt_no,GirviMaster.interset_rate,GirviMaster.Amount, GirviMaster.Date_of_deposit,GirviMaster.forwardstatus,datetime(substr(Date_of_deposit, 7, 4) || '-' || substr(Date_of_deposit, 4, 2) || '-' || substr(Date_of_deposit, 1, 2)) AS SomeDate ,GirviMaster.khatawani_No, GirviMaster.duration, GirviMaster.GirviRecordNo,customer_master.PageNo FROM  customer_master INNER JOIN GirviMaster ON customer_master.khatawani_No = GirviMaster.khatawani_No WHERE (status='unchange') and SomeDate >= DATE('"
                                + tstart
                                + "') AND SomeDate <= DATE('"
                                + tend
                                + "') order by SomeDate desc ").ToList();


                Result = data1;
                mListView.Adapter = new GirviDailyReportAdapter(this, Result);

            }
            catch (ParseException e1)
            {
                e1.PrintStackTrace();
            }
        }

        public void LoadData()
        {
            List<customer_master> Result1;
            try
            {
                var db = new SQLiteConnection(filename);
                Console.WriteLine("filename : " + filename);
                Console.WriteLine("db : " + db);
                var data = db.Query<customer_master>("SELECT  customer_master.FullName, customer_master.Address,customer_master.Contact_No,GirviMaster.receipt_no,GirviMaster.interset_rate, GirviMaster.Amount,GirviMaster.Date_of_deposit, GirviMaster.khatawani_No, GirviMaster.duration, GirviMaster.GirviRecordNo,customer_master.PageNo FROM  customer_master INNER JOIN GirviMaster ON customer_master.khatawani_No = GirviMaster.khatawani_No WHERE (status='unchange')").ToList();

                Result1 = data;
                mListView.Adapter = new GirviDailyReportAdapter(this, Result);
            }
            catch (Exception e)
            {
                String E = e.ToString();
                Console.WriteLine("Exception : " + E, ConsoleColor.DarkRed);
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
            SupportActionBar.SetTitle(Resource.String.girvi_daily_report);
            base.OnResume();
        }
    }
}