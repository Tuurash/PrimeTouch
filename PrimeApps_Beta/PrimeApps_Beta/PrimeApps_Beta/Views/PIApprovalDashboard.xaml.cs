using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PrimeApps_Beta.Models;
using PrimeApps_Beta.Manager;
using System.Collections.ObjectModel;
using PrimeApps_Beta.Services;
using System.Data;

namespace PrimeApps_Beta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PIApprovalDashboard : ContentPage
    {
        string getDocNo = "";
        string getCompanyName = "";
        string getUserName = "";

        public ObservableCollection<PIDataModel> items = new ObservableCollection<PIDataModel>();

        PIDataManager PDM = new PIDataManager();

        public PIApprovalDashboard(string userName)
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
            PIDataview.ItemsSource = items;
        }

        public void GetDataByUser()
        {
            DataTable dt = PIDataManager.GetPIApprovalrequest(getUserName);

            var piDetaillist = PDM.piDetailList(dt);

            foreach (var item in piDetaillist)
            {
                items.Add(item);
            }
        }

        private void PIDataview_Refreshing(object sender, EventArgs e)
        {
            RefreshAllItem();
            PIDataview.EndRefresh();
        }

        private void PIDataview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (PIDataModel)e.SelectedItem;

            getDocNo = item.PINO;
            getCompanyName = item.CompanyID;

            if (getDocNo != "" && getDocNo != null)
            {
                var pop = new PIDetailView(getCompanyName, getDocNo, getUserName);
                App.Current.MainPage.Navigation.PushPopupAsync(pop, true);
            }
        }
    }
}