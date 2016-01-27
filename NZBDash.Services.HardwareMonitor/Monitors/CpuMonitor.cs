#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: CpuMonitor.cs
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
using System.Threading;
using System.Web.Hosting;

using FluentScheduler;

using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Models.Settings;
using NZBDash.Services.HardwareMonitor.Alert;
using NZBDash.Services.HardwareMonitor.Interfaces;

namespace NZBDash.Services.HardwareMonitor.Monitors
{
    public class CpuMonitor : ITask, IRegisteredObject, IMonitor
    {
        private readonly object _lock = new object();

        public CpuMonitor(ISettingsService<HardwareSettingsDto> settingsService, IEventService eventService, ILogger logger, ISmtpClient client)
        {
            Threshold = new ThresholdModel();
            Logger = logger;
            SmtpClient = client;
            EventService = eventService;
            SettingsService = settingsService;

            HostingEnvironment.RegisterObject(this);
            GetThresholds();

            if (!MonitoringEnabled)
            {
                Logger.Info("CPU Monitor is not enabled. Shutting down.");
                ShuttingDown = true;
            }
        }

        private EmailAlert EmailAlert { get; set; }
        private IEventService EventService { get; set; }
        private ILogger Logger { get; set; }
        private bool MonitoringEnabled { get; set; }
        private HardwareSettingsDto Settings { get; set; }
        private ISettingsService<HardwareSettingsDto> SettingsService { get; set; }
        private bool ShuttingDown { get; set; }
        private ISmtpClient SmtpClient { get; set; }

        public ThresholdModel Threshold { get; set; }

        public bool Monitor(PerformanceCounter process, bool hasBeenBreached)
        {
            Logger.Trace("Monitoring... ");
            var breached = Threshold.HasBreached;
            if (breached)
            {
                Logger.Trace(string.Format("We have an CPU breach at {0}", DateTime.Now));
                hasBeenBreached = true;
                Threshold.BreachStart = DateTime.Now;
                Alert();
            }
            else if (hasBeenBreached)
            {
                Logger.Trace(string.Format("We have an ended a CPU breach at {0}", DateTime.Now));
                Threshold.BreachEnd = DateTime.Now;
                EmailAlert = new EmailAlert(EventService, Logger, SmtpClient, Settings.EmailAlertSettings, Threshold);
                EmailAlert.Alert();
                hasBeenBreached = false;
            }

            process.NextValue();
            Thread.Sleep(1000);
            var currentValue = process.NextValue();
            if (currentValue >= Threshold.Percentage)
            {
                Threshold.BreachCount++;
            }
            else
            {
                Threshold.BreachCount = 0;
            }

            return hasBeenBreached;
        }

        public void GetThresholds()
        {
            Logger.Trace("Settings all thresholds");
            Settings = SettingsService.GetSettings();
            MonitoringEnabled = Settings.EmailAlertSettings.AlertOnBreach || Settings.EmailAlertSettings.AlertOnBreachEnd;
            Threshold.Percentage = Settings.CpuMonitoring.CpuPercentageLimit;
            Threshold.TimeThresholdSec = Settings.CpuMonitoring.ThresholdTime;

            Logger.Trace("Monitoring Enabled Settings: " + MonitoringEnabled);
            Logger.Trace("Percentage Settings: " + Settings.CpuMonitoring.CpuPercentageLimit);
            Logger.Trace("TimeThresholdSec Settings: " + Settings.CpuMonitoring.ThresholdTime);
        }

        public void Alert()
        {
            EmailAlert = new EmailAlert(EventService, Logger, SmtpClient, Settings.EmailAlertSettings, Threshold);
            EmailAlert.Alert();
        }

        public void StartMonitoring()
        {
            Logger.Info("Starting CPU Monitor");
            try
            {
                using (var process = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
                {
                    var hasBeenBreached = false;
                    while (true)
                    {
                        hasBeenBreached = Monitor(process, hasBeenBreached);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Fatal(e);
                Stop();

                //TODO: We need to possibly restart the service.
            }
        }

        public void Stop(bool immediate = false)
        {
            lock (_lock)
            {
                ShuttingDown = true;
            }
            Logger.Info("Stopping CPU Monitor");
            HostingEnvironment.UnregisterObject(this);
        }

        public void Execute()
        {
            lock (_lock)
            {
                if (ShuttingDown)
                {
                    return;
                }

                StartMonitoring();
            }
        }
    }
}