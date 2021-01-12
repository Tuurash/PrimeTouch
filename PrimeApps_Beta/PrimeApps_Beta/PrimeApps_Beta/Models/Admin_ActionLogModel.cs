using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeApps_Beta.Models
{
    public class Admin_ActionLogModel
    {
        public string CompanyName { get; set; }
        public string ActionLog { get; set; }
        public DateTime ActionDate { get; set; }
        public string Module { get; set; }
        public string UserID { get; set; }
        public string ActionDocNo { get; set; }

    }
}
