using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PrimeApps_Beta.Gateway
{
    class InquiryApprovalGateway : DBConnector
    {
        internal DataTable GetMyApprovalLevelDetails(object getUserName, string documentName)
        {
            string query = "Select * From PR_DocumentApprovalBody Where UserName='" + getUserName + "'  AND  Status='Active' And DocumentName='" + documentName + "' ";
            return ExecuteQueryDT(query);
        }

        internal DataTable GetApprovalLevelhierarchy(string companyName, string currentReqLevel, string docNo)
        {
            string query = " Select T.*,reqTo,nextAppBody From( Select* From PR_DocumentApprovalBody Where CompanyName='" + companyName + "' AND ApprovalLevel= '" + currentReqLevel + "'  AND Status = 'Active' And DocumentName = '" + docNo + "') as T left outer Join(Select CompanyName, DocumentName, NextLevel, ApprovalLevel, UserName as reqTo,ApprovalBody nextAppBody From PR_DocumentApprovalBody FindMyNextlevelInfo) FindMyNextlevelInfo ON T.CompanyName = FindMyNextlevelInfo.CompanyName AND T.DocumentName = FindMyNextlevelInfo.DocumentName And T.NextLevel = FindMyNextlevelInfo.ApprovalLevel";
            return ExecuteQueryDT(query);
        }

        internal int UpdateApprovalStatus(string companyName, string docNo, string currentReqLevel, string getUserName, DateTime approveTime, string userIP)
        {
            string query = "Update LC_MarketingApprovalLog Set ApprovedBy='" + getUserName + "',ApproveTime='" + approveTime + "',UserIP='" + userIP + "' Where CompanyName='" + companyName + "' And DocumentNo='" + docNo + "' And ReqLevel='" + currentReqLevel + "'";
            return ExecuteNonQuery(query);
        }

        internal int InsertMarketingApprovalLog(string companyName, string documentName, string docNo, string reqTo, DateTime reqTime, string getUserName, string reqLevel, string userIP, bool digitalSign)
        {
            string query = "Insert Into LC_MarketingApprovalLog Values ('" + companyName + "',  '" + documentName + "',  '" + docNo + "',  '" + reqTo + "',  '" + reqTime + "',  '" + getUserName + "',  '" + reqLevel + "',NULL,NULL , '" + userIP + "','" + digitalSign + "','')";
            return ExecuteNonQuery(query);
        }

        internal void UpdateInquiryStatus(string docNo, string companyName, string inqAppStatus)
        {
            string query = "Update LC_SalesInquiry Set InqStatus='" + inqAppStatus + "' Where InquiryNo='" + docNo + "' And CompanyName='" + companyName + "'";
            ExecuteQueryDT(query);
        }

        internal void UpdateCashInquiryStatus(object documentNo, object companyName, string inqAppStatus)
        {
            string query = "Update CASH_YarnSalesInquiry Set InqStatus='" + inqAppStatus + "'  Where InquiryNo='" + documentNo + "' And CompanyName='" + companyName + "'";
            ExecuteQueryDT(query);
        }
    }
}
