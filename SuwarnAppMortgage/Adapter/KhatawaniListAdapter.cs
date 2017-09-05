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
    public class KhatawaniListAdapter : BaseAdapter<customer_master>
    {
        Activity context;
        List<customer_master> list;
        int SrNo;

        public KhatawaniListAdapter(Activity _context, List<customer_master> _list)
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


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.KhatawaniListItem, parent, false);

            customer_master item = this[position];
            if (item != null)
            {
                view.FindViewById<TextView>(Resource.Id.lblGirvikramank).Text = item.khatawani_No == 0 ? "" : item.khatawani_No.ToString();
                view.FindViewById<TextView>(Resource.Id.lblNave).Text = item.FullName == null ? "" : item.FullName.ToString();
                view.FindViewById<TextView>(Resource.Id.lblFoneNo).Text = item.Contact_No == null ? "" : item.Contact_No.ToString();
                view.FindViewById<TextView>(Resource.Id.lblPatta).Text = item.Address == null ? "" : item.Address.ToString();

                SrNo++;
            }
            return view;

        }
    }
}