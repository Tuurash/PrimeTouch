using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PrimeApps_Beta.Gateway;
using PrimeApps_Beta.Models;
using PrimeApps_Beta.Services;

namespace PrimeApps_Beta.Manager
{
    class DelSch_Manager
    {
        internal static DataTable GetUnapprovedDelSchedule(string getUserName)
        {
            return new Mkt_DataGateway().GetUnapprovedDelSchedule(getUserName);
        }

        public List<DeliveryScheduleModel> DelSch_DetailList(DataTable d)
        {
            List<DeliveryScheduleModel> delSch_DetailList = new List<DeliveryScheduleModel>();
            DataTable dt = d;

            delSch_DetailList = CommonMethod.ConvertToList<DeliveryScheduleModel>(d);
            return delSch_DetailList;
        }

        internal static DataTable GetDatewiseDeliveryScheduleBySchNo(DateTime getreportDate, string getscheduleNo)
        {
            return new Mkt_DataGateway().GetDatewiseDeliveryScheduleBySchNo(getreportDate, getscheduleNo);
        }

        internal static void UpdateDeliverySchedule(string documentNo, DateTime scheduledate)
        {
            new Mkt_DataGateway().UpdateDeliverySchedule(documentNo, scheduledate);
        }
    }
}
