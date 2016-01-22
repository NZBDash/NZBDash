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

using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.Services.HardwareMonitor.Monitors;

using Ploeh.AutoFixture;

namespace NZBDash.Services.HardwareMonitor.Tests
{
    [TestFixture]
    public class CpuMonitorTest
    {
        private ISettingsService<HardwareSettingsDto> Service { get; set; }
        private HardwareSettingsDto Settings { get; set; }

        [SetUp]
        public void Setup()
        {
            Settings = new Fixture().Create<HardwareSettingsDto>();
            var mock = new Mock<ISettingsService<HardwareSettingsDto>>();
            mock.Setup(x => x.GetSettings()).Returns(Settings);
            Service = mock.Object;
        }

        [Test]
        public void TestNoBreach()
        {
            var cpu = new CpuMonitor(Service)
            {
                TimeThreasholdSec = 1,
                ThreasholdPercentage = 1
            };

            using (var process = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
            {
                cpu.Monitor(process, false);
            }
        }

        [Test]
        public void TestStartBreach()
        {
            var cpu = new CpuMonitor(Service)
            {
                TimeThreasholdSec = 1,
                ThreasholdPercentage = 1,
                ThreasholdBreachCount = 2,
            };

            using (var process = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
            {
                cpu.Monitor(process, true);
            }

            Assert.That(cpu.BreachStart.Hour, Is.EqualTo(DateTime.Now.Hour));
            Assert.That(cpu.BreachStart.Minute, Is.EqualTo(DateTime.Now.Minute));
        }

        [Test]
        public void TestEndBreach()
        {
            var cpu = new CpuMonitor(Service)
            {
                TimeThreasholdSec = 1,
                ThreasholdPercentage = 1,
                ThreasholdBreachCount = 2,
            };

            using (var process = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
            {
                cpu.Monitor(process, true);

                cpu.ThreasholdBreachCount = 0;

                cpu.Monitor(process, true);
            }

            Assert.That(cpu.BreachEnd.Hour,Is.EqualTo(DateTime.Now.Hour));
            Assert.That(cpu.BreachEnd.Minute,Is.EqualTo(DateTime.Now.Minute));
        }

        [Test]
        public void TestResetBreach()
        {
            var cpu = new CpuMonitor(Service)
            {
                TimeThreasholdSec = 10,
                ThreasholdPercentage = 999,
                ThreasholdBreachCount = 1,
            };

            using (var process = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
            {
                cpu.Monitor(process, true);
            }
            
            Assert.That(cpu.ThreasholdBreachCount, Is.EqualTo(0));
        }
    }
}
