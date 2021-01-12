using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PrimeApps_Beta.Gateway
{
    class LoginGateway : DBConnector
    {
        internal DataTable getUserDetails(string getUserName, string getPassword)
        {
            string query = @"select * from PR_UserInformation where username='" + getUserName + "' and password='" + getPassword + "'";
            return ExecuteQueryDT(query);
        }
    }
}
