using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using PrimeApps_Beta.Manager;
using PrimeApps_Beta.Models;
using PrimeApps_Beta.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeApps_Beta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityList : ContentPage
    {
        string getUserName = "";

        public ObservableCollection<Admin_ActionLogModel> items = new ObservableCollection<Admin_ActionLogModel>();

        Admin_ActionLogManager ALM = new Admin_ActionLogManager();

        public ActivityList(string username)
        {
            getUserName = username;
            InitializeComponent();
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
            ActivityListview.ItemsSource = items;
            //InquiryDataView.IsRefreshing = false;
        }

        public void GetDataByUser()
        {
            DataTable dt = ALM.getAllActivityByUserName(getUserName);

            var ActionDetailList = ALM.ActionDetailList(dt);

            foreach (var item in ActionDetailList)
            {
                items.Add(item);
            }
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private void ActivityListview_Refreshing(object sender, EventArgs e)
        {
            RefreshAllItem();
            ActivityListview.EndRefresh();
        }

    }
}
