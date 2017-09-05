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
    public  class GirviSodvaAdapter : BaseAdapter<GirviMaster>
    {
        Activity context;
        List<GirviMaster> list;
        int SrNo;

        public GirviSodvaAdapter(Activity _context, List<GirviMaster> _list)
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

        public override GirviMaster this[int index]
        {
            get { SrNo = 1; return list[index]; }
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.GirviSodvaItemList, parent, false);

            GirviMaster item = this[position];
            if (item != null)
            {
                view.FindViewById<TextView>(Resource.Id.lblGSGirvikramank).Text = item.receipt_no == null ? "" : item.receipt_no.ToString();
                view.FindViewById<TextView>(Resource.Id.lblRakkam).Text = item.Amount == null ? "" : item.Amount.ToString();
                view.FindViewById<TextView>(Resource.Id.lblThewTarikh).Text = item.Date_of_deposit == null ? "" : item.Date_of_deposit.ToString();
                view.FindViewById<TextView>(Resource.Id.lblWyajTakke).Text = item.interset_rate == null ? "" : item.interset_rate.ToString();
                view.FindViewById<TextView>(Resource.Id.lblKalawadhi).Text = item.withdraw_release_date == null ? "" : item.withdraw_release_date.ToString();
                view.FindViewById<TextView>(Resource.Id.lblStithi).Text = item.Status == null ? "" : item.Status.ToString();

                SrNo++;
            }
            return view;

        }
    }
}