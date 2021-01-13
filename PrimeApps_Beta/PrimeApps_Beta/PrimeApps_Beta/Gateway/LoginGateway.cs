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

        internal DataTable getUserDetails(string getUserName)
        {
            string query = @"select IDCardNo,EmpName,Designation,t.*,CompanyName from(select username, ProfileID, deptname, role, UserGroup, UserLocation from PR_UserInformation where username = '" + getUserName + "') as t left outer join(select IDCardNo, EmpName, EmployeeCode, Designation, CompanyName from PR_EmployeeInformation) PR_EmployeeInformation on t.ProfileID = PR_EmployeeInformation.EmployeeCode";
            return ExecuteQueryDT(query);
        }
    }
}
