#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: TaskRegistry.cs
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
using FluentScheduler;

using Ninject;

using NZBDash.Services.HardwareMonitor.Monitors;

namespace NZBDash.Services.HardwareMonitor
{
    public class TaskRegistry : Registry
    {
        public TaskRegistry()
        {
            Schedule<CpuMonitor>().ToRunNow();
            //Schedule<NetworkMonitor>().ToRunNow();
        }
    }
    public class NinjectTaskFactory : ITaskFactory
    {
        private IKernel Kernel { get; set; }
        public NinjectTaskFactory(IKernel kernel)
        {
            Kernel = kernel;
        }

        /// <summary>
        /// Gets the task instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ITask GetTaskInstance<T>() where T : ITask
        {
            return Kernel.Get<T>();
        }
    }
}