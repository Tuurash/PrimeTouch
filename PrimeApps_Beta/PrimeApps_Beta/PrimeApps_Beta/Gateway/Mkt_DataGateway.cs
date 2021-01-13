using PrimeApps_Beta.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PrimeApps_Beta.Models;

namespace PrimeApps_Beta.Gateway
{
    class Mkt_DataGateway : DBConnector
    {
        internal DataTable GetPIApprovalrequest(string getUserName)
        {
            string query = "Select T1.*,TotalAdvQnty - ToBeAdjQnty as PendingAdvance from(   Select T.*, RequestTo, ISNULL(ToBeAdjQnty, 0) as ToBeAdjQnty, ISNULL(TotalAdvQnty, 0) as TotalAdvQnty from(   Select CompanyID, PINO, PIDate, BuyerCode, BuyerName, Tenor,Ref, SUM(PIQuantity) as PIQuantity, SUM(PIQuantity * Rate) as Amount From PR_PIInfo  Group By CompanyID, PINO, PIDate, BuyerCode, BuyerName, Tenor,Ref    ) as T left outer Join(Select CompanyName, DocumentNo, RequestTo From LC_MarketingApprovalLog Where DocumentName = 'PI'  And ApprovedBy IS NULL)  LC_MarketingApprovalLog ON T.CompanyID = LC_MarketingApprovalLog.CompanyName And T.PINO = LC_MarketingApprovalLog.DocumentNo left outer Join(Select PINo, BuyerCode, SUM(Quantity) as ToBeAdjQnty from PR_AdvanceDelivery Group By PINo, BuyerCode) PR_AdvanceDelivery ON T.PINO = PR_AdvanceDelivery.PINo And T.BuyerCode = PR_AdvanceDelivery.BuyerCode    left outer Join(Select BuyerCode, SUM(Quantity) as TotalAdvQnty from PR_AdvanceDelivery Total_PR_AdvanceDelivery Group By BuyerCode) Total_PR_AdvanceDelivery ON   T.BuyerCode = Total_PR_AdvanceDelivery.BuyerCode ) as T1 Where RequestTo = '" + getUserName + "'";
            return ExecuteQueryDT(query);
        }

        internal void UpdateAltDoreqStatus(string companyName, string requestNo, string lcNo, string altReqStatus)
        {
            string query = "Update PR_AltDORequest Set ReqStatus='" + altReqStatus + "' Where CompanyName='" + companyName + "'   And AltReqNo='" + requestNo + "' And LCNo='" + lcNo + "'";
            ExecuteNonQuery(query);
        }

