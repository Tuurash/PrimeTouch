using PrimeApps_Beta.Gateway;
using PrimeApps_Beta.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PrimeApps_Beta.Services
{
    class InquiryApprovalManager
    {
        readonly List<ApprovalDataModel> approvalData;
        DBConnector dBConnector = new DBConnector();

        internal static DataTable GetMyApprovalLevelDetails(object getUserName, string documentName)
        {
            return new InquiryApprovalGateway().GetMyApprovalLevelDetails(getUserName, documentName);
        }

        internal static DataTable GetApprovalLevelhierarchy(string companyName, string currentReqLevel, string docNo)
        {
            return new InquiryApprovalGateway().GetApprovalLevelhierarchy(companyName, currentReqLevel, docNo);
        }



        //CommonMethod for dt conversion to list
        #region ListConversionFromDt
        public static class CommonMethod
        {
            public static List<T> ConvertToList<T>(DataTable dt)
            {
                var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
                var properties = typeof(T).GetProperties();
                return dt.AsEnumerable().Select(row =>
                {
                    var objT = Activator.CreateInstance<T>();
                    foreach (var pro in properties)
                    {
                        if (columnNames.Contains(pro.Name.ToLower()))
                        {
                            try
                            {
                                pro.SetValue(objT, row[pro.Name]);
                            }
                            catch (Exception) { }
                        }
                    }
                    return objT;
                }).ToList();
            }
        }

        internal static int InsertSupersedeInquiryApprovalLog(string companyName, string documentName, string docNo, string getUserName1, DateTime reqTime, string getUserName2, string currentReqLevel, DateTime approveTime, string getUserName3, string userIP, bool digitalSign)
        {
            throw new NotImplementedException();
        }

        internal static int UpdateApprovalStatus(string companyName, string docNo, string currentReqLevel, string getUserName, DateTime approveTime, string userIP)
        {
            return new InquiryApprovalGateway().UpdateApprovalStatus(companyName, docNo, currentReqLevel, getUserName, approveTime, userIP);
        }

        internal static int InsertMarketingApprovalLog(string companyName, string documentName, string docNo, string reqTo, DateTime reqTime, string getUserName, string reqLevel, string userIP, bool digitalSign)
        {
            return new InquiryApprovalGateway().InsertMarketingApprovalLog(companyName, documentName, docNo, reqTo, reqTime, getUserName, reqLevel, userIP, digitalSign);
        }

        internal static void UpdateInquiryStatus(string docNo, string companyName, string inqAppStatus)
        {
            new InquiryApprovalGateway().UpdateInquiryStatus(docNo, companyName, inqAppStatus);
        }

        internal static void UpdateCashInquiryStatus(object documentNo, object companyName, string inqAppStatus)
        {
            new InquiryApprovalGateway().UpdateCashInquiryStatus(documentNo, companyName, inqAppStatus);
        }



        //public List<ApprovalDataModel> inqDetailList(DataTable d)
        //{
        //    List<ApprovalDataModel> InquiryDetail = new List<ApprovalDataModel>();
        //    DataTable dt = d;
        //    InquiryDetail = CommonMethod.ConvertToList<ApprovalDataModel>(d);
        //    return InquiryDetail;
        //}

        #endregion
    }
}
