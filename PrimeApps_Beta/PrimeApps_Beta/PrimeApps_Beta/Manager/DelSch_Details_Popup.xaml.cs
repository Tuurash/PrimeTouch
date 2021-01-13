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

namespace PrimeApps_Beta.Manager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DelSch_Details_Popup : PopupPage
    {

        string DocNo = "";
        string getUserName = "";
        DateTime SchDate = DateTime.Now.Date;

        public ObservableCollection<DeliveryScheduleModel> items = new ObservableCollection<DeliveryScheduleModel>();
        DelSch_Manager DSM = new DelSch_Manager();

        public DelSch_Details_Popup(string getDocNo, string getuserName, DateTime getSchDate)
        {
            this.InitializeComponent();
            this.BindingContext = this;
            this.IsBusy = false;
            SchDate = getSchDate;
            DocNo = getDocNo;
            getUserName = getuserName;

        }

        protected override void OnAppearing()
        {
            decimal TotalQnty, TotalAmount = 0;
            base.OnAppearing();
            DataTable dt = DelSch_Manager.GetDatewiseDeliveryScheduleBySchNo(SchDate, DocNo);

            var Alt_doDetailList = DSM.DelSch_DetailList(dt);

            if (dt.Rows.Count > 0)
            {
                TotalQnty = decimal.Parse(dt.Compute("Sum(ScheduleQnty)", "").ToString());
                TotalAmount = decimal.Parse(dt.Compute("Sum(Amount)", "").ToString());

                lblTotalAmount.Text = "Total qty:   " + TotalQnty.ToString("0.00") + "   Amount: " + TotalAmount.ToString("0.00");
                lblDocumentNo.Text = dt.Rows[0]["DocumentNo"].ToString();
            }
            foreach (var item in Alt_doDetailList)
            {
                items.Add(item);
            }
            DelSch_DetailList.ItemsSource = items;
        }

        private void btnApprove_Clicked(object sender, EventArgs e)
        {
            int approvalSet = 0;
            string companyName = "";
            string currentReqLevel = "1";
            DateTime ApproveTime = DateTime.Now;
            string userIP = "";

            approvalSet = ApprovalManager.UpdateApprovalStatus(companyName, DocNo, currentReqLevel, getUserName, ApproveTime, userIP);

            DelSch_Manager.UpdateDeliverySchedule(DocNo, SchDate);


            if (approvalSet > 0)
            {
                string module = "Sales and Marketting";
                string action = "Delivery Schedule has been Approved by" + getUserName + ", ScheduleNo: " + DocNo + "";
                Admin_ActionLogManager.SendActionLog(companyName, action, module, getUserName, DocNo);
            }
            //for refreshing dashboard
            MessagingCenter.Send<App>((App)Application.Current, "OnApproval");
            //closing Popup
            ApprovalFinalizing(DocNo);
        }

        private void ApprovalFinalizing(string docNo)
        {
            DependencyService.Get<INotification>().CreateNotification("primeApps", "Delivery Schedule: " + docNo + " has been sent to hierarchy for approval.");

            ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = false };
            App.Current.MainPage.Navigation.PopPopupAsync(true);
        }
    }
}