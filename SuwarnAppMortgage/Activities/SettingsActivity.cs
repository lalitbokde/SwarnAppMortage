using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.App;
using System;
using System.IO;
using System.Linq;
using SQLite;

namespace SuwarnAppMortgage.Activities
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : AppCompatActivity
    {
        Button btnSaveOrUpdate;
        TextView edDaysAddOrRemove;
        SettingMobileApp _ObjSettingMobileAppModel = new SettingMobileApp();
        string path = Application.Context.FilesDir.Path;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SettingsLayout);


            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetTitle(Resource.String.app_name);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            btnSaveOrUpdate = FindViewById<Button>(Resource.Id.btnSaveOrUpdate);
            edDaysAddOrRemove = FindViewById<EditText>(Resource.Id.txt_DaysAddOrRemove);
         
            btnSaveOrUpdate.Click += BtnSaveOrUpdate_Click;
        }

        private void BtnSaveOrUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var pathToDatabase = Path.Combine(path, "JewelleryMortgageLocalDB.db");
                var db = new SQLiteConnection(pathToDatabase);
                var _ObjRates = db.Query<SettingMobileApp>("SELECT * from SettingMobileApp").FirstOrDefault();
                _ObjSettingMobileAppModel.SrNo = _ObjRates.SrNo;
                _ObjSettingMobileAppModel.CalculationDaysDifference = Convert.ToInt32(edDaysAddOrRemove.Text);
                db.Update(_ObjSettingMobileAppModel);
                db.Commit();
                Toast.MakeText(this, "Updated Sucessfully", ToastLength.Short).Show();
                LoadData();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void LoadData()
        {
            var pathToDatabase = Path.Combine(path, "JewelleryMortgageLocalDB.db");
            var db = new SQLiteConnection(pathToDatabase);
            SettingMobileApp data = db.Query<SettingMobileApp>("Select * from SettingMobileApp where SrNo = '1'").FirstOrDefault();
            if (data != null)
            {
                edDaysAddOrRemove.Text = data.CalculationDaysDifference.ToString();
            }
            else
            {
                _ObjSettingMobileAppModel.CalculationDaysDifference = 0;

                db.Insert(_ObjSettingMobileAppModel);
                db.Commit();
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
            SupportActionBar.SetTitle(Resource.String.Settings);
            LoadData();
            base.OnResume();
        }
    }
}