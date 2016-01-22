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
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.Services.HardwareMonitor.Alert;

namespace NZBDash.Services.HardwareMonitor.Monitors
{
    public class CpuMonitor : ITask, IRegisteredObject, IMonitor
    {
        private readonly object _lock = new object();
        public int ThreasholdPercentage { get; set; }
        public int TimeThreasholdSec { get; set; }
        public int ThreasholdBreachCount { get; set; }
        public DateTime BreachStart { get; set; }
        public DateTime BreachEnd { get; set; }
        private bool ShuttingDown { get; set; }
        private bool MonitoringEnabled { get; set; }
        private ISettingsService<HardwareSettingsDto> SettingsService { get; set; }
        private HardwareSettingsDto Settings { get; set; }
        private IEventService EventService { get; set; }
        private EmailAlert EmailAlert { get; set; }

        public CpuMonitor(ISettingsService<HardwareSettingsDto> settingsService, IEventService eventService)
        {
            EventService = eventService;
            SettingsService = settingsService;

            HostingEnvironment.RegisterObject(this);
            GetThresholds();

            if (!MonitoringEnabled)
                ShuttingDown = true;
        }

        public void GetThresholds()
        {
            Settings = SettingsService.GetSettings();
            MonitoringEnabled = Settings.EmailAlertSettings.AlertOnBreach || Settings.EmailAlertSettings.AlertOnBreachEnd;
            ThreasholdPercentage = Settings.CpuMonitoringDto.CpuPercentageLimit;
            TimeThreasholdSec = Settings.CpuMonitoringDto.ThresholdTime;
        }

        public void Alert()
        {
            EmailAlert = new EmailAlert(EventService, Settings.EmailAlertSettings, BreachStart, BreachEnd);
            EmailAlert.Alert();
        }

        public void StartMonitoring()
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

        public bool Monitor(PerformanceCounter process, bool hasBeenBreached)
        {
            Console.WriteLine("Monitoring");
            var breached = CheckBreach();
            if (breached)
            {
                hasBeenBreached = true;
                BreachStart = DateTime.Now;
                Alert();
            }
            else if (hasBeenBreached)
            {
                BreachEnd = DateTime.Now;
                EmailAlert = new EmailAlert(EventService, Settings.EmailAlertSettings, BreachStart, BreachEnd);
                EmailAlert.Alert();
            }

            process.NextValue();
            Thread.Sleep(1000);
            var currentValue = process.NextValue();
            if (currentValue >= ThreasholdPercentage)
            {
                ThreasholdBreachCount++;
            }
            else
            {
                ThreasholdBreachCount = 0;
            }

            return hasBeenBreached;
        }

        private bool CheckBreach()
        {
            return ThreasholdBreachCount >= TimeThreasholdSec;
        }

        public void Stop(bool immediate)
        {   
            lock (_lock)
            {
                ShuttingDown = true;
            }

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