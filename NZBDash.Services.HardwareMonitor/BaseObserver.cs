#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: BaseObserver.cs
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
using System.Configuration;

using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Models.Settings;
using NZBDash.Services.HardwareMonitor.Interfaces;

using Configuration = NZBDash.Services.HardwareMonitor.Interfaces.Configuration;

namespace NZBDash.Services.HardwareMonitor
{
    public abstract class BaseObserver
    {
        protected BaseObserver(ILogger logger)
        {
            Logger = logger;
        }

        protected virtual int ConfigurationRefreshTime
        {
            get
            {
                var val = ConfigurationManager.AppSettings["configRefresh"];
                int retVal;
                int.TryParse(val, out retVal);
                return retVal;
            }
        }
        protected ILogger Logger { get; set; }
        protected IDisposable Subscription { get; set; }
        protected IDisposable ConfigurationSync { get; set; }
        protected IPerformanceCounter Counter { get; set; }
        protected INotifier Notifier { get; set; }
        protected IEventService EventService { get; set; }
        protected ISmtpClient SmtpClient { get; set; }
        protected IConfigurationReader ConfigurationReader { get; set; }
        protected abstract void RefreshSettings(Configuration c);
    }
}