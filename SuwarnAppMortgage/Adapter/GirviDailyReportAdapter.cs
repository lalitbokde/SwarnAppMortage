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

namespace SuwarnAppMortgage.Adapter
{
    [Activity(Label = "GirviDailyReportAdapter")]
    public class GirviDailyReportAdapter : BaseAdapter<customer_master>
    {
        Activity context;
        List<customer_master> list;
        int SrNo;

        public GirviDailyReportAdapter(Activity _context, List<customer_master> _list)
                : base()
        {
            this.context = _context;
            this.list = _list;

        }

        public override int Count
        {
            get { return list.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override customer_master this[int index]
        {
            get { SrNo = 1; return list[index]; }
        }

        String FStatus = "";
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.GirviDailyReportLisrItem, parent, false);

            customer_master item = this[position];
            if (item != null)
            {
                FStatus = item.forwardstatus == null ? "" : item.forwardstatus.ToString();
                view.FindViewById<TextView>(Resource.Id.lblGDRThewTarikh).Text = item.Date_of_deposit == null ? "" : item.Date_of_deposit.ToString();
                view.FindViewById<TextView>(Resource.Id.lblGDRGirviKramank).Text = item.receipt_no == null ? "" : item.receipt_no.ToString();
                view.FindViewById<TextView>(Resource.Id.lblGDRRakkam).Text = item.Amount == null ? "" : item.Amount.ToString();

                if (FStatus == "Forward")
                { view.FindViewById<TextView>(Resource.Id.lblGDRNave).Text = item.FullName == null ? "" : "@ " + item.FullName.ToString(); }
                else { view.FindViewById<TextView>(Resource.Id.lblGDRNave).Text = item.FullName == null ? "" : item.FullName.ToString(); }
                view.FindViewById<TextView>(Resource.Id.lblGDRFoneNO).Text = item.Contact_No == null ? "" : item.Contact_No.ToString();


                SrNo++;
            }
            return view;

        }
    }
}