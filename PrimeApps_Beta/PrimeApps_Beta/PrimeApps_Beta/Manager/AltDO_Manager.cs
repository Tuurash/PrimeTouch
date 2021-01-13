using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PrimeApps_Beta.Gateway;
using PrimeApps_Beta.Models;

namespace PrimeApps_Beta.Manager
{
    class AltDO_Manager
    {

        internal static DataTable GetAltDelRequestToApprove(string getUserName)
        {
            return new Mkt_DataGateway().GetAltDelRequestToApprove(getUserName);
        }

    }
}
