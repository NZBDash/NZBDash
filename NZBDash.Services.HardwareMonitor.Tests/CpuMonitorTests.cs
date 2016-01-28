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

using Ploeh.AutoFixture;

namespace NZBDash.Services.HardwareMonitor.Tests
{

    [TestFixture]
    public class CpuMonitorTests
    {
        private ISettingsService<HardwareSettingsDto> Service { get; set; }
        private Mock<IEventService> EventService { get; set; }
        private Mock<ILogger> Logger { get; set; }
        private Mock<ISmtpClient> SmtpClient { get; set; }
        private HardwareSettingsDto Settings { get; set; }

        [SetUp]
        public void Setup()
        {
            Settings = new Fixture().Create<HardwareSettingsDto>();
            var mock = new Mock<ISettingsService<HardwareSettingsDto>>();
            var mockEvent = new Mock<IEventService>();
            var mockLogger = new Mock<ILogger>();
            var mockSmtp = new Mock<ISmtpClient>();

            mock.Setup(x => x.GetSettings()).Returns(Settings);
            mockEvent.Setup(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>())).Returns(1);

            Service = mock.Object;
            EventService = mockEvent;
            Logger = mockLogger;
            SmtpClient = mockSmtp;
        }

        //[Test]
        //public void TestNoBreach()
        //{
        //    var cpu = new CpuMonitor(Service, EventService.Object, Logger.Object, SmtpClient.Object)
        //    {
        //        Threshold = new ThresholdModel
        //        {
        //            TimeThresholdSec = 1,
        //            Percentage = 1
        //        }
        //    };

        //    using (var process = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
        //    {
        //        cpu.Monitor(process, false);
        //    }
        //    EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Never);
        //}
    }
}
