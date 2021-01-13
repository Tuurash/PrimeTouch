using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PrimeApps_Beta.Services;
using PrimeApps_Beta.Models;
using System.Data;
using System.Collections.ObjectModel;
using PrimeApps_Beta.Manager;

namespace PrimeApps_Beta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InquiryDetail : PopupPage
    {
        string CompanyName = ""; string DocNo = "";
        string getMyApprovalLevel = ""; string getNextApprovalLevel = "";

        string getUserName = ""; string getSellingType = "";

        public ObservableCollection<ApprovalDataModel> items = new ObservableCollection<ApprovalDataModel>();
        ApprovalDataManager ADM = new ApprovalDataManager();

        public InquiryDetail(string getCompanyName, string getDocNo, string getuserName)
        {
            this.InitializeComponent();
            this.BindingContext = this;
            this.IsBusy = false;
            CompanyName = getCompanyName;
            DocNo = getDocNo;
            getUserName = getuserName;
        }

        protected override async void OnAppearing()
        {
            decimal TotalQnty, TotalAmount = 0;
            base.OnAppearing();
            DataTable dt = ADM.getDetailByInqCompany(CompanyName, DocNo, getUserName);

            var inqDetaillist = ADM.inqDetailList(dt);

            if (dt.Rows.Count > 0)
            {
                TotalQnty = decimal.Parse(dt.Compute("Sum(Quantity)", "").ToString());
                TotalAmount = decimal.Parse(dt.Compute("Sum(Amount)", "").ToString());

                lblTotalAmount.Text = "Quantity:   " + TotalQnty.ToString("0.00") + "   Amount: " + TotalAmount.ToString("0.00");
                lblInqName.Text = dt.Rows[0]["DocumentNo"].ToString();
            }
            foreach (var item in inqDetaillist)
            {
                items.Add(item);
            }


            listView.ItemsSource = items;
        }


        private void btnClose_Clicked(object sender, EventArgs e)
        {
            //for Closing The Popoup
            App.Current.MainPage.Navigation.PopPopupAsync(true);
        }

        private void btnApprove_Clicked(object sender, EventArgs e)
        {
            indicatorApproveProgress.IsRunning = true;
            this.IsBusy = true;


            string documentName = "INQUIRY";
            string currentReqLevel = "";
            string reqTo = ""; string reqLevel = "";
            string userIP = "";
            DateTime reqTime = DateTime.Now;
            DateTime ApproveTime = DateTime.Now;
            bool digitalSign = true;
            int approvalSet = 0;
            int InqappReq = 0;
            string inqAppStatus = "Approved";
            string actionDocNo = "";
            string action = "";
            string module = "Sales & Marketting";


            //GetApprovalLevel();

            DataTable dt = ADM.getDetailByInqCompany(CompanyName, DocNo, getUserName);

            getSellingType = dt.Rows[0]["SellingType"].ToString();
            currentReqLevel = dt.Rows[0]["ReqLevel"].ToString();

            DataTable ApprovalBody = InquiryApprovalManager.GetApprovalLevelhierarchy(CompanyName, currentReqLevel, documentName);

            if (ApprovalBody.Rows.Count > 0)
            {
                for (int x = 0; x < ApprovalBody.Rows.Count; x++)
                {
                    getMyApprovalLevel = ApprovalBody.Rows[x]["ApprovalLevel"].ToString();
                    getNextApprovalLevel = ApprovalBody.Rows[x]["NextLevel"].ToString();
                    reqTo = ApprovalBody.Rows[x]["reqTo"].ToString();
                }
            }

            reqLevel = getNextApprovalLevel;

            approvalSet = InquiryApprovalManager.UpdateApprovalStatus(CompanyName, DocNo, currentReqLevel, getUserName, ApproveTime, userIP);
            if (approvalSet > 0 && currentReqLevel != "5")
            {
                InqappReq = InquiryApprovalManager.InsertMarketingApprovalLog(CompanyName, documentName, DocNo, reqTo, reqTime, getUserName, reqLevel, userIP, digitalSign);
                App.Current.MainPage.Navigation.PopPopupAsync(true);
            }
            else if (approvalSet > 0 && currentReqLevel == "5")
            {
                if (getSellingType == "LC")
                {
                    InquiryApprovalManager.UpdateInquiryStatus(DocNo, CompanyName, inqAppStatus);
                }
                else
                {
                    InquiryApprovalManager.UpdateCashInquiryStatus(DocNo, CompanyName, inqAppStatus);
                }

            }

            actionDocNo = DocNo;
            action = "Inquiry  has been signed by " + getUserName + ", Inquiry No: " + DocNo + "";
            Admin_ActionLogManager.SendActionLog(CompanyName, action, module, getUserName, actionDocNo);

            //for refreshing dashboard
            MessagingCenter.Send<App>((App)Application.Current, "OnApproval");
            //closing Popup
            ApprovalFinalizing(getMyApprovalLevel, actionDocNo);

        }

        private void ApprovalFinalizing(string getMyApprovalLevel, string actionDocNo)
        {
            if (getMyApprovalLevel == "5")
            {
                DependencyService.Get<INotification>().CreateNotification("primeApps", "INQUIRY: " + actionDocNo + " Has been Approved.");
            }
            else
            {
                DependencyService.Get<INotification>().CreateNotification("primeApps", "INQUIRY: " + actionDocNo + " has been sent to hierarchy for approval.");
            }

            ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = false };
            App.Current.MainPage.Navigation.PopPopupAsync(true);
        }

    }
}