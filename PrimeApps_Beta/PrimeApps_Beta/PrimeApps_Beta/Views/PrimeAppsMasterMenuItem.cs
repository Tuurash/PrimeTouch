using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeApps_Beta.Views
{
    public class PrimeAppsMasterMenuItem
    {
        public PrimeAppsMasterMenuItem()
        {
            TargetType = typeof(PrimeAppsMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageSrc { get; set; }
        public Type TargetType { get; set; }

    }
}