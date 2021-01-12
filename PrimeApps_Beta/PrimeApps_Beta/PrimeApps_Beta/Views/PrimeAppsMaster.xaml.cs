
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PrimeApps_Beta.Services;

namespace PrimeApps_Beta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrimeAppsMaster : ContentPage
    {
        protected string userName = "";
        public ListView ListView;


        public PrimeAppsMaster()
        {
            InitializeComponent();
            lblUserName.Text = Settings.SavedUserName;
            lblDate.Text = DateTime.Now.ToLongDateString();
            BindingContext = new PrimeAppsMasterViewModel();
            ListView = MenuItemsListView;
        }

        class PrimeAppsMasterViewModel : INotifyPropertyChanged
        {

            public ObservableCollection<PrimeAppsMasterMenuItem> MenuItems { get; set; }


            public PrimeAppsMasterViewModel()
            {

                MenuItems = new ObservableCollection<PrimeAppsMasterMenuItem>(new[]
                {
                    new PrimeAppsMasterMenuItem { Id = 0, Title = "  Home" ,ImageSrc="home.png", TargetType=typeof(HomePage) },
                    new PrimeAppsMasterMenuItem { Id = 1, Title = "  Approval Dashboard" ,ImageSrc="dashboard.png",TargetType=typeof(Tabbed) },
                    new PrimeAppsMasterMenuItem { Id = 2, Title = "  Abouts"  ,ImageSrc="heart.png" ,TargetType=typeof(AboutPage) },
                    new PrimeAppsMasterMenuItem { Id = 3, Title = "  logout"  ,ImageSrc="logout.png" ,TargetType=typeof(LoginPage2) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage2(""));
        }
    }
}