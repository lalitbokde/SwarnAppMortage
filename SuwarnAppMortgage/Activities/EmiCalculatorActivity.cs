using System;
using System.Linq;
using Android.Support.V7.App;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;
using Android.Graphics.Drawables;
using System.IO;
using SQLite;
using System.Globalization;

namespace SuwarnAppMortgage
{
    [Activity(Label = "E.M.I Calculator")]
    public class EmiCalculatorActivity : AppCompatActivity
    {
        SettingMobileApp _ObjSettingMobileAppModel = new SettingMobileApp();
        String txt_Interset, txt_TotalAmount;
        EditText txtlonRakkam, txtWyajDar;
        TextView txtJamaTarikh, txtSodawnyachiTarikh, txtEkunDiwas, tvResult;
        Button btnCalculate, btnClear;
        string path = Application.Context.FilesDir.Path;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EmiCalculatorlayout);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.app_bar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetTitle(Resource.String.app_name);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            btnCalculate = FindViewById<Button>(Resource.Id.btnCalculate);
            btnClear = FindViewById<Button>(Resource.Id.btnClear);

            tvResult = FindViewById<TextView>(Resource.Id.lblResult);
            txtJamaTarikh = FindViewById<TextView>(Resource.Id.txtJamaTarikh);
            txtSodawnyachiTarikh = FindViewById<TextView>(Resource.Id.txtSodvanyachiTarikh);
            txtEkunDiwas = FindViewById<TextView>(Resource.Id.txtekunDiwas);
            txtlonRakkam = FindViewById<EditText>(Resource.Id.txtlonRakkam);
            txtWyajDar = FindViewById<EditText>(Resource.Id.txtWyajDar);

            txtJamaTarikh.Click += TxtJamaTarikh_Click;
            txtSodawnyachiTarikh.Click += TxtSodawnyachiTarikh_Click;

            txtlonRakkam.TextChanged += TxtlonRakkam_TextChanged;

            txtJamaTarikh.TextChanged += TxtJamaTarikh_TextChanged;
            txtSodawnyachiTarikh.TextChanged += TxtSodawnyachiTarikh_TextChanged;

            txtEkunDiwas.Click += TxtEkunDiwas_Click;

            btnCalculate.Click += BtnCalculate_Click;
            btnClear.Click += BtnClear_Click;

            txtJamaTarikh.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtSodawnyachiTarikh.Text = DateTime.Now.ToString("dd-MM-yyyy");

        }

        private void TxtEkunDiwas_Click(object sender, EventArgs e)
        {
            //txtlonRakkam.RequestFocus();
            //InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
            //inputManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            tvResult.Text = "Monthwise EMI Calculator";
            txtJamaTarikh.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtSodawnyachiTarikh.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtEkunDiwas.Text = "0";
            txtlonRakkam.Text = "";
            txtWyajDar.Text = "";
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                Drawable icon_error = Resources.GetDrawable(Resource.Drawable.alert);
                icon_error.SetBounds(0, 0, 40, 30);
                int Days = 1;
                string sEkunnDivas = (Convert.ToInt32(txtEkunDiwas.Text) + (_ObjSettingMobileAppModel.CalculationDaysDifference)).ToString();
                if (Convert.ToInt32(txtEkunDiwas.Text) >= (Days))
                {

                    if (txtlonRakkam.Text != "")
                    {
                        if (txtWyajDar.Text != "")
                        {
                            txt_Interset = InterestAmount(txtlonRakkam.Text, txtWyajDar.Text, txtEkunDiwas.Text).ToString();
                            txt_TotalAmount = (Convert.ToDouble(txt_Interset) + Convert.ToDouble(txtlonRakkam.Text)).ToString();
                            tvResult.Text = " लोन रक्कम :  " + txtlonRakkam.Text + "\n" +
                                            "व्याज रक्कम :  " + txt_Interset + "\n" +
                                            "एकुण रक्कम :  " + txt_TotalAmount;
                        }
                        else
                        {
                            txtWyajDar.RequestFocus();
                            txtWyajDar.SetError("Please Enter व्याज दर First", icon_error);
                            //Toast.MakeText(this, "Please Enter Time First", ToastLength.Long).Show();
                        }
                    }
                    else
                    {
                        txtlonRakkam.RequestFocus();
                        txtlonRakkam.SetError("Please Enter लोन रक्कम First", icon_error);
                        //Toast.MakeText(this, "Please Enter Time First", ToastLength.Long).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, "Invalid Date Selection", ToastLength.Long).Show();
                    return;
                }

            }
            catch
            { }
        }


        private void TxtlonRakkam_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {

        }

        public object InterestAmount(string Amount, string InterestRate, string noofdays)
        {
            decimal intAmount = 0;
            try
            {
                int amt = Convert.ToInt32(Amount);
                decimal rate = Convert.ToDecimal(Convert.ToDecimal(InterestRate));
                decimal dys = Convert.ToDecimal(Convert.ToInt32(noofdays));
                //intAmount = (amt * (rate * 12) * dys) / (100 * 365);
                intAmount = (amt * (rate * 12) * dys) / (365 * 100);
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

        private void TxtSodawnyachiTarikh_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                if (txtJamaTarikh.Text != "" && txtSodawnyachiTarikh.Text != "")
                {
                    double EkunDiwas = 0;
                    EkunDiwas = Convert.ToDouble(((DateTime.ParseExact(txtSodawnyachiTarikh.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture) - DateTime.ParseExact(txtJamaTarikh.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture)).TotalDays));
                    EkunDiwas += Convert.ToDouble(_ObjSettingMobileAppModel.CalculationDaysDifference);
                    txtEkunDiwas.Text = EkunDiwas.ToString();
                }
            }
            catch { }
        }

        private void TxtJamaTarikh_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                if (txtJamaTarikh.Text != "" && txtSodawnyachiTarikh.Text != "")
                {
                    double EkunDiwas = 0;
                    EkunDiwas = Convert.ToDouble(((DateTime.ParseExact(txtSodawnyachiTarikh.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture) - DateTime.ParseExact(txtJamaTarikh.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture)).TotalDays));
                    EkunDiwas += Convert.ToDouble(_ObjSettingMobileAppModel.CalculationDaysDifference);
                    txtEkunDiwas.Text = EkunDiwas.ToString();
                }
            }
            catch { }
        }

        private void TxtSodawnyachiTarikh_Click(object sender, EventArgs e)
        {

            InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
            inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);


            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                txtSodawnyachiTarikh.Text = time.ToString("dd-MM-yyyy");
            });

            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }



        private void TxtJamaTarikh_Click(object sender, EventArgs e)
        {
            InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
            inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                txtJamaTarikh.Text = time.ToString("dd-MM-yyyy");
            });

            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
                Finish();

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnResume()
        {
            SupportActionBar.SetTitle(Resource.String.emical);
            LoadData();
            base.OnResume();
        }

        public void LoadData()
        {
            try
            {
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
    }
}