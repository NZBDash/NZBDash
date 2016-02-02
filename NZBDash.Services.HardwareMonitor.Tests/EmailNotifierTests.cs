#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: CpuMonitorTests.cs
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

using Moq;

using NUnit.Framework;

using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Models;
using NZBDash.Core.Models.Settings;
using NZBDash.Services.HardwareMonitor.Interfaces;
using NZBDash.Services.HardwareMonitor.Notification;

using Ploeh.AutoFixture;

namespace NZBDash.Services.HardwareMonitor.Tests
{

    [TestFixture]
    public class EmailNotifierTests
    {

        private Mock<ISmtpClient> Smtp { get; set; }
        private Mock<IEventService> EventService { get; set; }

        [SetUp]
        public void Setup()
        {
            Smtp = new Mock<ISmtpClient>();
            EventService = new Mock<IEventService>();
            EventService.Setup(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>())).Returns(1);
        }

        [Test]
        [Ignore("Need to mock out the IFile")]
        public void TestSendStartNotification()
        {
            var n = new EmailNotifier(new TimeSpan(0, 0, 0, 1), EventService.Object, Smtp.Object)
            {
                CpuSettings = new CpuMonitoringDto { CpuPercentageLimit = 1, Enabled = true, ThresholdTime = 1 },
                EmailSettings = new EmailAlertSettingsDto { AlertOnBreach = true }
            };
            n.Notify(true);
        }
    }
}
