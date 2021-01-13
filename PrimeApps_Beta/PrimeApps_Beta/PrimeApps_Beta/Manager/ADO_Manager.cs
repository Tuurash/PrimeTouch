using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PrimeApps_Beta.Gateway;
using PrimeApps_Beta.Models;
using PrimeApps_Beta.Services;

namespace PrimeApps_Beta.Manager
{
    class ADO_Manager
    {
        internal static DataTable GetAllUnApprovedAdvReqByUser(string getUserName)
        {
            return new Mkt_DataGateway().GetAllUnApprovedAdvReqByUser(getUserName);
        }

        public List<ADORequestModel> ADO_DetailList(DataTable d)
        {
            List<ADORequestModel> ADODetailList = new List<ADORequestModel>();
            DataTable dt = d;

            ADODetailList = CommonMethod.ConvertToList<ADORequestModel>(d);
            return ADODetailList;
        }

        internal static DataTable GetADO_ReqDetails(string CompanyName, string docNo)
        {
            return new Mkt_DataGateway().GetADO_ReqDetails(CompanyName, docNo);
        }

        internal static int UpdateAdvanceStatus(string companyName, string docNo, string advStatus)
        {
            return new Mkt_DataGateway().UpdateAdvanceStatus(companyName, docNo, advStatus);
        }
    }
}
