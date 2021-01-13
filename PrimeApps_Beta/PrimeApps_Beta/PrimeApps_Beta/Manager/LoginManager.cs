using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PrimeApps_Beta.Gateway;

namespace PrimeApps_Beta.Manager
{
    class LoginManager
    {
        internal static DataTable getUserDetails(string getUserName, string getPassword)
        {
            return new LoginGateway().getUserDetails(getUserName, getPassword);
        }

        internal static DataTable getEmployeeDetails(string getUserName)
        {
            return new LoginGateway().getUserDetails(getUserName);
        }

    }
}
