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
using Android.Webkit;

namespace SuwarnAppMortgage
{
    [Activity(Label = "webViewActivity")]
    public class webViewActivity : Activity
    {
   
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.webviewlayout);

            ShareToBrowser("https://play.google.com/store/search?q=appsthentic&hl=en");
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


        public class MyWebViewClient : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                view.LoadUrl(url);
                return true;
            }
        }

        public class MyWebChromeClient : WebChromeClient
        {
            private readonly Context _context;

            public MyWebChromeClient(Context context)
            {
                _context = context;
            }

            public override void OnGeolocationPermissionsShowPrompt(string origin, GeolocationPermissions.ICallback callback)
            {
                const bool remember = false;
                var builder = new AlertDialog.Builder(_context);
                builder.SetTitle("Location")
                    .SetMessage(string.Format("would like to use your current location", origin))
                    .SetPositiveButton("Allow", (sender, args) => callback.Invoke(origin, true, remember))
                    .SetNegativeButton("Disallow", (sender, args) => callback.Invoke(origin, false, remember));
                var alert = builder.Create();
                alert.Show();
            }
        }
    }
}