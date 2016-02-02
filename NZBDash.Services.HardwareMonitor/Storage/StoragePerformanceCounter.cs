using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using NZBDash.Services.HardwareMonitor.Interfaces;

namespace NZBDash.Services.HardwareMonitor.Storage
{
    class StoragePerformanceCounter : IPerformanceCounter
    {
        public double Value
        {
            get
            {
                using (var process = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
                {
                    process.NextValue();
                    Thread.Sleep(1000);
                    var currentValue = process.NextValue();
                    return currentValue;
                }
            }
        }
    }
}
