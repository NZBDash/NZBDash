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
using System.Linq.Expressions;
using System.Threading;
using System.Web.Hosting;

using FluentScheduler;

namespace NZBDash.Services.HardwareMonitor.Monitors
{
    public class CpuMonitor : ITask, IRegisteredObject
    {
        private readonly object _lock = new object();

        private int ThreasholdPercentage { get { return 20; } } // TODO Get these from settings page
        private int TimeThreasholdSec { get { return 5; } }
        private int ThreasholdBreach { get; set; }
        private DateTime BreachStart { get; set; }
        private DateTime BreachEnd { get; set; }

        public CpuMonitor()
        {
            HostingEnvironment.RegisterObject(this);
        }

        private bool ShuttingDown { get; set; }

        private void MonitorCpu()
        {
            using (var process = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
            {
                var hasBeenBreached = false;
                while (true)
                {
                    var breached = CheckBreach();
                    if (breached)
                    {
                        hasBeenBreached = true;
                        BreachStart = DateTime.Now;
                    }
                    else if (hasBeenBreached)
                    {
                        BreachEnd = DateTime.Now;
                    }

                    process.NextValue();
                    Thread.Sleep(1000);
                    var realValue = process.NextValue();
                    if (realValue >= ThreasholdPercentage)
                    {
                        ThreasholdBreach++;
                    }
                    else
                    {
                        ThreasholdBreach = 0;
                    }
                }
            }
        }

        private bool CheckBreach()
        {
            return ThreasholdBreach >= TimeThreasholdSec;
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

                MonitorCpu();
            }
        }
    }
}