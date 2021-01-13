using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeApps_Beta.Models
{
    public class ADORequestModel
    {
        //for Card
        public string CompanyName { get; set; }
        public string BuyerCode { get; set; }
        public string AdvNO { get; set; }
        public string PINo { get; set; }
        public DateTime AdvDate { get; set; }
        public string BuyerName { get; set; }
        public decimal Amount { get; set; }
        public string RequestTo { get; set; }
        public string ApprovedBy { get; set; }
        public string ReqLevel { get; set; }

        //for Details
        //AdvNO,AdvDate,BuyerCode,BuyerName,YarnCode,Rate,Quantity,Discount,Value,Ref,CompanyName,Status,Tenor,PINo,'Requested Advance' as AdvPosition
        public string YarnCode { get; set; }
        public decimal Rate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal Value { get; set; }
        public string ShortName { get; set; }
        public string TotalAmount { get; set; }


    }
}
