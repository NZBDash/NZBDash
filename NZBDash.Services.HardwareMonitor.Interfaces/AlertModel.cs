using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZBDash.Services.HardwareMonitor.Interfaces
{
    public class AlertModel
    {
        public DateTime BreachEnd { get; set; }
        public DateTime BreachStart { get; set; }
        public int Percentage { get; set; }
        public int TimeThresholdSec { get; set; }
        public TimeSpan Duration { get { return BreachEnd - BreachStart; } }
    }
}
