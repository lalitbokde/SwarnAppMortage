using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using SQLite;
using SuwarnAppMortgage.Adapter;

namespace SuwarnAppMortgage.Activities
{
    [Activity(Label = "Accounting List")]
    public class KhatawaniListActivity : AppCompatActivity
    {
        Vibrator myVib;
        AutoCompleteTextView Search;
        List<KhatawaniTapshilNaveJama> Result = new List<KhatawaniTapshilNaveJama>();
        customer_master ContactNo = new customer_master();
        List<customer_master> ResultCoNo = new List<customer_master>();
        List<KhatawaniTapshilNaveJama> ResultCustomerName;
        ListView mListView;
        string dbPath = "/storage/emulated/0/JewelleryDB.db";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.KhatawaniListlayout);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetTitle(Resource.String.app_name);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            myVib = (Vibrator)this.GetSystemService(VibratorService);

            Search = FindViewById<AutoCompleteTextView>(Resource.Id.autoKSearch);
            mListView = FindViewById<ListView>(Resource.Id.listViewKhatawaniList);

            LoadData();
            CustomerNameSearch();
            Search.AfterTextChanged += Search_AfterTextChanged;
            mListView.ItemLongClick += MListView_ItemLongClick;

        }

        private void MListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            Call(e.Position);
        }

        private void Search_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            List<customer_master> Result;
            try
            {
                var db = new SQLiteConnection(dbPath);

                var data = db.Query<customer_master>("Select * from customer_master where FullName = '" + Search.Text.Trim().ToString() + "'").ToList();

                Result = data;
                mListView.Adapter = new KhatawaniListAdapter(this, Result);
            }
            catch 
            {
                String E = e.ToString();
            }
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

                ArrayAdapter adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, CustomerNameString);
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
                var db = new SQLiteConnection(dbPath);

                var data = db.Query<customer_master>("Select * from customer_master").ToList();

                ResultCoNo = data;
                mListView.Adapter = new KhatawaniListAdapter(this, ResultCoNo);
            }
            catch (Exception e)
            {
                String E = e.ToString();
            }
        }

        
        public void Call(int Position)
        {
            ContactNo = ResultCoNo.ElementAt(Position);

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


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnResume()
        {
            SupportActionBar.SetTitle(Resource.String.khatawani_list);
            base.OnResume();
        }
    }
}