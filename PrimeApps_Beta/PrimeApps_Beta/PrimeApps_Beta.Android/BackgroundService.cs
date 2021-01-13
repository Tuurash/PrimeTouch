using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrimeApps_Beta.Droid
{
    [Service(Label = "BackgroundService")]
    public class BackgroundService : Service
    {
        bool isRunning = true;
        public override StartCommandResult onStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {

        }

    }
}