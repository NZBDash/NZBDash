#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: CpuObserver.cs
//   Created By: Jamie Rees
//  
//   Permission is hereby granted, free of charge, to any person obtaining
//   a copy of this software and associated documentation files (the
//   "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish,
//   distribute, sublicense, and/or sell copies of the Software, and to
//   permit persons to whom the Software is furnished to do so, subject to
//   the following conditions:
//  
//   The above copyright notice and this permission notice shall be
//   included in all copies or substantial portions of the Software.
//  
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//   EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//   LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//   OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//   WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ************************************************************************/
#endregion
using System;
using System.Reactive.Linq;

using FluentScheduler;

using NZBDash.Core.Interfaces;
using NZBDash.Core.Models.Settings;
using NZBDash.Services.HardwareMonitor.Interfaces;
using NZBDash.Services.HardwareMonitor.Notification;

namespace NZBDash.Services.HardwareMonitor.Cpu
{
    public class CpuObserver : IService, ITask
    {
        private IDisposable Subscription { get; set; }
        private IDisposable SettingsRefresh { get; set; }
        private IPerformanceCounter CpuCounter { get; set; }
        private IIntervals CpuIntervals { get; set; }
        private IThresholds CpuThresholds { get; set; }
        private INotifier Notifier { get; set; }
        private ISettingsService<HardwareSettingsDto> SettingsService { get; set; }
        private IEventService EventService { get; set; }
        private ISmtpClient SmtpClient { get; set; }

        public CpuObserver(ISettingsService<HardwareSettingsDto> settings, IEventService eventService, ISmtpClient client)
        {
            SettingsService = settings;
            EventService = eventService;
            SmtpClient = client;
        }

        private void RefreshSettings(long a = default(long))
        {
            var settings = SettingsService.GetSettings();
            CpuIntervals = new CpuIntervals(settings);
            CpuThresholds = new CpuThreshold(settings);
            Notifier = new EmailNotifier(CpuIntervals.CriticalNotification, EventService, settings.CpuMonitoring, settings.EmailAlertSettings, SmtpClient);
            Console.WriteLine("Settings Refreshed");
            Console.WriteLine("New threshold {0}",CpuThresholds.CriticalLoad);
            Console.WriteLine("New interval {0}", CpuIntervals.CriticalNotification);
        }

        public void Start()
        {
            RefreshSettings();
            
            CpuCounter = new CpuPerformanceCounter();
            
            var alarms = Observable.Interval(CpuIntervals.Measurement) // generate endless sequence of events
                                   .Select(i => CpuCounter.Value) // convert event index to cpu load value
                                   .Select(load => load > CpuThresholds.CriticalLoad); // is critical? convert load to boolean

            var settingsRefresh = Observable.Interval(TimeSpan.FromSeconds(20));

            SettingsRefresh = Observable.Merge(settingsRefresh).Subscribe(RefreshSettings);

            Subscription = alarms // here we throttle critical alarms 
                .Where(critical => critical).Sample(CpuIntervals.Notification).Merge(
                    // allow critical notification no more often then ...
                    alarms // here we throttle non critical alarms
                        .Where(critical => !critical).Sample(CpuIntervals.Notification)) // allow non critical notification no more often then ...
                .Subscribe(Notifier.Notify); // our action to send notifications
        }

        public void Stop()
        {
            Subscription.Dispose();
            SettingsRefresh.Dispose();
        }

        public void Execute()
        {
            Start();
        }
    }
}