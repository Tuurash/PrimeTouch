using PrimeApps_Beta.Services;
using PrimeApps_Beta.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeApps_Beta
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";

        public App()
        {
            InitializeComponent();
            //DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new LoginPage2(""));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
