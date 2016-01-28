#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: HardwareSettingsMapperTests.cs
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

using NUnit.Framework;
using NZBDash.Common.Mapping;
using NZBDash.Core.Models.Settings;
using NZBDash.DataAccessLayer.Models.Settings;
using Omu.ValueInjecter;
using Ploeh.AutoFixture;

namespace NZBDash.Common.Tests.Mapping
{
    [TestFixture]
    public class HardwareSettingsMapperTests
    {

        [Test]
        public void TestMap()
        {
            var entity = new Fixture().Create<HardwareSettings>();

            var m = new HardwareSettingsMapper();
            var a = m.Map(entity);

            var dto = new HardwareSettingsDto();
            var result = Mapper.Map<HardwareSettingsDto>(entity);


            Assert.That(result.NetworkMonitoring.Enabled, Is.EqualTo(entity.NetworkMonitoring.Enabled));
            Assert.That(result.NetworkMonitoring.NetworkUsagePercentage, Is.EqualTo(entity.NetworkMonitoring.NetworkUsagePercentage));
            Assert.That(result.NetworkMonitoring.NicId, Is.EqualTo(entity.NetworkMonitoring.NicId));
            Assert.That(result.NetworkMonitoring.ThresholdTime, Is.EqualTo(entity.NetworkMonitoring.ThresholdTime));
            Assert.That(result.NetworkMonitoring.Id, Is.EqualTo(entity.NetworkMonitoring.Id));
            Assert.That(result.CpuMonitoring.Enabled, Is.EqualTo(entity.CpuMonitoring.Enabled));
            Assert.That(result.CpuMonitoring.CpuPercentageLimit, Is.EqualTo(entity.CpuMonitoring.CpuPercentageLimit));
            Assert.That(result.CpuMonitoring.ThresholdTime, Is.EqualTo(entity.CpuMonitoring.ThresholdTime));
            Assert.That(result.CpuMonitoring.Id, Is.EqualTo(entity.CpuMonitoring.Id));
            Assert.That(result.EmailAlertSettings.AlertOnBreach, Is.EqualTo(entity.EmailAlertSettings.AlertOnBreach));
            Assert.That(result.EmailAlertSettings.AlertOnBreachEnd, Is.EqualTo(entity.EmailAlertSettings.AlertOnBreachEnd));
            Assert.That(result.EmailAlertSettings.EmailHost, Is.EqualTo(entity.EmailAlertSettings.EmailHost));
            Assert.That(result.EmailAlertSettings.EmailPassword, Is.EqualTo(entity.EmailAlertSettings.EmailPassword));
            Assert.That(result.EmailAlertSettings.EmailPort, Is.EqualTo(entity.EmailAlertSettings.EmailPort));
            Assert.That(result.EmailAlertSettings.EmailUsername, Is.EqualTo(entity.EmailAlertSettings.EmailUsername));
            Assert.That(result.EmailAlertSettings.RecipientAddress, Is.EqualTo(entity.EmailAlertSettings.RecipientAddress));
            Assert.That(result.EmailAlertSettings.Id, Is.EqualTo(entity.EmailAlertSettings.Id));
            Assert.That(result.Drives.Count, Is.EqualTo(entity.Drives.Count));
            Assert.That(result.Drives[0].AvailableFreeSpace, Is.EqualTo(entity.Drives[0].AvailableFreeSpace));
        }
    }
}
