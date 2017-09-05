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
    public class KhatawaniTapshilAdapter : BaseAdapter<KhatawaniTapshilNaveJama>
    {
        Activity context;
        List<KhatawaniTapshilNaveJama> list;
        int SrNo;

        public KhatawaniTapshilAdapter(Activity _context, List<KhatawaniTapshilNaveJama> _list)
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
                view = context.LayoutInflater.Inflate(Resource.Layout.KhatawaniTapashilListItem, parent, false);

            KhatawaniTapshilNaveJama item = this[position];
            if (item != null)
            {
                view.FindViewById<TextView>(Resource.Id.lblKTGirviKramank).Text = item.GirviRecordNo == null ? "" : item.GirviRecordNo.ToString();
                view.FindViewById<TextView>(Resource.Id.lblKTGiraviTarikh).Text = item.Date_of_deposit == null ? "" : item.Date_of_deposit.ToString();
                view.FindViewById<TextView>(Resource.Id.lblKTWarnan).Text = (item.metal_type +" "+ item.item_type + " (" + item.Total_Quantity + "nos.) " +
                    "ग्रॉस वेट : " + item.gross_wt +" gm "+ "नेट वेट : " + item.net_wt + " gm " + "फाईन वेट : " + item.fine_wt + " gm ") == null ? "" : (item.metal_type +
                    " " + item.item_type + " (" + item.Total_Quantity + " nos.) " + "ग्रॉस वेट : " + item.gross_wt + " gm " + "नेट वेट : " + item.net_wt + " gm " + "फाईन वेट : " +
                    item.fine_wt + " gm ").ToString();
                view.FindViewById<TextView>(Resource.Id.lblKTRakkam).Text = item.Amount == null ? "" : item.Amount.ToString();
                
            }
            return view;

        }
    }
}