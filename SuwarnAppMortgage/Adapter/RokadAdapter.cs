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
    public class RokadAdapter : BaseAdapter<KhatawaniTapshilNaveJama>
    {
        Activity context;
        List<KhatawaniTapshilNaveJama> list;
        int SrNo;
        String Message;

        public RokadAdapter(Activity _context, List<KhatawaniTapshilNaveJama> _list)
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

        public override KhatawaniTapshilNaveJama this[int index]
        {
            get { SrNo = 1; return list[index]; }
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.RokadListItem, parent, false);

            KhatawaniTapshilNaveJama item = this[position];
            if (item != null)
            {


                Message = (item.metal_type + " " + item.item_type + " (" + item.Total_Quantity + "nos.) " + "\n" +
               "ग्रॉस वेट : " + item.gross_wt + " gm " + "\n" +
               "नेट वेट : " + item.net_wt + " gm " + "\n" +
               "फाईन वेट : " + item.fine_wt + " gm ") == null ? "" : (item.metal_type + " " + item.item_type + " (" + item.Total_Quantity + "nos.) " + "\n" +
               "ग्रॉस वेट : " + item.gross_wt + " gm " + "\n" +
               "नेट वेट : " + item.net_wt + " gm " + "\n" +
               "फाईन वेट : " + item.fine_wt + " gm ").ToString();


                view.FindViewById<TextView>(Resource.Id.lblRNav).Text = item.FullName == null ? "" : item.FullName.ToString();
                view.FindViewById<TextView>(Resource.Id.lblRGirviKramank).Text = item.GirviRecordNo == null ? "" : item.GirviRecordNo.ToString();
                view.FindViewById<TextView>(Resource.Id.lblRWarnan).Text = Message;
                view.FindViewById<TextView>(Resource.Id.lblRRakkam).Text = item.Amount == null ? "" : item.Amount.ToString();
                
            }
            return view;

        }
    }
}