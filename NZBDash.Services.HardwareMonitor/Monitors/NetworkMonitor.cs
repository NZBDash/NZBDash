#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: MonitorNetwork.cs
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

namespace NZBDash.Services.HardwareMonitor.Monitors
{
    public class NetworkMonitor : ITask, IRegisteredObject
    {
        private readonly object _lock = new object();

        public NetworkMonitor()
        {
            HostingEnvironment.RegisterObject(this);
        }

        private bool ShuttingDown { get; set; }

        private void MonitorNetwork()
        {
            while (true)
            {
                using (var process = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
                {
                    // Call this an extra time before reading its value
                    process.NextValue();

                    // We require the PC to update it self... We need to wait.
                    Thread.Sleep(1000);
                    var realValue = process.NextValue();
                    //Console.WriteLine("OI!"); // TODO Store
                }

                // TODO: We require the network card value from the Settings page.
                //var performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
                //var cn = performanceCounterCategory.GetInstanceNames();
                //var firstNetworkCard = cn[0]; 
                //var networkBytesSent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", firstNetworkCard);
                //var networkBytesReceived = new PerformanceCounter("Network Interface", "Bytes Received/sec", firstNetworkCard);
                //var networkBytesTotal = new PerformanceCounter("Network Interface", "Bytes Total/sec", firstNetworkCard);

                
                //// First counter is empty
                //Thread.Sleep(1000);

            }
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

                MonitorNetwork();
            }
        }
    }
}