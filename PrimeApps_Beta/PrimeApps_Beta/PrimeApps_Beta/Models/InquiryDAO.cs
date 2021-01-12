using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeApps_Beta.Models
{
    class InquiryDAO
    {
        public string CompanyName { get; set; }
        public string DocumentNo { get; set; }
        public string ReqLevel { get; set; }
        public string RequestTo { get; set; }
        public string ItemCode { get; set; }
        private string ItemName { get; set; }
        public string SalesPerson { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string InqStatus { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public string SellingType { get; set; }
        public string AdvQnty { get; set; }
    }

}
