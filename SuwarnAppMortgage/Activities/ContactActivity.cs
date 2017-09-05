using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;

namespace SuwarnAppMortgage
{
    [Activity(Label = "ContactActivity")]
    public class ContactActivity : AppCompatActivity
    {
        ImageView Facebook, Share, WebShow, Location, Call;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ContactUSlayout);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetTitle(Resource.String.app_name);
            //SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            Facebook = FindViewById<ImageView>(Resource.Id.imageViewfacebook);
            Share = FindViewById<ImageView>(Resource.Id.imageViewshare);
            WebShow = FindViewById<ImageView>(Resource.Id.imageViewwebview);
            Location = FindViewById<ImageView>(Resource.Id.imageViewlocation);
            Call = FindViewById<ImageView>(Resource.Id.imageViewcall);

            Facebook.Click += Facebook_Click;
            Share.Click += Share_Click;
            WebShow.Click += WebShow_Click;
            Location.Click += Location_Click;
            Call.Click += Call_Click;
        }

        private void Call_Click(object sender, EventArgs e)
        {
            try
            {
                string telphone = "+917776801113";
                Intent phone = new Intent(Intent.ActionCall, Android.Net.Uri.Parse(string.Format("tel:{0}", telphone)));
                StartActivity(phone);
            }
            catch { }
        }

        private void Location_Click(object sender, EventArgs e)
        {
            try
            {
                ShareToBrowser("https://www.google.co.in/maps/place/TheThinker/@21.1132124,79.0564577,19z/data=!4m12!1m6!3m5!1s0x3bd4bf842418afdf:0xb0adfdde6baf6fdd!2sTheThinker!8m2!3d21.1132111!4d79.0570049!3m4!1s0x3bd4bf842418afdf:0xb0adfdde6baf6fdd!8m2!3d21.1132111!4d79.0570049?hl=en");
            }
            catch { }
        }

        private void WebShow_Click(object sender, EventArgs e)
        {
            try
            {
                ShareToBrowser("http://www.thethiinker.com/");
            }
            catch { }
        }

        private void Share_Click(object sender, EventArgs e)
        {
            try
            {
                //Android.Net.Uri uri = Android.Net.Uri.Parse(url);
                Intent intent = new Intent(Intent.ActionView);
                //intent.SetData(uri);

                Intent chooser = Intent.CreateChooser(intent, "Open with");

                this.StartActivity(chooser);
            }
            catch { }
        }
        
        private void Facebook_Click(object sender, EventArgs e)
        {
            try
            {
                ShareToBrowser("https://www.facebook.com/TheThiinker-610526002292104/");
            }
            catch { }
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

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnResume()
        {
            SupportActionBar.SetTitle(Resource.String.Contact);
            base.OnResume();
        }     
    }
}