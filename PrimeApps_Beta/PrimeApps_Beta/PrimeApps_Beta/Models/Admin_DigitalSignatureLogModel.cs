using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeApps_Beta.Models
{
    public class Admin_DigitalSignatureLogModel
    {
        private string companyName { get; set; }
        private string documentName { get; set; }
        private string documentNo { get; set; }
        private string signedBy { get; set; }
        private DateTime signedDate { get; set; }
        private string signTitle { get; set; }
        private string signedPCID { get; set; }
    }
}
