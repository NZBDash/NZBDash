#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: CpuMonitorTest.cs
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
using NZBDash.Services.HardwareMonitor.Monitors;

using Ploeh.AutoFixture;

namespace NZBDash.Services.HardwareMonitor.Tests
{
    [Ignore]
    [TestFixture]
    public class CpuMonitorTest 
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

        [Test]
        public void TestNoBreach()
        {
            var cpu = new CpuMonitor(Service, EventService.Object, Logger.Object, SmtpClient.Object)
            {
                TimeThresholdSec = 1,
                ThresholdPercentage = 1
            };

            using (var process = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
            {
                cpu.Monitor(process, false);
            }
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Never);
        }

        [Test]
        public void TestStartBreach()
        {
            var mock = new Mock<ISettingsService<HardwareSettingsDto>>();
            Settings.EmailAlertSettings.AlertOnBreach = true;
            Settings.EmailAlertSettings.RecipientAddress = "google@gmail.com";
            mock.Setup(x => x.GetSettings()).Returns(Settings);

            var cpu = new CpuMonitor(mock.Object, EventService.Object, Logger.Object, SmtpClient.Object)
            {
                TimeThresholdSec = 1,
                ThresholdPercentage = 1,
                ThresholdBreachCount = 2,
            };

            using (var process = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
            {
                cpu.Monitor(process, true);
            }

            Assert.That(cpu.BreachStart.Hour, Is.EqualTo(DateTime.Now.Hour));
            Assert.That(cpu.BreachStart.Minute, Is.EqualTo(DateTime.Now.Minute));
            EventService.Verify(x => x.RecordEvent(It.Is<MonitoringEventsDto>(dto => dto.EventType == EventTypeDto.Start)), Times.Once);
        }

        [Test]
        public void TestEndBreach()
        {
            var mock = new Mock<ISettingsService<HardwareSettingsDto>>();
            Settings.EmailAlertSettings.AlertOnBreach = false;
            Settings.EmailAlertSettings.AlertOnBreachEnd = true;
            mock.Setup(x => x.GetSettings()).Returns(Settings);

            var cpu = new CpuMonitor(Service, EventService.Object, Logger.Object, SmtpClient.Object)
            {
                TimeThresholdSec = 1,
                ThresholdPercentage = 1,
                ThresholdBreachCount = 2,
            };

            using (var process = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
            {
                cpu.Monitor(process, true);

                EventService.Verify(x => x.RecordEvent(It.Is<MonitoringEventsDto>(dto => dto.EventType == EventTypeDto.End)), Times.Never);

                cpu.ThresholdBreachCount = 0;

                cpu.Monitor(process, true);
            }


            Assert.That(cpu.BreachEnd.Hour, Is.EqualTo(DateTime.Now.Hour));
            Assert.That(cpu.BreachEnd.Minute, Is.EqualTo(DateTime.Now.Minute));
            EventService.Verify(x => x.RecordEvent(It.Is<MonitoringEventsDto>(dto => dto.EventType == EventTypeDto.Start)), Times.Never);
            EventService.Verify(x => x.RecordEvent(It.Is<MonitoringEventsDto>(dto => dto.EventType == EventTypeDto.End)), Times.Once);
        }

        [Test]
        public void TestResetBreach()
        {
            var mock = new Mock<ISettingsService<HardwareSettingsDto>>();
            Settings.EmailAlertSettings.AlertOnBreach = false;
            Settings.EmailAlertSettings.AlertOnBreachEnd = false;

            mock.Setup(x => x.GetSettings()).Returns(Settings);

            var cpu = new CpuMonitor(Service, EventService.Object, Logger.Object, SmtpClient.Object)
            {
                TimeThresholdSec = 10,
                ThresholdPercentage = 999,
                ThresholdBreachCount = 1,
            };

            using (var process = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
            {
                cpu.Monitor(process, true);
            }

            Assert.That(cpu.ThresholdBreachCount, Is.EqualTo(0));
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Never);
        }
    }
}
