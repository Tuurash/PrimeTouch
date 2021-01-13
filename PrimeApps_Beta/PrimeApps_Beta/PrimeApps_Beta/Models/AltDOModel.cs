using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeApps_Beta.Models
{
    public class AltDOModel
    {
        public string CompanyName { get; set; }
        public string DocumentName { get; set; }
        public string DocumentNo { get; set; }
        public string RequestTo { get; set; }
        public string RequestTime { get; set; }
        public string RequestBy { get; set; }
        public string ReqLevel { get; set; }
        public string ApproveTime { get; set; }
        public string ApprovedBy { get; set; }
        public string UserIP { get; set; }
        public string DigitalSigned { get; set; }
        public string Remarks { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public string AltReqNo { get; set; }
        public string Ref { get; set; }
        public string BuyerName { get; set; }
        public string LCNo { get; set; }
        public string InvoiceNo { get; set; }
        public string Dotype { get; set; }

        //for Detail
        public decimal ItemRate { get; set; }
        public string Count { get; set; }
        public string ShortName { get; set; }
        public string Discount { get; set; }

    }
}
