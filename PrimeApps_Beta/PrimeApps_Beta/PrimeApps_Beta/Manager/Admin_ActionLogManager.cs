using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PrimeApps_Beta.Gateway;
using PrimeApps_Beta.Services;
using PrimeApps_Beta.Models;

namespace PrimeApps_Beta.Manager
{
    class Admin_ActionLogManager
    {
        internal static void SendActionLog(string companyName, string action, string module, string getUserName, string actionDocNo)
        {
            new Admin_ActionLogGateway().SendActionLog(companyName, action, module, getUserName, actionDocNo);
        }

        internal DataTable getAllActivityByUserName(object getUserName)
        {
            return new Admin_ActionLogGateway().getAllActivityByUserName(getUserName);
        }

        public List<Admin_ActionLogModel> ActionDetailList(DataTable d)
        {
            List<Admin_ActionLogModel> ActionDetailList = new List<Admin_ActionLogModel>();
            DataTable dt = d;
            ActionDetailList = CommonMethod.ConvertToList<Admin_ActionLogModel>(d);
            return ActionDetailList;
        }

    }
}
