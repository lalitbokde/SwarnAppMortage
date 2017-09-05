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
using SQLite;

namespace SuwarnAppMortgage
{
    [Activity(Label = "Models")]
    public class Models
    {
    }

    public class AddressMaster
    {
        public int srno { get; set; }
        public int Address { get; set; }
    }

    public class customer_master
    {
        public string Date_of_deposit { get; set; }
        public string receipt_no { get; set; }
        public int khatawani_No { get; set; }
        public string FullName { get; set; }
        public string Amount { get; set; }
        public string Contact_No { get; set; }
        public string Address { get; set; }
        public string occupation { get; set; }
        public string cast { get; set; }
        public string Address2 { get; set; }
                public string forwardstatus { get; set; }
    }

    //SELECT  customer_master.FullName, customer_master.Address,customer_master.Contact_No,GirviMaster.Amount,GirviMaster.interset_rate, GirviMaster.Date_of_deposit, GirviMaster.khatawani_No, GirviMaster.duration, GirviMaster.GirviRecordNo,customer_master.PageNo FROM  customer_master INNER JOIN GirviMaster ON customer_master.khatawani_No = GirviMaster.khatawani_No WHERE (status='unchange')
    public class GirviMaster
    {
        public int khatawani_No { get; set; }
        public string receipt_no { get; set; }
        public string Amount { get; set; }
        public string Date_of_deposit { get; set; }
        public string interset_rate { get; set; }
        public string withdraw_release_date { get; set; }
        public string Status { get; set; }
    }

    public class GirviDailyReport
    {
        public int khatawani_No { get; set; }
        public string receipt_no { get; set; }
        public string Amount { get; set; }
        public string Date_of_deposit { get; set; }
        public string interset_rate { get; set; }
        public string withdraw_release_date { get; set; }
        public string Status { get; set; }
    }

    public class KhatawaniTapshilNaveJama
    {
        public int khatawani_No { get; set; }
        public string GirviRecordNo { get; set; }
        public string FullName { get; set; }
        public string Contact_No { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string metal_type { get; set; }
        public string item_type { get; set; }
        public string Total_Quantity { get; set; }
        public string gross_wt { get; set; }
        public string net_wt { get; set; }
        public string fine_wt { get; set; }
        public string Amount { get; set; }
        public string Date_of_deposit { get; set; }

        public string forwardstatus { get; set; }
        public string Forwarded_to { get; set; }
        public string Date_of_Forward { get; set; }

        public string forwardamount { get; set; }

    }

    public class CustomerNameAndKhatawaniNo
    {
        public string khatawani_No { get; set; }
        public string FullName { get; set; }
    }

    public class SettingMobileApp
    {
        [PrimaryKey, AutoIncrement, NotNull, Column("SrNo")]
        public int SrNo { get; set; }
        public int CalculationDaysDifference { get; set; }
    }
}