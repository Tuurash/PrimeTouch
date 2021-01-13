using PrimeApps_Beta.Manager;
using PrimeApps_Beta.Models;
using Rg.Plugins.Popup.Extensions;
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
    public partial class Alt_DO_ApprovalDashboard : ContentPage
    {
        string getDocNo = "";
        string getCompanyName = "";
        string getUserName = "";
        string getCurrentLevel = "";
        string getLCNo = "";
        string getInvoiceNo = "";
        string getDoType = "";

        public ObservableCollection<AltDOModel> items = new ObservableCollection<AltDOModel>();
        AltDO_Manager ADODM = new AltDO_Manager();

        public Alt_DO_ApprovalDashboard(string userName)
        {
            getUserName = userName;
            InitializeComponent();
            MessagingCenter.Subscribe<App>((App)Application.Current, "OnApproval", (sender) =>
            {
                RefreshAllItem();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshAllItem();
        }

        private void RefreshAllItem()
        {
            items.Clear();
            GetDataByUser();
            Alt_DODataview.ItemsSource = items;
        }

        private void GetDataByUser()
        {
            DataTable dt = AltDO_Manager.GetAltDelRequestToApprove(getUserName);

            var DODetaillist = ADODM.Alt_DODetailList(dt);

            foreach (var item in DODetaillist)
            {
                items.Add(item);
            }
        }

        private void Alt_DODataview_Refreshing(object sender, EventArgs e)
        {
            RefreshAllItem();
            Alt_DODataview.EndRefresh();
        }

        private void Alt_DODataview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (AltDOModel)e.SelectedItem;
            //Eval("AltReqNo") %> &LCNo =<%# Eval("LCNo") %>&InvoiceNo=<%# Eval("InvoiceNo") %>&DoType=<%# Eval("DoType")
            getDocNo = item.AltReqNo;
            getCompanyName = item.CompanyName;
            getCurrentLevel = item.ReqLevel;
            getLCNo = item.LCNo;
            getInvoiceNo = item.InvoiceNo;
            getDoType = item.Dotype;

            if (getDocNo != "" && getDocNo != null)
            {
                var pop = new Alt_DODetailsPopup(getCompanyName, getDocNo, getUserName, getCurrentLevel, getLCNo, getInvoiceNo, getDoType);
                App.Current.MainPage.Navigation.PushPopupAsync(pop, true);
            }
        }
    }
}