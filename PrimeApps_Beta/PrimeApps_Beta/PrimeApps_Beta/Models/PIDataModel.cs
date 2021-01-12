using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeApps_Beta.Models
{
    public class PIDataModel
    {
        public string CompanyID { get; set; }
        public string PINO { get; set; }
        public DateTime PIDate { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerName { get; set; }
        public string Tenor { get; set; }
        public string Ref { get; set; }
        public decimal PIQuantity { get; set; }
        public decimal Amount { get; set; }
        public string RequestTo { get; set; }
        public decimal ToBeAdjQnty { get; set; }
        public decimal TotalAdvQnty { get; set; }
        public decimal PendingAdvance { get; set; }
    }
}
