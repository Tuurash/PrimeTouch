using PrimeApps_Beta.Manager;
using PrimeApps_Beta.Models;
using PrimeApps_Beta.Services;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeApps_Beta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Alt_DODetailsPopup : PopupPage
    {
        string CompanyName = ""; string DocNo = ""; string InvoiceNo = "";
        string LCNo = ""; string DOType = "";
        string CurrentReqLevel = "";
        string getUserName = "";

        public ObservableCollection<AltDOModel> items = new ObservableCollection<AltDOModel>();
        AltDO_Manager Alt_DODM = new AltDO_Manager();

        public Alt_DODetailsPopup(string getCompanyName, string getDocNo, string getuserName, string getCurrentLevel, string getLCNo, string getInvoiceNo, string getDoType)
        {
            this.InitializeComponent();
            this.BindingContext = this;
            this.IsBusy = false;
            CompanyName = getCompanyName;
            DocNo = getDocNo;
            CurrentReqLevel = getCurrentLevel;
            getUserName = getuserName;
            LCNo = getLCNo;
            DOType = getDoType;
            InvoiceNo = getInvoiceNo;
        }

        protected override void OnAppearing()
        {
            decimal TotalQnty, TotalAmount = 0;
            base.OnAppearing();
            DataTable dt = AltDO_Manager.GetUnApprovedAdvAlternateReq(CompanyName, DocNo, LCNo);

            var Alt_doDetailList = Alt_DODM.Alt_DODetailList(dt);

            if (dt.Rows.Count > 0)
            {
                TotalQnty = decimal.Parse(dt.Compute("Sum(Quantity)", "").ToString());
                TotalAmount = decimal.Parse(dt.Compute("Sum(Amount)", "").ToString());

                lblTotalAmount.Text = "Total qty:   " + TotalQnty.ToString("0.00") + "   Amount: " + TotalAmount.ToString("0.00");
                lblAlt_DOReqNo.Text = dt.Rows[0]["AdvNO"].ToString();
            }
            foreach (var item in Alt_doDetailList)
            {
                items.Add(item);
            }
            Alt_DOReqlist.ItemsSource = items;
        }
        private void btnApprove_Clicked(object sender, EventArgs e)
        {
            string documentName = "ALT_DEL_REQ";
            string reqTo = "";
            string reqLevel = "";
            DateTime ApproveTime = DateTime.Now;
            string userIP = "";
            DateTime reqTime = DateTime.Now;
            bool digitalSign = true;
            string salesPerson = "";
            string appBodyName = "";
            int approvalSet = 0;
            int InqappReq = 0;
            string altReqStatus = "Approved";

            string myAppLevel = "";
            string nextAppLevel = "";



            DataTable appbodydt = InquiryApprovalManager.GetApprovalLevelhierarchy(CompanyName, CurrentReqLevel, documentName);

            if (appbodydt.Rows.Count > 0)
            {
                for (int x = 0; x < appbodydt.Rows.Count; x++)
                {
                    myAppLevel = appbodydt.Rows[x]["ApprovalLevel"].ToString();
                    nextAppLevel = appbodydt.Rows[x]["NextLevel"].ToString();

                    appBodyName = appbodydt.Rows[x]["nextAppBody"].ToString();

                    if (appBodyName == "Sales Person")
                    {
                        reqTo = salesPerson;
                    }
                    else
                    {
                        reqTo = appbodydt.Rows[x]["reqTo"].ToString();
                    }

                    reqLevel = nextAppLevel;


                    approvalSet = ApprovalManager.UpdateApprovalStatus(CompanyName, DocNo, CurrentReqLevel, getUserName, ApproveTime, userIP);
                    if (approvalSet > 0 && CurrentReqLevel != "2")
                    {
                        InqappReq = InquiryApprovalManager.InsertMarketingApprovalLog(CompanyName, documentName, DocNo, reqTo, reqTime, getUserName, reqLevel, userIP, digitalSign);
                    }
                    else if (approvalSet > 0 && CurrentReqLevel == "2")
                    {
                        AltDO_Manager.UpdateAltDoreqStatus(CompanyName, DocNo, LCNo, altReqStatus);
                    }
                }
            }
            string module = "Sales and Marketing";
            string action = "Alternate DO request  has been signed by " + getUserName + ", requestNo: " + DocNo + "";
            Admin_ActionLogManager.SendActionLog(CompanyName, action, module, getUserName, DocNo);

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
