#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: WindowsHardwareServiceTests.cs
//  Created By: Jamie Rees
// 
//  Permission is hereby granted, free of charge, to any person obtaining
//  a copy of this software and associated documentation files (the
//  "Software"), to deal in the Software without restriction, including
//  without limitation the rights to use, copy, modify, merge, publish,
//  distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to
//  the following conditions:
// 
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
// 
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//  LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//  WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ***********************************************************************
#endregion
using NUnit.Framework;

using NZBDash.Core.Interfaces;
using NZBDash.Core.Services;

namespace NZBDash.Core.Test
{
    [TestFixture]
    public class WindowsHardwareServiceTests
    {
        private IHardwareService Service { get; set; }

        [SetUp]
        public void SetUp()
        {
            Service = new WindowsHardwareService();
        }

        [Test]
        public void GetDrives()
        {
            var result = Service.GetDrives();

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetRam()
        {
            var result = Service.GetRam();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.PhysicalPercentageFilled, Is.Not.Null);
            Assert.That(result.VirtualPercentageFilled, Is.Not.Null);
        }

        [Test]
        public void GetUpTime()
        {
            var upTime = Service.GetUpTime();

            Assert.That(upTime,Is.Not.Null);
        }

        [Test]
        public void GetAvailableRam()
        {
            var process = Service.GetAvailableRam();

            Assert.That(process, Is.Not.Null);
            Assert.That(process, Is.GreaterThan(0));
        }

        [Test]
        public void GetCpuPercentage()
        {
            var process = Service.GetCpuPercentage();

            Assert.That(process, Is.Not.Null);
            Assert.That(process, Is.GreaterThan(-1));
        }

        [Test]
        public void GetNetworkInformation()
        {
            var process = Service.GetNetworkInformation();

            Assert.That(process, Is.Not.Null);
            Assert.That(process.Recieved, Is.Not.Null);
            Assert.That(process.Sent, Is.Not.Null);
            Assert.That(process.Total, Is.Not.Null);
        }
    }
}
