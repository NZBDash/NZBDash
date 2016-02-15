using System;

using NZBDash.Core.Interfaces;
using NZBDash.Core.Models.Settings;
using NZBDash.Services.HardwareMonitor.Interfaces;

namespace NZBDash.Services.HardwareMonitor.Cpu
{
    public class CpuIntervalsOld : IIntervals
    {
        public CpuIntervalsOld(HardwareSettingsDto service)
        {
            CriticalNotification = TimeSpan.FromSeconds(service.CpuMonitoring.ThresholdTime);
        }
        public TimeSpan Measurement => TimeSpan.FromSeconds(0.1);
        public TimeSpan CriticalNotification { get; }
        public TimeSpan Notification => TimeSpan.FromSeconds(1);
    }
}
