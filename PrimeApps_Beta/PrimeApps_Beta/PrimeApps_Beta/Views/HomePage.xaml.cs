using PrimeApps_Beta.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrimeApps_Beta.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        string UserName = "";



        public HomePage(string getUserName)
        {
            UserName = getUserName;
            InitializeComponent();

            DataTable userDt = LoginManager.getEmployeeDetails(UserName);
            if (userDt != null)
            {
                lblEmployeeName.Text = userDt.Rows[0]["EmpName"].ToString();
                lblDesignation.Text = userDt.Rows[0]["Designation"].ToString();
            }
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();

            Task.Run(async () =>
            {

            });
        }

    }
}