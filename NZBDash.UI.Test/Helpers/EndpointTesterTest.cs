#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: EndpointTesterTest.cs
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
using System;

using Moq;

using NUnit.Framework;

using NZBDash.Common;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.ThirdParty.Api.Models.Api;
using NZBDash.ThirdParty.Api.Models.Api.Sonarr;
using NZBDash.UI.Helpers;

using Ploeh.AutoFixture;

namespace NZBDash.UI.Test.Helpers
{
    [TestFixture]
    public class EndpointTesterTest
    {
        private Mock<IThirdPartyService> ThirdPartyServiceMock { get; set; }
        private EndpointTester Tester { get; set; }
        private NzbGetStatus NzbGetStatus { get; set; }
        private SonarrSystemStatus SonarrSystemStatus { get; set; }
        private PlexServers PlexServers { get; set; }
        private SabNzbdQueue SabNzbdQueue { get; set; }
        private CouchPotatoStatus CouchPotatoStatus { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mockThirdPartyService = new Mock<IThirdPartyService>();
            var f = new Fixture();
            NzbGetStatus = f.Create<NzbGetStatus>();
            SonarrSystemStatus = f.Create<SonarrSystemStatus>();
            PlexServers = f.Create<PlexServers>();
            SabNzbdQueue = f.Create<SabNzbdQueue>();
            CouchPotatoStatus = f.Build<CouchPotatoStatus>().With(x => x.success, true).Create();

            mockThirdPartyService.Setup(x => x.GetNzbGetStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(NzbGetStatus);
            mockThirdPartyService.Setup(x => x.GetSonarrSystemStatus(It.IsAny<string>(), It.IsAny<string>())).Returns(SonarrSystemStatus);
            mockThirdPartyService.Setup(x => x.GetPlexServers(It.IsAny<string>())).Returns(PlexServers);
            mockThirdPartyService.Setup(x => x.GetCouchPotatoStatus(It.IsAny<string>(), It.IsAny<string>())).Returns(CouchPotatoStatus);
            mockThirdPartyService.Setup(x => x.GetSabNzbdQueue(It.IsAny<string>(), It.IsAny<string>())).Returns(SabNzbdQueue);


            ThirdPartyServiceMock = mockThirdPartyService;
            Tester = new EndpointTester(ThirdPartyServiceMock.Object);
        }

        [Test]
        public void NzbGetConnection()
        {
            var result = Tester.TestApplicationConnectivity(Applications.NzbGet, "a", "a", "a", "a");
            Assert.That(result, Is.True);
        }

        [Test]
        public void GetSonarrSystemStatus()
        {
            var result = Tester.TestApplicationConnectivity(Applications.Sonarr, "a", "a", "a", "a");
            Assert.That(result, Is.True);
        }

        [Test]
        public void GetPlexServers()
        {
            var result = Tester.TestApplicationConnectivity(Applications.Plex, "a", "a", "a", "a");
            Assert.That(result, Is.True);
        }

        [Test]
        public void GetCouchPotatoStatus()
        {
            var result = Tester.TestApplicationConnectivity(Applications.CouchPotato, "a", "a", "a", "a");
            Assert.That(result, Is.True);
        }

        [Test]
        public void GetSabNzbStatus()
        {
            var result = Tester.TestApplicationConnectivity(Applications.SabNZB, "a", "a", "a", "a");
            Assert.That(result, Is.True);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetSickbeardStatus()
        {
            var result = Tester.TestApplicationConnectivity(Applications.Sickbeard, "a", "a", "a", "a");
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetKodiStatus()
        {
            var result = Tester.TestApplicationConnectivity(Applications.Kodi, "a", "a", "a", "a");
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetHeadphonesStatus()
        {
            var result = Tester.TestApplicationConnectivity(Applications.Headphones, "a", "a", "a", "a");
        }
    }
}
