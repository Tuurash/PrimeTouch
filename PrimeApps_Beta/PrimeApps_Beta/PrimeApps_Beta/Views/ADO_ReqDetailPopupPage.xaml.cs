using PrimeApps_Beta.Manager;
using PrimeApps_Beta.Models;
using PrimeApps_Beta.Services;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;

using System.Collections.ObjectModel;
using System.Data;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeApps_Beta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ADO_ReqDetailPopupPage : PopupPage
    {
        string CompanyName = ""; string DocNo = "";
        string LCNo = ""; string DOType = "";
        string CurrentReqLevel = "";
        string getUserName = "";

        public ObservableCollection<ADORequestModel> items = new ObservableCollection<ADORequestModel>();
        ADO_Manager ADODM = new ADO_Manager();

        public ADO_ReqDetailPopupPage(string getCompanyName, string getDocNo, string getuserName, string getCurrentLevel)
        {
            this.InitializeComponent();
            this.BindingContext = this;
            this.IsBusy = false;
            CompanyName = getCompanyName;
            DocNo = getDocNo;
            CurrentReqLevel = getCurrentLevel;
            getUserName = getuserName;
        }

        protected override void OnAppearing()
        {
            decimal TotalQnty, TotalAmount = 0;
            base.OnAppearing();
            DataTable dt = ADO_Manager.GetADO_ReqDetails(CompanyName, DocNo);

            var doDetailList = ADODM.ADO_DetailList(dt);

            if (dt.Rows.Count > 0)
            {
                TotalQnty = decimal.Parse(dt.Compute("Sum(Quantity)", "").ToString());
                TotalAmount = decimal.Parse(dt.Compute("Sum(TotalAmount)", "").ToString());

                lblTotalAmount.Text = "Total qty:   " + TotalQnty.ToString("0.00") + "   Amount: " + TotalAmount.ToString("0.00");
                lblADOReqNo.Text = dt.Rows[0]["AdvNO"].ToString();
            }
            foreach (var item in doDetailList)
            {
                items.Add(item);
            }


            ADOReq_DetailList.ItemsSource = items;
        }

        private void btnApprove_Clicked(object sender, EventArgs e)
        {
            string documentName = "ADV_DEL_REQ";

            DateTime reqTime = DateTime.Now;
            DateTime ApproveTime = DateTime.Now;
            string userIP = "";
            bool digitalSign = true;
            int InqappReq = 0;
            int approvalSet = 0;

            string advStatus = "Approved";
            string myAppLevel = "";
            string nextAppLevel = "";
            string reqTo = "";
            string reqLevel = "";
            int insertrslt = 0;

            approvalSet = ApprovalManager.UpdateApprovalStatus(CompanyName, DocNo, CurrentReqLevel, getUserName, ApproveTime, userIP);

            DataTable appbodydt = InquiryApprovalManager.GetApprovalLevelhierarchy(CompanyName, CurrentReqLevel, documentName);


            if (appbodydt.Rows.Count > 0)
            {
                for (int x = 0; x < appbodydt.Rows.Count; x++)
                {
                    myAppLevel = appbodydt.Rows[x]["ApprovalLevel"].ToString();
                    nextAppLevel = appbodydt.Rows[x]["NextLevel"].ToString();
                    reqTo = appbodydt.Rows[x]["reqTo"].ToString();

                    reqLevel = nextAppLevel;


                    if (approvalSet > 0 && CurrentReqLevel != "3")
                    {
                        InqappReq = InquiryApprovalManager.InsertMarketingApprovalLog(CompanyName, documentName, DocNo, reqTo, reqTime, getUserName, reqLevel, userIP, digitalSign);
                    }
                    else if (approvalSet > 0 && CurrentReqLevel == "3")
                    {
                        insertrslt = ADO_Manager.UpdateAdvanceStatus(CompanyName, DocNo, advStatus);
                    }
                }
            }
            if (insertrslt != 0)
            {
                string module = "Sales And Marketing";
                string action = "Advance DO request  has been signed by " + getUserName + ", Advance No: " + DocNo + "";
                Admin_ActionLogManager.SendActionLog(CompanyName, action, module, getUserName, DocNo);
            }

            //for refreshing dashboard
            MessagingCenter.Send<App>((App)Application.Current, "OnApproval");
            //closing Popup
            ApprovalFinalizing(DocNo);
        }

        private void ApprovalFinalizing(string docNo)
        {
            DependencyService.Get<INotification>().CreateNotification("primeApps", "DO: " + docNo + " has been sent to hierarchy for approval.");

            ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = false };
            App.Current.MainPage.Navigation.PopPopupAsync(true);
        }
    }

}

