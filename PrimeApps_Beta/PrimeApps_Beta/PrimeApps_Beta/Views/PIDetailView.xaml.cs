using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeApps_Beta.Models;
using PrimeApps_Beta.Manager;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data;
using PrimeApps_Beta.Services;
using Rg.Plugins.Popup.Extensions;

namespace PrimeApps_Beta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PIDetailView : PopupPage
    {
        string CompanyName = ""; string DocNo = "";
        string getCurrentApprovalLevel = ""; string getNextApprovalLevel = "";
        string getUserName = ""; string getSellingType = "";

        int insertRslt = 0;
        string buyercode = "";
        string refer = "";

        public ObservableCollection<PIDetailModel> items = new ObservableCollection<PIDetailModel>();
        PIDataManager PDM = new PIDataManager();


        public PIDetailView(string getCompanyName, string getDocNo, string getuserName)
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
            DataTable dt = PIDataManager.GetPIInfoWithAdvByPINo(CompanyName, DocNo);

            var piDetailList = PDM.piDetailListPOP(dt);

            if (dt.Rows.Count > 0)
            {
                TotalQnty = decimal.Parse(dt.Compute("Sum(PIQuantity)", "").ToString());
                TotalAmount = decimal.Parse(dt.Compute("Sum(PIValue)", "").ToString());

                lblTotalAmount.Text = "PIQuantity:   " + TotalQnty.ToString("0.00") + "   PI Value: " + TotalAmount.ToString("0.00");
                lblPINo.Text = dt.Rows[0]["PINO"].ToString();
            }
            foreach (var item in piDetailList)
            {
                items.Add(item);
            }


            piListview.ItemsSource = items;
        }

        private void btnApprove_Clicked(object sender, EventArgs e)
        {
            string DocumentName = "PI";

            string SignedBy = getUserName;
            DateTime SignedDate = DateTime.Now.Date;
            string SignTitle = "";
            string SignedPCID = "";

            int insertRslt = ApprovalManager.InsertSgnatureLOg(CompanyName, DocNo, SignedBy, SignedDate, SignTitle, SignedPCID, DocumentName);

            if (insertRslt > 0)
            {
                int updateApproval = ApprovalManager.UpdatePIApprvalStatus(CompanyName, DocNo);
                getCurrentApprovalLevel = "0";
                DateTime ApproveTime = DateTime.Now.Date;
                int approvalSet = ApprovalManager.UpdateApprovalStatus(CompanyName, DocNo, getCurrentApprovalLevel, getUserName, ApproveTime, SignedPCID);

                if (approvalSet > 0)
                {
                    string module = "Sales And Marketing";
                    string action = "New PI  has been signed digitaly, PI No: " + DocNo + "";
                    Admin_ActionLogManager.SendActionLog(CompanyName, action, module, getUserName, DocNo);
                }

                //for refreshing dashboard
                MessagingCenter.Send<App>((App)Application.Current, "OnApproval");
                //closing Popup
                ApprovalFinalizing(DocNo);

            }
        }

        private void ApprovalFinalizing(string docNo)
        {
            DependencyService.Get<INotification>().CreateNotification("primeApps", "PI: " + docNo + " has been sent to hierarchy for approval.");

            ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = false };
            App.Current.MainPage.Navigation.PopPopupAsync(true);
        }

    }
}