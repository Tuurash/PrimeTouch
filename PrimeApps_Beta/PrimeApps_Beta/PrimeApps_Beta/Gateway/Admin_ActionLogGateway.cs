using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PrimeApps_Beta.Gateway
{
    class Admin_ActionLogGateway : DBConnector
    {
        internal void SendActionLog(string companyName, string action, string module, string getUserName, string actionDocNo)
        {
            string query = "Insert Into Admin_ActionLog Values ('" + companyName + "','" + action + "','" + DateTime.Now + "','" + module + "','" + getUserName + "','" + actionDocNo + "')";
            ExecuteNonQuery(query);
        }

        internal DataTable getAllActivityByUserName(object getUserName)
        {
            string query = @"select * from Admin_ActionLog where UserID = '" + getUserName + "' and ActionDate > GETDATE() - 1";
            return ExecuteQueryDT(query);
        }

    }
}
