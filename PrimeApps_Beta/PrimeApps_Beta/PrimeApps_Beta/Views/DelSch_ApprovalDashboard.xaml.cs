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
    public partial class DelSch_ApprovalDashboard : ContentPage
    {

        string getDocNo = "";
        string getUserName = "";
        DateTime getSchDate = DateTime.Now.Date;

        public ObservableCollection<DeliveryScheduleModel> items = new ObservableCollection<DeliveryScheduleModel>();
        DelSch_Manager DSM = new DelSch_Manager();

        public DelSch_ApprovalDashboard(string userName)
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
            DelSch_DataView.ItemsSource = items;
        }

        private void GetDataByUser()
        {
            DataTable dt = AltDO_Manager.GetAltDelRequestToApprove(getUserName);

            var DODetaillist = DSM.DelSch_DetailList(dt);

            foreach (var item in DODetaillist)
            {
                items.Add(item);
            }
        }

        private void DelSch_DataView_Refreshing(object sender, EventArgs e)
        {
            RefreshAllItem();
            DelSch_DataView.EndRefresh();
        }

        private void DelSch_DataView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (DeliveryScheduleModel)e.SelectedItem;
            //<%# Eval("DocumentNo") %>&reportDate=<%# Eval("ScheduleDate") %>&ReportName=Delivery_Schedule" target="_blank">
            getDocNo = item.DocumentNo;
            getSchDate = item.ScheduleDate;

            if (getDocNo != "" && getDocNo != null)
            {
                var pop = new DelSch_Details_Popup(getDocNo, getUserName, getSchDate);
                App.Current.MainPage.Navigation.PushPopupAsync(pop, true);
            }
        }
    }
}