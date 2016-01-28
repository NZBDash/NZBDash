using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject;

using NZBDash.Core.Interfaces;
using NZBDash.Core.Models.Settings;
using NZBDash.Services.HardwareMonitor.Interfaces;
using NZBDash.Services.HardwareMonitor.React;

namespace NZBDash.Services.HardwareMonitor
{
    class HardwareObserver : IService
    {
        public IDisposable Subscription { get; set; }
        
        private IIntervals CpuIntervals { get; set; }
        private IPerformanceCounter CpuCounter { get; set; }
        private IThresholds CpuThresholds { get; set; }
        private INotifier Notifier { get; set; }
        public void Start()
        {
            var kernel = ServiceKernel.GetKernel();
            var settingsService = kernel.Get<ISettingsService<HardwareSettingsDto>>();

            CpuIntervals = new CpuIntervals(settingsService);
            CpuCounter = new CpuPerformanceCounter();
            CpuThresholds = new CpuThreshold(settingsService);
            Notifier = new EmailNotifier(CpuIntervals.CriticalNotification);

            var alarms = Observable
                .Interval(CpuIntervals.Measurement)  // generate endless sequence of events
                .Select(i => CpuCounter.Value) // convert event index to cpu load value
                .Select(load => load > CpuThresholds.CriticalLoad); // is critical? convert load to boolean

            var settingsRefresh = Observable.Interval(TimeSpan.FromSeconds(20)).Select(x => settingsService);

            Observable.Merge(settingsRefresh).Subscribe(RefreshSettings);

            Subscription = Observable.Merge(
            alarms // here we throttle critical alarms 
                .Where(critical => critical)
                .Sample(CpuIntervals.Notification), // allow critical notification no more often then ...
            alarms // here we throttle non critical alarms
                .Where(critical => !critical)
                .Sample(CpuIntervals.Notification)) // allow non critical notification no more often then ...
            .Subscribe(Notifier.Notify); // our action to send notifications
        }

        private void RefreshSettings(ISettingsService<HardwareSettingsDto> settings)
        {
            CpuIntervals = new CpuIntervals(settings);
            CpuThresholds = new CpuThreshold(settings);
        }

        public void Stop()
        {
            Subscription.Dispose();
        }
    }
}
