using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using PrimeApps_Beta.Gateway;
using PrimeApps_Beta.Models;
using PrimeApps_Beta.Services;


namespace PrimeApps_Beta.Manager
{
    class DO_Manager
    {
        internal static DataTable GetUnApprovedDOSummaryList(string getUserName)
        {
            return new Mkt_DataGateway().GetUnApprovedDOSummaryList(getUserName);
        }

        public List<DODataModel> DODetailList(DataTable d)
        {
            List<DODataModel> DODetailList = new List<DODataModel>();
            DataTable dt = d;

            DODetailList = CommonMethod.ConvertToList<DODataModel>(d);
            return DODetailList;
        }

        internal static DataTable GetDoToPrint(string getCompanyName, string getLCNo, object getDoNo)
        {
            return new Mkt_DataGateway().GetDoToPrint(getCompanyName, getLCNo, getDoNo);
        }

    }
}
