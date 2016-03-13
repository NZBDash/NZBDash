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
using System.Linq;
using System.Reactive.Linq;

using FluentScheduler;

using NZBDash.Common.Helpers;
using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Models.Settings;
using NZBDash.Services.HardwareMonitor.Interfaces;
using NZBDash.Services.Monitor.Common;
using NZBDash.Services.Monitor.Notification;

namespace NZBDash.Services.Monitor.Storage
{
    public class StorageObserver : BaseObserver, ITask, IHardwareObserver
    {
        private ISettingsService<AlertSettingsDto> SettingsService { get; set; }
        private IHardwareService Hardware { get; set; }
        private int HddId { get; set; }
        public StorageObserver(ISettingsService<AlertSettingsDto> settings, IEventService eventService, ISmtpClient client, IFile file, ILogger logger, IHardwareService hardwareService) : base(logger)
        {
            SettingsService = settings;
            EventService = eventService;
            SmtpClient = client;
            ConfigurationReader = new ConfigurationReader(SettingsService, AlertTypeDto.Hdd);
            Notifier = new Notifier(ConfigurationReader.Read().Intervals.CriticalNotification, eventService, client, file, logger, AlertTypeDto.Hdd);
            Hardware = hardwareService;
        }

        protected override void RefreshSettings(Configuration c)
        {
            var settings = SettingsService.GetSettings();
            Notifier.Email = new EmailModel { Address = settings.Address, Host = settings.EmailHost, Port = settings.Port, Username = settings.Username, Alert = settings.Alert, Password = settings.Password };

            var hdd = settings.AlertRules.FirstOrDefault(x => x.AlertType == AlertTypeDto.Hdd);
            if (hdd != null)
            {
                Enabled = hdd.Enabled;
                HddId = hdd.DriveId;
                Notifier.NotificationSettings = new NotificationSettings { PercentageLimit = hdd.Percentage, Enabled = hdd.Enabled, ThresholdTime = hdd.ThresholdTime };
            }

            Logger.Trace("Storage enabled: {0}", Enabled);
            Notifier.Interval = c.Intervals.CriticalNotification;

            Logger.Trace(settings.DumpJson().ToString());
            Logger.Trace("Settings Refreshed");

        }

        public void Start(Configuration c)
        {
            if (Notifier.StartEventSaved && !Notifier.EndEventSaved)
            {
                // Ensure we do not dispose of the Observers if they are in the middle of an alert cycle.
                return;
            }
            Subscription?.Dispose();
            ConfigurationSync?.Dispose();

            RefreshSettings(c);

            Logger.Trace("New threshold {0}", c.Thresholds.CriticalLoad);
            Logger.Trace("New interval {0}", c.Intervals.CriticalNotification);

            ConfigurationSync = Observable
            .Interval(TimeSpan.FromMinutes(ConfigurationRefreshTime)) // Refresh the settings and counters every X minutes
            .Select(i => ConfigurationReader.Read())
            .DistinctUntilChanged()
            .Subscribe(Start);

            Counter = new StoragePerformanceCounter(Hardware, HddId);
            if (Enabled)
            {
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
        }

        public void Stop()
        {
            Subscription?.Dispose();
            ConfigurationSync?.Dispose();
        }

        public void Execute()
        {
            Logger.Trace("Starting Storage Monitor");
            Start(ConfigurationReader.Read());
        }
    }
}