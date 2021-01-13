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
    public partial class PrimeAppsDetail : TabbedPage
    {
        string getUserName = "";
        public PrimeAppsDetail()
        {
            //DateTime.Now.ToLongDateString()
            getUserName = Settings.SavedUserName;
            InitializeComponent();
            this.Children.Add(new HomePage(getUserName) { Title = "Home", IconImageSource = "home.png" });
            this.Children.Add(new ActivityList(getUserName) { Title = "Activity", IconImageSource = "dashboard.png" });
            
        }
    }
}