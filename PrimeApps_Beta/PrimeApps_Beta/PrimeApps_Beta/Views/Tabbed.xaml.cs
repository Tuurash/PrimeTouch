using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeApps_Beta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tabbed : TabbedPage
    {
        string getUserName = "";
        public Tabbed(string userName)
        {
            InitializeComponent();
            getUserName = userName;

            this.Children.Add(new ApprovalDashboard(userName) { Title = "Inquiry", IconImageSource = "survey.png" });
            this.Children.Add(new PIApprovalDashboard(userName) { Title = "PI", IconImageSource = "PI2.png" });
            this.Children.Add(new DOApprovalDashboard(userName) { Title = "DO", IconImageSource = "D.png" });//Delivery Order
            this.Children.Add(new ADORequestApprovalDashboard(userName) { Title = "Advance", IconImageSource = "adv.png" });
            this.Children.Add(new BlankPage() { Title = "Alt DO", IconImageSource = "advdel.png" });
            this.Children.Add(new BlankPage() { Title = "Del Schedule", IconImageSource = "delsch.png" });//Delivery Schedule Request
            this.Children.Add(new BlankPage() { Title = "Compensation", IconImageSource = "com.png" });

        }
    }
}