        internal DataTable GetUnApprovedAdvAlternateReq(string getCompanyName, string getDOreqNo, string advno)
        {
            string query = @"select ShortName,t1.* from (Select T.*,FullName, Address From ( 
                             Select CompanyName, AltReqNo, ReqDate, LCNo, InvoiceNo, Quantity, ItemRate, Count, CountCode, Discount, Commission, BuyerName, BuyerCode,'ALTERNATE REQUEST' as DOCs,Ref From 
                             PR_AltDORequest Where CompanyName = '" + getCompanyName + "'   And AltReqNo = '" + getDOreqNo + "' And DoType<>'COMPENSATION DO' UNION Select CompanyName,NULL,NULL,AdvNO,NULL,Quantity,Rate,YarnCount,YarnCode,Discount,Commission,BuyerName,BuyerCode,'ADVANCE REQUESTED' as DOCs, Ref From PR_AdvanceDelivery Where AdvNO = '" + advno + "'  ) as T left outer join(Select ShortName, FullName, Address From PR_CompanyInformation) PR_CompanyInformation ON   T.CompanyName = PR_CompanyInformation.ShortName)as t1 left outer join(select ShortName, ProductCode from PR_ProductInfo) PR_ProductInfo on t1.CountCode = PR_ProductInfo.ProductCode";
            return ExecuteQueryDT(query);
        }

        internal DataTable GetAltDelRequestToApprove(string getUserName)
        {
            string query = "Select T.*,Quantity,Amount,AltReqNo,Ref,BuyerName,LCNo,InvoiceNo,DoType from (Select* From LC_MarketingApprovalLog Where DocumentName = 'ALT_DEL_REQ' And RequestTo = '" + getUserName + "' And ApprovedBy IS NULL) as T left outer join(Select CompanyName, AltReqNo, BuyerName, LCNo, InvoiceNo, Ref, DoType, SUM(Quantity) as Quantity, Sum(Quantity * ItemRate) as Amount from PR_AltDORequest Group By CompanyName, AltReqNo, BuyerName, LCNo, InvoiceNo, Ref, DoType) PR_AltDORequest ON T.CompanyName = PR_AltDORequest.CompanyName And T.DocumentNo = PR_AltDORequest.AltReqNo ";
            return ExecuteQueryDT(query);
        }

        internal int UpdateAdvanceStatus(string companyName, string docNo, string advStatus)
        {
            string query = "Update PR_AdvanceDelivery Set Status='" + advStatus + "' Where CompanyName='" + companyName + "' And AdvNo='" + docNo + "'";
            return ExecuteNonQuery(query);
        }

        internal DataTable GetADO_ReqDetails(string CompanyName, string docNo)
        {
            string query = @"select t.*,ShortName, Nullif(TotalAmount,0) as TotalAmount from (Select AdvNO,AdvDate,BuyerCode,BuyerName,YarnCode,Rate,Quantity,Rate* Quantity as TotalAmount,Discount,Value,Ref,CompanyName,Status,Tenor,PINo,'Requested Advance' as AdvPosition From PR_AdvanceDelivery 
                             Where AdvNo = '" + docNo + "' )as t left outer join(select ShortName,ProductCode from PR_ProductInfo) PR_ProductInfo on t.YarnCode=PR_ProductInfo.ProductCode";
            return ExecuteQueryDT(query);
        }

        internal DataTable GetAllUnApprovedAdvReqByUser(string getUserName)
        {
            string query = "Select T1.* From ( Select T.*,RequestTo,ApprovedBy,ReqLevel From( Select CompanyName, BuyerCode, AdvNO, PINo, AdvDate, BuyerName, SUM(Value) as Amount From PR_AdvanceDelivery Where Status = 'UnApproved' Group By CompanyName,BuyerCode,AdvNO,PINo,AdvDate,BuyerName 		) as T                 Left outer Join(Select CompanyName, DocumentNo, RequestTo, ApprovedBy, ReqLevel from LC_MarketingApprovalLog Where DocumentName = 'ADV_DEL_REQ') LC_MarketingApprovalLog ON T.CompanyName = LC_MarketingApprovalLog.CompanyName And T.AdvNO = LC_MarketingApprovalLog.DocumentNo) as T1 Where  RequestTo = '" + getUserName + "' And ApprovedBy IS NULL ";
            return ExecuteQueryDT(query);
        }

        internal int UpdateAdvanceDoStatus(string documentNo, string lcNo, string companyName, string status)
        {
            string query = "Update PR_AdvanceDODetails Set DoStatus='" + status + "' Where CompanyName='" + companyName + "' And AdvNo='" + lcNo + "' And DoNo='" + documentNo + "'";
            return ExecuteNonQuery(query);
        }

        internal int UpdateDoStatus(string documentNo, string lcNo, string companyName, string status)
        {
            string query = "Update PR_DeliveryInfo Set DoStatus='" + status + "' Where Company='" + companyName + "' And LCNoNo='" + lcNo + "' And DoNo='" + documentNo + "'";
            return ExecuteNonQuery(query);
        }

        internal DataTable GetDoToPrint(string getCompanyName, string getLCNo, object getDoNo)
        {
            string query = @"select ShortName,t.*,Nullif(Amount,0) as DOAmount from(Select *,(ItemRate*DoQnty) as Amount, DOQnty / 50 as Pkt, Right(DoNo, 4) as DocNo from PR_DeliveryInfo where Company = '" + getCompanyName + "' and LCNoNo = '" + getLCNo + "'  and DoNo = '" + getDoNo + "') as t left outer join(select ProductCode,ShortName from PR_ProductInfo) PR_ProductInfo on t.CountCode=PR_ProductInfo.ProductCode";
            return ExecuteQueryDT(query);
        }

        internal DataTable GetUnApprovedDOSummaryList(string getUserName)
        {
            string query = "Select T1.*from( Select T.*, RequestTo from( Select Company, BuyerCode, BuyerName, LCNoNo, InvoiceNo, DONo, Ref, DoType,SellingType, SUM(DoQnty) as DoQnty, SUM(DoQnty * ItemRate) as Amount From PR_DeliveryInfo Where DoStatus = 'UnApproved' Group by Company, BuyerCode, BuyerName, LCNoNo, InvoiceNo, DONo, Ref, DoType,SellingType UNION Select CompanyName, BuyerCode, BuyerName, AdvNo, '' as InvoiceNo, DONo, Ref, 'ADVANCE DO' as DoType,SellingType, SUM(DoQnty) as DoQnty, SUM(DoQnty * ItemRate) as Amount From PR_AdvanceDODetails Where DoStatus = 'UnApproved' Group by CompanyName, BuyerCode, BuyerName, AdvNo, DONo, Ref,SellingType ) as T left outer Join(Select CompanyName, DocumentNo, RequestTo From LC_MarketingApprovalLog Where DocumentName = 'DO'  And ApprovedBy IS NULL)  LC_MarketingApprovalLog ON T.Company = LC_MarketingApprovalLog.CompanyName And T.DONo = LC_MarketingApprovalLog.DocumentNo ) as T1 Where RequestTo = '" + getUserName + "'";
            return ExecuteQueryDT(query);
        }

        internal int UpdateApprovalStatus(string companyName, string docNo, string getCurrentApprovalLevel, string getUserName, DateTime approveTime, string signedPCID)
        {
            string query = "Update LC_MarketingApprovalLog Set ApprovedBy='" + getUserName + "',ApproveTime='" + approveTime + "',UserIP='" + signedPCID + "' Where CompanyName='" + companyName + "' And DocumentNo='" + docNo + "' And ReqLevel='" + getCurrentApprovalLevel + "'";
            return ExecuteNonQuery(query);
        }

        internal int UpdatePIApprvalStatus(string companyName, string docNo)
        {
            string query = "Update PR_PIInfo Set AppStatus ='Digitaly Signed' Where CompanyID='" + companyName + "' And PINo='" + docNo + "'";
            return ExecuteNonQuery(query);
        }

        internal int InsertSgnatureLOg(string companyName, string docNo, string signedBy, DateTime signedDate, string signTitle, string signedPCID, string documentName)
        {
            string query = "Insert Into Admin_DigitalSignatureLog Values ('" + companyName + "','" + documentName + "','" + docNo + "','" + signedBy + "','" + signedDate + "','" + signTitle + "','" + signedPCID + "')";
            return ExecuteNonQuery(query);
        }

        internal DataTable GetPIInfoWithAdvByPINo(string getCompanyName, string getPINo)
        {
            string query = @"select ShortName,t.* from(select PINO,PIDate,BuyerName,BuyerCode,YarnCode,Rate,PIQuantity,PIValue,Ref,CompanyID from PR_PIInfo Where PiNo = '" + getPINo + "' And CompanyID = '" + getCompanyName + "') as t left outer join (select ShortName,ProductCode from PR_ProductInfo)PR_ProductInfo on t.YarnCode=PR_ProductInfo.ProductCode";
            return ExecuteQueryDT(query);
        }

        internal DataTable getAllInqDataByUserName(string username)
        {
            string query = @"Select CompanyName+'_'+DocumentNo as CompanyInq, T1.*  From( 
                             Select T.*,InquiryDate,  SalesPerson,BuyerCode, BuyerName, InqStatus, Quantity,Amount, SellingType, ISNULL(AdvQnty, 0) as AdvQnty from( 
                             Select CompanyName, DocumentNo, ReqLevel, RequestTo from LC_MarketingApprovalLog Where RequestTo = '" + username + "' And DocumentName = 'Inquiry' And ApprovedBy IS NULL) as T left outer join(Select CompanyName,InquiryNo,SalesPerson,InqStatus,SellingType,InquiryDate,BuyerCode,BuyerName,SUM(Quantity) as Quantity,SUM(Quantity*Negorate) as Amount,SUM(AdvQnty) as AdvQnty  From View_InquiryView Group By CompanyName,InquiryNo,SalesPerson,InqStatus,SellingType,InquiryDate,BuyerCode,BuyerName ) LC_SalesInquiry ON T.CompanyName = LC_SalesInquiry.CompanyName And T.DocumentNo = LC_SalesInquiry.InquiryNo)  As T1 Where InqStatus = 'UnApproved' ";
            return ExecuteQueryDT(query);
        }

        internal DataTable getDetailByInqCompany(string companyName, string docNo, string getUserName)
        {
            string query = @"Select CompanyName+'_'+DocumentNo as CompanyInq,T1.*,ShortName,Rate*Quantity as Amount  From( Select T.*,ItemCode,ItemName, SalesPerson,BuyerCode, BuyerName, InqStatus, Quantity,Rate, SellingType, ISNULL(AdvQnty, 0) as AdvQnty from( 
                             Select CompanyName, DocumentNo, ReqLevel, RequestTo from LC_MarketingApprovalLog Where RequestTo = '" + getUserName + "' And DocumentName = 'Inquiry' And ApprovedBy IS NULL) as T left outer join(Select CompanyName, InquiryNo, SalesPerson, InqStatus, SellingType, InquiryDate, ItemCode, ItemName, Quantity, ApprovedRate - Discount + Commision as Rate, AdvQnty, BuyerCode, BuyerName  From LC_SalesInquiry) LC_SalesInquiry ON T.CompanyName = LC_SalesInquiry.CompanyName And T.DocumentNo = LC_SalesInquiry.InquiryNo)  As T1 left Outer join(select ProductName, ShortName from PR_ProductInfo)PR_ProductInfo on T1.ItemName = PR_ProductInfo.ProductName Where InqStatus = 'UnApproved' and CompanyName = '" + companyName + "' and DocumentNo = '" + docNo + "'";
            return ExecuteQueryDT(query);
        }
    }
}
