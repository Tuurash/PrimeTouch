using System;
using PrimeApps_Beta.Models;
using PrimeApps_Beta.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using PrimeApps_Beta.Manager;

using System.Data;
using System.Collections.ObjectModel;

namespace PrimeApps_Beta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApprovalDashboard : ContentPage
    {

        string getDocNo = "";
        string getCompanyName = "";
        string getUserName = "";

        public ObservableCollection<ApprovalDataModel> items = new ObservableCollection<ApprovalDataModel>();

        ApprovalDataManager ADM = new ApprovalDataManager();

        public ApprovalDashboard(string userName)
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
            InquiryDataView.ItemsSource = items;
            //InquiryDataView.IsRefreshing = false;

        }

        public void GetDataByUser()
        {
            DataTable dt = ApprovalDataManager.getAllDataByUserName(getUserName);

            var inqDetaillist = ADM.inqDetailList(dt);

            foreach (var item in inqDetaillist)
            {
                items.Add(item);
            }
        }

        private void InquiryDataView_Refreshing(object sender, EventArgs e)
        {
            RefreshAllItem();
            InquiryDataView.EndRefresh();
        }

        private void InquiryDataView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //string str = ((Button)sender).BindingContext as string;
            InquiryDataView.BackgroundColor = Color.Transparent;
            var item = (ApprovalDataModel)e.SelectedItem;

            getDocNo = item.DocumentNo;
            getCompanyName = item.CompanyName;
            //char v = '_';
            //string[] Data = str.Split(v);

            //getDocNo = Data[1];
            //getCompanyName = Data[0];

            if (getDocNo != "" && getDocNo != null)
            {
                var pop = new InquiryDetail(getCompanyName, getDocNo, getUserName);
                App.Current.MainPage.Navigation.PushPopupAsync(pop, true);
                //await PopupNavigation.PushAsync(new InquiryDetail());
            }
        }

    }
}