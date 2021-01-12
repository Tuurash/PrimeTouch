using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeApps_Beta.Models
{
    public class DODataModel
    {
        public string Company { get; set; }
        public string BuyerName { get; set; }
        public string LCNoNo { get; set; }
        public string InvoiceNo { get; set; }
        public string DONo { get; set; }
        public string Ref { get; set; }
        public string DOType { get; set; }
        public string SellingType { get; set; }
        public decimal DoQnty { get; set; }
        public decimal Amount { get; set; }
        public string RequestTo { get; set; }
    }
}
