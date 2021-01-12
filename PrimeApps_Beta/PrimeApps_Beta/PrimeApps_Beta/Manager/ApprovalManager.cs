using System;
using System.Collections.Generic;
using System.Text;
using PrimeApps_Beta.Models;
using PrimeApps_Beta.Gateway;
using System.Data;
using System.Threading.Tasks;
using PrimeApps_Beta.Services;
using System.Linq;

namespace PrimeApps_Beta.Manager
{
    class ApprovalManager
    {
        internal static int InsertSgnatureLOg(string companyName, string docNo, string signedBy, DateTime signedDate, string signTitle, string signedPCID, string documentName)
        {
            return new Mkt_DataGateway().InsertSgnatureLOg(companyName, docNo, signedBy, signedDate, signTitle, signedPCID, documentName);
        }

        internal static int UpdatePIApprvalStatus(string companyName, string docNo)
        {
            return new Mkt_DataGateway().UpdatePIApprvalStatus(companyName, docNo);
        }

        internal static int UpdateApprovalStatus(string companyName, string docNo, string getCurrentApprovalLevel, string getUserName, DateTime approveTime, string signedPCID)
        {
            return new Mkt_DataGateway().UpdateApprovalStatus(companyName, docNo, getCurrentApprovalLevel, getUserName, approveTime, signedPCID);
        }
    }
}
