#region Copyright
// /************************************************************************
//   Copyright (c) 2016 Jamie Rees
//   File: CpuCounter.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using FluentScheduler;
using NZBDash.Core.Interfaces;

namespace NZBDash.UI.Helpers
{
    public class CpuCounter : ITask, IRegisteredObject
    {
        public CpuCounter(IHardwareService service)
        {
            // Register this task with the hosting environment.
            // Allows for a more graceful stop of the task, in the case of IIS shutting down.
            HostingEnvironment.RegisterObject(this);

            Service = service;
        }

        /// <summary>
        /// Gets or sets the counter.
        /// </summary>
        /// <value>
        /// The counter.
        /// </value>
        public static List<Counter> Counter = new List<Counter>();

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        private IHardwareService Service { get; set; }

        /// <summary>
        /// The _lock
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// Gets or sets a value indicating whether [_shutting down].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [_shutting down]; otherwise, <c>false</c>.
        /// </value>
        private bool ShuttingDown { get; set; }

        /// <summary>
        /// Starts the counter. The counter will only ever contain 60 items in the <see cref="List{T}" />.
        /// <para>There is an internal counter being incremented and decremented.</para>
        /// </summary>
        /// <returns></returns>
        public List<Counter> StartCounter()
        {
            var cpuCurrent = (int)Service.GetCpuPercentage();
        
            if (Counter.Count >= 60)
            {
                Counter.RemoveAt(0); 
            }
            var c = new Counter(cpuCurrent);

            Counter.Add(c);

            return Counter;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public void Execute()
        {
            lock (_lock)
            {
                if (ShuttingDown)
                    return;

                StartCounter();
            }
        }

        /// <summary>
        /// Stops the specified immediate.
        /// </summary>
        /// <param name="immediate">if set to <c>true</c> [immediate].</param>
        public void Stop(bool immediate)
        {
            // Locking here will wait for the lock in Execute to be released until this code can continue.
            lock (_lock)
            {
                ShuttingDown = true;
            }

            HostingEnvironment.UnregisterObject(this);
        }
    }

    public class Counter
    {
        public Counter(int cpuVal)
        {
            CpuVal = cpuVal;
        }

        public Int64 TimeMs
        {
            get
            {
                var d1 = new DateTime(1970, 1, 1);
                var d2 = DateTime.UtcNow.ToUniversalTime();
                var ts = new TimeSpan(d2.Ticks - d1.Ticks);

                return (Int64)ts.TotalMilliseconds;
            }
        }
        public int CpuVal { get; private set; }
    }
}

