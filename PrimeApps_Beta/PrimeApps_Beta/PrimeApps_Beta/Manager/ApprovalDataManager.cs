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
    public class ApprovalDataManager
    {
        readonly List<ApprovalDataModel> approvalData;

        internal static DataTable getAllDataByUserName(string username)
        {
            return new Mkt_DataGateway().getAllInqDataByUserName(username);
        }

        public DataTable getDetailByInqCompany(string companyName, string docNo, string getUserName)
        {
            return new Mkt_DataGateway().getDetailByInqCompany(companyName, docNo, getUserName);
        }

        public List<ApprovalDataModel> inqDetailList(DataTable d)
        {
            List<ApprovalDataModel> InquiryDetail = new List<ApprovalDataModel>();
            DataTable dt = d;
            InquiryDetail = CommonMethod.ConvertToList<ApprovalDataModel>(d);
            return InquiryDetail;
        }

        public async Task<IEnumerable<ApprovalDataModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(approvalData);
        }

    }


}
