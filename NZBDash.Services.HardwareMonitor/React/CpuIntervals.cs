using System;

using NZBDash.Core.Interfaces;
using NZBDash.Core.Models.Settings;
using NZBDash.Services.HardwareMonitor.Interfaces;

namespace NZBDash.Services.HardwareMonitor.React
{
    public class CpuIntervals : IIntervals
    {
        public CpuIntervals(ISettingsService<HardwareSettingsDto> service)
        {
            var settings = service.GetSettings();
            CriticalNotification = TimeSpan.FromSeconds(settings.CpuMonitoring.ThresholdTime);
        }
        public TimeSpan Measurement => TimeSpan.FromSeconds(0.1);
        public TimeSpan CriticalNotification { get; }
        public TimeSpan Notification => TimeSpan.FromSeconds(1);
    }
}
