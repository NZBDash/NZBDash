#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: StorageObserver.cs
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
using System.Diagnostics;
using System.Reactive.Linq;

using FluentScheduler;

using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Models.Settings;
using NZBDash.Services.HardwareMonitor.Cpu;
using NZBDash.Services.HardwareMonitor.Interfaces;
using NZBDash.Services.HardwareMonitor.Notification;

namespace NZBDash.Services.HardwareMonitor.Storage
{
    public class StorageObserver : BaseObserver, ITask, IHardwareObserver
    {
        public StorageObserver(ISettingsService<HardwareSettingsDto> settings, IEventService eventService, ISmtpClient client, IFile file, ILogger logger) : base(logger)
        {
            SettingsService = settings;
            EventService = eventService;
            SmtpClient = client;
            ConfigurationReader = new CpuConfigurationReader(SettingsService);
            Notifier = new EmailNotifier(ConfigurationReader.Read().Intervals.CriticalNotification, eventService, client, file, logger);
        }

        protected override void RefreshSettings(Configuration c)
        {
            var settings = SettingsService.GetSettings();
            Notifier.EmailSettings = settings.EmailAlertSettings;
            Notifier.CpuSettings = settings.CpuMonitoring;
            Notifier.Interval = c.Intervals.CriticalNotification;

            Logger.Trace("Settings Refreshed");
        }

        public void Start(Configuration c)
        {
            Subscription?.Dispose();
            ConfigurationSync?.Dispose();

            RefreshSettings(c);

            Debug.WriteLine("New threshold {0}", c.Thresholds.CriticalLoad);
            Debug.WriteLine("New interval {0}", c.Intervals.CriticalNotification);

            ConfigurationSync = Observable.Interval(TimeSpan.FromMinutes(ConfigurationRefreshTime)) // Refresh the settings and counters every X minutes
                                          .Select(i => ConfigurationReader.Read()).DistinctUntilChanged().Subscribe(Start);

            Counter = new StoragePerformanceCounter();

            var alarms = Observable.Interval(c.Intervals.Measurement) // generate endless sequence of events
                                   .Select(i => Counter.Value) // convert event index to cpu load value
                                   .Select(load => load > c.Thresholds.CriticalLoad); // is critical? convert load to boolean

            Subscription = alarms // here we throttle critical alarms 
                .Where(critical => critical).Sample(c.Intervals.Notification).Merge(
                    // allow critical notification no more often then ...
                    alarms // here we throttle non critical alarms
                        .Where(critical => !critical).Sample(c.Intervals.Notification)) // allow non critical notification no more often then ...
                .Subscribe(Notifier.Notify); // our action to send notifications
        }

        public void Stop()
        {
            Subscription?.Dispose();
            ConfigurationSync?.Dispose();
        }

        public void Execute()
        {
            Start(ConfigurationReader.Read());
        }
    }
}