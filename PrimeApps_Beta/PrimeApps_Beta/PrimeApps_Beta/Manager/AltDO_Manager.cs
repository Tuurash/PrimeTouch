using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PrimeApps_Beta.Gateway;
using PrimeApps_Beta.Models;
using PrimeApps_Beta.Services;

namespace PrimeApps_Beta.Manager
{
    class AltDO_Manager
    {

        internal static DataTable GetAltDelRequestToApprove(string getUserName)
        {
            return new Mkt_DataGateway().GetAltDelRequestToApprove(getUserName);
        }

        public List<AltDOModel> Alt_DODetailList(DataTable d)
        {
            List<AltDOModel> Alt_DODetailList = new List<AltDOModel>();
            DataTable dt = d;

            Alt_DODetailList = CommonMethod.ConvertToList<AltDOModel>(d);
            return Alt_DODetailList;
        }

        internal static DataTable GetUnApprovedAdvAlternateReq(string getCompanyName, string getDOreqNo, string Advno)
        {
            return new Mkt_DataGateway().GetUnApprovedAdvAlternateReq(getCompanyName, getDOreqNo, Advno);
        }

        internal static void UpdateAltDoreqStatus(string companyName, string requestNo, string lcNo, string altReqStatus)
        {
            new Mkt_DataGateway().UpdateAltDoreqStatus(companyName, requestNo, lcNo, altReqStatus);
        }
    }
}
