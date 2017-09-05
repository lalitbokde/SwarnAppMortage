

using Android.App;
using Android.Content;
using Android.Database.Sqlite;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using SQLite;
using SuwarnAppMortgage.Activities;
using System.IO;
using System.Linq;

namespace SuwarnAppMortgage
{
    [Activity(Label = "स्वर्ण ऐप M", MainLauncher = true)]
    public class HomeActivity : AppCompatActivity
    {
        SettingMobileApp _ObjSettingMobileAppModel = new SettingMobileApp();
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        ImageButton GirviDailyReport, KhatawaniTapshil, GirviSodwa, Rokad, KhatawaniList, EmiCalculator;
        string path = Application.Context.FilesDir.Path;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Homelayout);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

            //Create ActionBarDrawerToggle button and add it to the toolbar
           var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.open_drawer, Resource.String.close_drawer);
            drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            View header = navigationView.GetHeaderView(0);

            GirviDailyReport = FindViewById<ImageButton>(Resource.Id.imgbtnGirviDailyReport);
            KhatawaniTapshil = FindViewById<ImageButton>(Resource.Id.imgbtnKhatawaniTapshil);
            GirviSodwa = FindViewById<ImageButton>(Resource.Id.imgbtnGirviSodva);
            Rokad = FindViewById<ImageButton>(Resource.Id.imgbtnRokad);
            KhatawaniList = FindViewById<ImageButton>(Resource.Id.imgbtnKhatawaniList);
            EmiCalculator = FindViewById<ImageButton>(Resource.Id.imgbtnEmiCalculator);


            EmiCalculator.Click += EmiCalculator_Click;
            KhatawaniList.Click += KhatawaniList_Click;
            Rokad.Click += Rokad_Click;
            GirviSodwa.Click += GirviSodwa_Click;
            KhatawaniTapshil.Click += KhatawaniTapshil_Click;
            GirviDailyReport.Click += GirviDailyReport_Click;

        }

        private void GirviDailyReport_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(GirviDailyReportActivity));
            this.StartActivity(intent);  
        }

        private void KhatawaniTapshil_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(KhatawaniTapshilActivity));
            this.StartActivity(intent);           
        }

        private void GirviSodwa_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(GirviSodvaActivity));
            this.StartActivity(intent);         
        }

        private void Rokad_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RokadActivity));
            this.StartActivity(intent);      
        }

        private void KhatawaniList_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(KhatawaniListActivity));
            this.StartActivity(intent);           
        }

        private void EmiCalculator_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(EmiCalculatorActivity));
            this.StartActivity(intent);       
        }

        public void onCreateDatabase()
        {
            var pathToDatabase = Path.Combine(path, "JewelleryMortgageLocalDB.db");
            SQLiteDatabase sqlDB = SQLiteDatabase.OpenOrCreateDatabase(pathToDatabase, null);
            string query;
            query = "CREATE TABLE IF NOT EXISTS `SettingMobileApp` (`SrNo`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,	`CalculationDaysDifference`	INTEGER NOT NULL DEFAULT 0);";
            sqlDB.ExecSQL(query);


            var db = new SQLiteConnection(pathToDatabase);
            SettingMobileApp data = db.Query<SettingMobileApp>("Select * from SettingMobileApp where SrNo = '1'").FirstOrDefault();
            if (data != null)
            {
                _ObjSettingMobileAppModel.CalculationDaysDifference = data.CalculationDaysDifference;
            }
            else
            {
                _ObjSettingMobileAppModel.CalculationDaysDifference = 0;

                db.Insert(_ObjSettingMobileAppModel);
                db.Commit();
            }

        }

      

        private void ShareToBrowser(string url)
        {
            if (!url.StartsWith("http"))
            {
                url = "http://" + url;
            }

            Android.Net.Uri uri = Android.Net.Uri.Parse(url);
            Intent intent = new Intent(Intent.ActionView);
            intent.SetData(uri);

            Intent chooser = Intent.CreateChooser(intent, "Open with");

            this.StartActivity(chooser);
        }

        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            var ft = FragmentManager.BeginTransaction();
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_about_us):
                    StartActivity(new Intent(this, typeof(AboutUsActivity)));
                    break;
                case (Resource.Id.nav_contact):
                    StartActivity(new Android.Content.Intent(this, typeof(ContactActivity)));
                    break;
                case (Resource.Id.nav_shareit):
                    ShareToBrowser("https://play.google.com/store/search?q=thethinker&c=apps&hl=en");
                    break;
                case (Resource.Id.nav_settings):
                    StartActivity(new Intent(this, typeof(SettingsActivity)));
                    break;
            }
            //Close drawer
            drawerLayout.CloseDrawers();
        }

        //add custom icon to tolbar
        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            if (menu != null)
            {
                menu.FindItem(Resource.Id.action_Addnew).SetVisible(true);
            }
            return base.OnCreateOptionsMenu(menu);
        }

        public override void OnBackPressed()
        {
            if (FragmentManager.BackStackEntryCount != 0)
            {
                FragmentManager.PopBackStack();// fragmentManager.popBackStack();
            }
            else
            {
                base.OnBackPressed();
            }
        }


        protected override void OnResume()
        {
            SupportActionBar.SetTitle(Resource.String.app_name);
            onCreateDatabase();
            base.OnResume();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }
    }
}