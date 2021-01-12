using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeApps_Beta.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeApps_Beta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrimeApps : MasterDetailPage
    {
        string userName = "";
        NavigationPage nav = new NavigationPage();
        public PrimeApps(string getuserName)
        {

            InitializeComponent();

            //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
            userName = getuserName;
            var x = new ApprovalDashboard(getuserName);
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MasterPage.ListView.BackgroundColor = Color.Transparent;
            var item = e.SelectedItem as PrimeAppsMasterMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType, userName);
            page.Title = item.Title;

            Detail = new NavigationPage(page)
            {
                BarBackgroundColor = Color.FromHex("#9e2a2b"),
                BarTextColor = Color.FromHex("#fbfbff")
            };
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}