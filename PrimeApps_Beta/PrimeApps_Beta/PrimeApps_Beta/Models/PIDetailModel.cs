﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeApps_Beta.Models
{
    public class PIDetailModel
    {
        public string ShortName { get; set; }
        public string PINO { get; set; }
        public DateTime PIDate { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
        public string YarnCode { get; set; }
        public decimal Rate { get; set; }
        public decimal PIQuantity { get; set; }
        public decimal PIValue { get; set; }
        public string Ref { get; set; }
        public string CompanyID { get; set; }

    }
}
