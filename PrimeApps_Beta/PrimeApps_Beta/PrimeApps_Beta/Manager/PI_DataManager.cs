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
    class PIDataManager
    {
        readonly List<PIDataModel> approvalData;


        internal static DataTable GetPIApprovalrequest(string getUserName)
        {
            return new Mkt_DataGateway().GetPIApprovalrequest(getUserName);
        }

        internal static DataTable GetPIInfoWithAdvByPINo(string getCompanyName, string getPINo)
        {
            return new Mkt_DataGateway().GetPIInfoWithAdvByPINo(getCompanyName, getPINo);
        }

        public List<PIDataModel> piDetailList(DataTable d)
        {
            List<PIDataModel> InquiryDetail = new List<PIDataModel>();
            DataTable dt = d;

            InquiryDetail = CommonMethod.ConvertToList<PIDataModel>(d);
            return InquiryDetail;
        }

        public List<PIDetailModel> piDetailListPOP(DataTable d)
        {
            List<PIDetailModel> piDetail = new List<PIDetailModel>();
            DataTable dt = d;

            piDetail = CommonMethod.ConvertToList<PIDetailModel>(d);
            return piDetail;
        }


        public async Task<IEnumerable<PIDataModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(approvalData);
        }

    }
}
