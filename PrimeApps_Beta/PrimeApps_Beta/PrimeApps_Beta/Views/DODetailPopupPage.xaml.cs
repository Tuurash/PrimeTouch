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
    public partial class DODetailPopupPage : PopupPage
    {
        string CompanyName = ""; string DocNo = "";
        string LCNo = ""; string DOType = "";
        string getCurrentApprovalLevel = ""; string getNextApprovalLevel = "";
        string getUserName = ""; string getSellingType = "";

        int insertRslt = 0;
        string buyercode = "";
        string refer = "";

        public ObservableCollection<DODataModel> items = new ObservableCollection<DODataModel>();
        DO_Manager DODM = new DO_Manager();

        public DODetailPopupPage(string getCompanyName, string getDocNo, string getuserName, string getLCNo, string getDoType)
        {
            this.InitializeComponent();
            this.BindingContext = this;
            this.IsBusy = false;
            CompanyName = getCompanyName;
            DocNo = getDocNo;
            LCNo = getLCNo;
            DOType = getDoType;
            getUserName = getuserName;
        }

        protected override async void OnAppearing()
        {
            decimal TotalQnty, TotalAmount = 0;
            base.OnAppearing();
            DataTable dt = DO_Manager.GetDoToPrint(CompanyName, LCNo, DocNo);

            var doDetailList = DODM.DODetailList(dt);

            if (dt.Rows.Count > 0)
            {
                TotalQnty = decimal.Parse(dt.Compute("Sum(DoQnty)", "").ToString());
                TotalAmount = decimal.Parse(dt.Compute("Sum(DOAmount)", "").ToString());

                lblTotalAmount.Text = "PIQuantity:   " + TotalQnty.ToString("0.00") + "   PI Value: " + TotalAmount.ToString("0.00");
                lblDONo.Text = dt.Rows[0]["DONo"].ToString();
            }
            foreach (var item in doDetailList)
            {
                items.Add(item);
            }


            DOListview.ItemsSource = items;
        }

        private void btnApprove_Clicked(object sender, EventArgs e)
        {
            int approvalSet = 0;
            string currentReqLevel = "1";
            DateTime ApproveTime = DateTime.Now;
            string userIP = "";
            int DOStatus = 0;
            string status = "Approved";

            if (DOType == "ADVANCE DO")
            {
                DOStatus = ApprovalManager.UpdateAdvanceDoStatus(DocNo, LCNo, CompanyName, status);
            }
            else
            {
                DOStatus = ApprovalManager.UpdateDoStatus(DocNo, LCNo, CompanyName, status);
            }

            approvalSet = ApprovalManager.UpdateApprovalStatus(CompanyName, DocNo, currentReqLevel, getUserName, ApproveTime, userIP);

            if (approvalSet > 0)
            {
                string module = "Sales And Marketing";
                string actionDocNo = DocNo;
                string action = "DO  has been Approved by" + getUserName + ", DONo: " + DocNo + "";
                Admin_ActionLogManager.SendActionLog(CompanyName, action, module, getUserName, actionDocNo);
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



