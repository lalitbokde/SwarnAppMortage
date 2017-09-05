
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
using SQLite;
using SuwarnAppMortgage.Adapter;
using Java.Text;
using Android.Support.V7.App;
using Android.Graphics.Drawables;

namespace SuwarnAppMortgage.Activities
{
    [Activity(Label = "रोकड")]
    public class RokadActivity : AppCompatActivity
    {
        TextView txtSurvatichiTarikh, txtShewatchiTarikh;
        Button btnJama, btnNave, btnTotal;
        ListView mListView;
        Double total = 0;
        String STotal, Datett, d1, d2, StatusAssign;
        List<KhatawaniTapshilNaveJama> Result = new List<KhatawaniTapshilNaveJama>();


        string dbPath = "/storage/emulated/0/JewelleryDB.db";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Rokadlayout);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetTitle(Resource.String.app_name);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            txtSurvatichiTarikh = FindViewById<TextView>(Resource.Id.txtRSurwatichiTarikh);
            txtShewatchiTarikh = FindViewById<TextView>(Resource.Id.txtRShewatchiTarikh);
            btnJama = FindViewById<Button>(Resource.Id.btnKPJma);
            btnTotal = FindViewById<Button>(Resource.Id.btnTotalAmount);
            btnNave = FindViewById<Button>(Resource.Id.btnKPNave);
            mListView = FindViewById<ListView>(Resource.Id.listViewKhatawaniList);

            txtSurvatichiTarikh.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtShewatchiTarikh.Text = DateTime.Now.ToString("dd/MM/yyyy");
            btnJama.Click += BtnJama_Click;
            btnNave.Click += BtnNave_Click;

            txtShewatchiTarikh.Click += TxtShewatchiTarikh_Click;
            txtSurvatichiTarikh.Click += TxtSurvatichiTarikh_Click;

            txtShewatchiTarikh.AfterTextChanged += TxtShewatchiTarikh_AfterTextChanged;
            txtSurvatichiTarikh.AfterTextChanged += TxtSurvatichiTarikh_AfterTextChanged;
            //LoadData();
        }

        private void TxtSurvatichiTarikh_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            StatusAssign = "unchange";
            LoadDataDateWise();

        }

        private void TxtShewatchiTarikh_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            StatusAssign = "unchange";
            LoadDataDateWise();
        }


        public void LoadDataDateWise()
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
                btnNave.RequestFocus();
                total = 0;
                var db = new SQLiteConnection(dbPath);

                tstart = myFormat.Format(fromUser.Parse(d1));
                tend = myFormat.Format(fromUser.Parse(d2));


                var data1 = db.Query<KhatawaniTapshilNaveJama>("SELECT     CM.khatawani_No, GM.GirviRecordNo, GIM.metal_type, GIM.item_type, GIM.Total_Quantity, GIM.gross_wt, GIM.net_wt, GIM.fine_wt, GM.Amount, GM.Date_of_deposit, CM.FullName, CM.Contact_No, CM.Address,GM.Status,datetime(substr(Date_of_deposit, 7, 4) || '-' || substr(Date_of_deposit, 4, 2) || '-' || substr(Date_of_deposit, 1, 2)) AS SomeDate FROM customer_master AS CM INNER JOIN GirviMaster AS GM ON CM.khatawani_No = GM.khatawani_No INNER JOIN GirviItemMaster AS GIM ON GM.GirviRecordNo = GIM.GirviNo where GM.Status = '" + StatusAssign + "' and SomeDate >= DATE('"
                                + tstart
                                + "') AND SomeDate <= DATE('"
                                + tend
                                + "') order by SomeDate desc ").ToList();


                Result = data1;

                mListView.Adapter = new RokadAdapter(this, Result);
                for (int i = 0; i < mListView.Count; i++)
                {

                    STotal = Result[i].Amount.ToString();
                    total = total + Convert.ToDouble(STotal);
                }
            }
            catch { }


            btnTotal.Text = "0.0";
            btnTotal.Text = Convert.ToString(total);
        }

        private void TxtShewatchiTarikh_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                Datett = time.ToString("dd/MM/yyyy");
                txtShewatchiTarikh.Text = Datett;
            });

            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }


        private void TxtSurvatichiTarikh_Click(object sender, EventArgs e)
        {

            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                Datett = time.ToString("dd/MM/yyyy");
                txtSurvatichiTarikh.Text = Datett;
            });

            frag.Show(FragmentManager, DatePickerFragment.TAG);

        }

        private void BtnNave_Click(object sender, EventArgs e)
        {
            StatusAssign = "unchange";
            LoadDataDateWise();
        }

        private void BtnJama_Click(object sender, EventArgs e)
        {
            StatusAssign = "Release";
            LoadDataDateWise();
           
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
                mListView.Adapter = new RokadAdapter(this, Result);

                for (int i = 0; i < Result.Count; i++)
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
            SupportActionBar.SetTitle(Resource.String.rokad);
            base.OnResume();
        }
    }
}