using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeApps_Beta.Models
{
    public class DeliveryScheduleModel
    {
        public string DocumentName { get; set; }
        public string DocumentNo { get; set; }
        public DateTime ScheduleDate { get; set; }
        public decimal ScheduleQnty { get; set; }
        public decimal PKT { get; set; }
        public decimal TON { get; set; }


        //for Detail
        public string CompanyName { get; set; }
        public string BuyerName { get; set; }
        public string Ref { get; set; }
        public string DONo { get; set; }
        public string shortName { get; set; }
        public string LotNo { get; set; }
        public string DoQnty { get; set; }
        public string StockQnty { get; set; }
        public decimal Rate { get; set; }
        public string BuyerCode { get; set; }
        public string Amount { get; set; }
    }
}
