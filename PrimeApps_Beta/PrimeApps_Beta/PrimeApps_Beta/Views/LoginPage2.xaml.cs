using PrimeApps_Beta.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeApps_Beta.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PrimeApps_Beta.Animations;

namespace PrimeApps_Beta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage2 : ContentPage
    {
        string getUserName = "";
        string getPassword = "";
        public LoginPage2(string username)
        {
            //InitializeComponent();
            this.InitializeComponent();
            this.BindingContext = this;
            this.IsBusy = false;
            if (Settings.SavedUserName != null && Settings.SavedUserName != "")
            {
                txtName.Text = Settings.SavedUserName;
                txtPassword.Text = Settings.SavedPassword;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () =>
            {
                await ViewAnimations.FadeAnimY(Logo);
                await ViewAnimations.FadeAnimY(MainStack);

            });


        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            indicatorLogin.IsRunning = true;
            this.IsBusy = true;

            //ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = true };
            getUserName = txtName.Text;
            getPassword = txtPassword.Text;


            if (getUserName != "" && getPassword != "")
            {
                try
                {
                    DataTable dt = LoginManager.getUserDetails(getUserName, getPassword);
                    if (dt.Rows.Count > 0)
                    {
                        //For Saving Login
                        Settings.SavedPassword = getPassword;

                        // Start BackGroundServices For Notification
                        MessagingCenter.Send<string>("1", "MyService");


                        string getuserName = dt.Rows[0]["username"].ToString();
                        Settings.SavedUserName = getUserName;
                        //activityIndicator = new ActivityIndicator { IsRunning = false };
                        await Navigation.PushAsync(new PrimeApps(getuserName));
                        indicatorLogin.IsRunning = false;
                        //await Navigation.PushAsync(new ItemsPage());
                    }
                    else { DisplayAlert("Login", "You've Done Something wrong boi!!", "Yahoo!!"); indicatorLogin.IsRunning = false; }
                }
                catch (Exception exc) { DisplayAlert("Error!", string.Format("The login failed. Check the error to see why. Error: {0}", exc), "ok"); indicatorLogin.IsRunning = false; }
            }
            else
            {
                DisplayAlert("Login", "You've Done Something wrong boi!!", "OOps!!");
            }
        }

        private void webApp_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("http://192.168.150.127:2300/pages/pr_login.aspx"));
        }
    }
}