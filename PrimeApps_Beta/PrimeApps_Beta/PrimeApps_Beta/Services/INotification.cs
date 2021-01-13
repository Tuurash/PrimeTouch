using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeApps_Beta.Services
{
    public interface INotification
    {
        void CreateNotification(string title, string message);
    }
}
