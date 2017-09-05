
using Android.App;
using Android.OS;
using Android.Views;
using Android.Support.V7.App;

namespace SuwarnAppMortgage
{
    [Activity(Label = "RegistrationActivity")]
    public class RegistrationActivity : AppCompatActivity
    {
   
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Registrationlayout);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetTitle(Resource.String.app_name);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnResume()
        {
            SupportActionBar.SetTitle(Resource.String.Registration);
            base.OnResume();
        }
    }
}