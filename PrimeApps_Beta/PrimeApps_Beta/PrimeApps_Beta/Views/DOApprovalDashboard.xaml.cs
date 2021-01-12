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
    public partial class DOApprovalDashboard : ContentPage
    {
        string getDocNo = "";
        string getCompanyName = "";
        string getUserName = "";

        public ObservableCollection<DODataModel> items = new ObservableCollection<DODataModel>();
        DO_Manager DODM = new DO_Manager();
        public DOApprovalDashboard(string userName)
        {
            getUserName = userName;
            InitializeComponent();
            MessagingCenter.Subscribe<App>((App)Application.Current, "OnApproval", (sender) =>
            {
                RefreshAllItem();
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            RefreshAllItem();
        }

        private void RefreshAllItem()
        {
            items.Clear();
            GetDataByUser();
            DODataview.ItemsSource = items;
        }

        private void GetDataByUser()
        {
            DataTable dt = DO_Manager.GetUnApprovedDOSummaryList(getUserName);

            var DODetaillist = DODM.DODetailList(dt);

            foreach (var item in DODetaillist)
            {
                items.Add(item);
            }
        }

        private void DODataview_Refreshing(object sender, EventArgs e)
        {
            RefreshAllItem();
            DODataview.EndRefresh();
        }

        private void DODataview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (DODataModel)e.SelectedItem;

            getDocNo = item.DONo;
            getCompanyName = item.Company;

            if (getDocNo != "" && getDocNo != null)
            {
                var pop = new PIDetailView(getCompanyName, getDocNo, getUserName);
                App.Current.MainPage.Navigation.PushPopupAsync(pop, true);
            }
        }
    }
}