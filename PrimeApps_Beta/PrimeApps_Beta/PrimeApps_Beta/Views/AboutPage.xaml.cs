using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeApps_Beta.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage(string userName)
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var url = "https://tuurash.github.io/";
            Device.OpenUri(new Uri(url));
        }
    }
}