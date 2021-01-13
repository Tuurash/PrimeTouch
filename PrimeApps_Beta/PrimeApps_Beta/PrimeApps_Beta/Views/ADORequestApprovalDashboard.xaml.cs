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
    public partial class ADORequestApprovalDashboard : ContentPage
    {

        string getDocNo = "";
        string getCompanyName = "";
        string getUserName = "";
        string getCurrentLevel = "";


        public ObservableCollection<ADORequestModel> items = new ObservableCollection<ADORequestModel>();
        ADO_Manager ADODM = new ADO_Manager();

        public ADORequestApprovalDashboard(string userName)
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
            ADODataview.ItemsSource = items;
        }

        private void GetDataByUser()
        {
            DataTable dt = ADO_Manager.GetAllUnApprovedAdvReqByUser(getUserName);

            var DODetaillist = ADODM.ADO_DetailList(dt);

            foreach (var item in DODetaillist)
            {
                items.Add(item);
            }
        }

        private void ADODataview_Refreshing(object sender, EventArgs e)
        {
            RefreshAllItem();
            ADODataview.EndRefresh();
        }

        private void ADODataview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (ADORequestModel)e.SelectedItem;

            getDocNo = item.AdvNO;
            getCompanyName = item.CompanyName;
            getCurrentLevel = item.ReqLevel;
            if (getDocNo != "" && getDocNo != null)
            {
                var pop = new ADO_ReqDetailPopupPage(getCompanyName, getDocNo, getUserName, getCurrentLevel);
                App.Current.MainPage.Navigation.PushPopupAsync(pop, true);
            }
        }
    }
}