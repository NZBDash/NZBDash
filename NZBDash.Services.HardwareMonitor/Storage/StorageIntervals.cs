using System;

using NZBDash.Core.Models.Settings;
using NZBDash.Services.HardwareMonitor.Interfaces;

namespace NZBDash.Services.Monitor.Storage
{
    public class StorageIntervals : IIntervals
    {
        public StorageIntervals(HardwareSettingsDto service)
        {
            //CriticalNotification = TimeSpan.FromSeconds(service.Drives.ThresholdTime);
        }
        public TimeSpan Measurement => TimeSpan.FromSeconds(0.1);
        public TimeSpan CriticalNotification { get; }
        public TimeSpan Notification => TimeSpan.FromSeconds(1);
    }
}
