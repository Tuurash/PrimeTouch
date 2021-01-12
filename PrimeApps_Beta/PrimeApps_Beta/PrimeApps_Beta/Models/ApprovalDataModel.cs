using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeApps_Beta.Models
{
    public class ApprovalDataModel
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
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public string SellingType { get; set; }
        public decimal AdvQnty { get; set; }
        public string CompanyInq { get; set; }
        public string ShortName { get; set; }
        public decimal Amount { get; set; }
        public DateTime InquiryDate { get; set; }
    }



}
