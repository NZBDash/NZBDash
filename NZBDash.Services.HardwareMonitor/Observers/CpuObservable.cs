#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: MonitoringObserver.cs
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

using NZBDash.Services.HardwareMonitor.Interfaces;

namespace NZBDash.Services.HardwareMonitor.Observers
{
    public class CpuObservable : IMonitorObservable, ITask, IRegisteredObject
    {
        public event EventHandler StartAlert;
        public event EventHandler EndAlert;
        public event UnhandledExceptionEventHandler ExceptionThrown;

        private int BreachCount { get; set; }
        private bool Running { get; set; }

        private bool StartAlertSent { get; set; }

        private int CpuUsageLimit = 10;
        private int SecondsLimit = 10;
        private ThresholdModel ThresholdData { get; set; }

        public CpuObservable()
        {
            ThresholdData = new ThresholdModel { Percentage = CpuUsageLimit, TimeThresholdSec = SecondsLimit };
        }
        public void StartMonitoring()
        {
            try
            {
                using (var process = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
                {
                    while (true)
                    {

                        process.NextValue();
                        Thread.Sleep(1000);
                        var currentValue = process.NextValue();

                        if (currentValue >= ThresholdData.Percentage)
                        {
                            BreachCount++;
                        }
                        else
                        {
                            Console.WriteLine("Reset");
                            BreachCount = 0;
                        }

                        if (BreachCount >= ThresholdData.TimeThresholdSec && !StartAlertSent) // We have hit the limit
                        {
                            var alertHandler = StartAlert;
                            if (alertHandler != null)
                            {
                                ThresholdData.BreachStart = DateTime.Now;
                                alertHandler(ThresholdData, EventArgs.Empty);
                                StartAlertSent = true;
                            }
                        }
                        else if (StartAlertSent && BreachCount < SecondsLimit)
                        {
                            var alertHandler = EndAlert;
                            if (alertHandler != null)
                            {
                                ThresholdData.BreachEnd = DateTime.Now;
                                alertHandler(ThresholdData, EventArgs.Empty);
                                ResetAlerts();
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                var exceptionHandle = ExceptionThrown;
                if (exceptionHandle != null)
                {
                    exceptionHandle(e, new UnhandledExceptionEventArgs(e, true));
                }
                throw;
            }
        }
        private void ResetAlerts()
        {
            StartAlertSent = false;
        }

        public void Execute()
        {
            Running = true;
            StartMonitoring();
        }

        public void Stop(bool immediate)
        {
            Running = false;
        }
    }
}