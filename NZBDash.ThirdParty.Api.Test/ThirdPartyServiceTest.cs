﻿#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: ThirdPartyServiceTest.cs
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
using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using NZBDash.Common.Interfaces;
using NZBDash.ThirdParty.Api.Models.Api;
using NZBDash.ThirdParty.Api.Models.Api.Sonarr;
using NZBDash.ThirdParty.Api.Service;

using Ploeh.AutoFixture;

namespace NZBDash.ThirdParty.Api.Test
{
    [TestFixture]
    public class ThirdPartyServiceTest
    {
        private Mock<ISerializer> Mock { get; set; }
        private ThirdPartyService Service { get; set; }
        private PlexServers Plex { get; set; }
        private SonarrSystemStatus SonarrSystemStatus { get; set; }
        private CouchPotatoStatus CouchPotatoStatus { get; set; }
        private NzbGetHistory NzbGetHistory { get; set; }
        private NzbGetList NzbGetList { get; set; }
        private NzbGetStatus NzbGetStatus { get; set; }
        private List<SonarrSeries> SonarrSeries { get; set; }
        private SabNzbHistory SabNzbHistory { get; set; }
        private SabNzbQueue SabNzbQueue { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mock = new Mock<ISerializer>();
            var fixture = new Fixture();
            Plex = fixture.Create<PlexServers>();
            SonarrSystemStatus = fixture.Create<SonarrSystemStatus>();
            CouchPotatoStatus = fixture.Create<CouchPotatoStatus>();
            NzbGetHistory = fixture.Create<NzbGetHistory>();
            NzbGetList = fixture.Create<NzbGetList>();
            NzbGetStatus = fixture.Create<NzbGetStatus>();
            SonarrSeries = fixture.CreateMany<SonarrSeries>().ToList();
            SabNzbHistory = fixture.Create<SabNzbHistory>();
            SabNzbQueue = fixture.Create<SabNzbQueue>();

            mock.Setup(x => x.SerializeXmlData<PlexServers>(It.IsAny<string>())).Returns(Plex);
            mock.Setup(x => x.SerializedJsonData<SonarrSystemStatus>(It.IsAny<string>())).Returns(SonarrSystemStatus);
            mock.Setup(x => x.SerializedJsonData<CouchPotatoStatus>(It.IsAny<string>())).Returns(CouchPotatoStatus);
            mock.Setup(x => x.SerializedJsonData<NzbGetHistory>(It.IsAny<string>())).Returns(NzbGetHistory);
            mock.Setup(x => x.SerializedJsonData<NzbGetList>(It.IsAny<string>())).Returns(NzbGetList);
            mock.Setup(x => x.SerializedJsonData<NzbGetStatus>(It.IsAny<string>())).Returns(NzbGetStatus);
            mock.Setup(x => x.SerializedJsonData<List<SonarrSeries>>(It.IsAny<string>())).Returns(SonarrSeries);
            mock.Setup(x => x.SerializedJsonData<SabNzbHistory>(It.IsAny<string>())).Returns(SabNzbHistory);
            mock.Setup(x => x.SerializedJsonData<SabNzbQueue>(It.IsAny<string>())).Returns(SabNzbQueue);

            Mock = mock;
            Service = new ThirdPartyService(Mock.Object);
        }

        [Test]
        public void GetPlexServers()
        {
            var result = Service.GetPlexServers("a");
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Size, Is.EqualTo(Plex.Size));
        }

        [Test]
        public void GetSonarrSystemStatus()
        {
            var result = Service.GetSonarrSystemStatus("a","api");
            Assert.That(result, Is.Not.Null);
            Assert.That(result.version, Is.EqualTo(SonarrSystemStatus.version));
        }

        [Test]
        public void GetSonarrSeries()
        {
            var result = Service.GetSonarrSeries("a", "api");
            Assert.That(result, Is.Not.Null);
            Assert.That(result[0].id, Is.EqualTo(SonarrSeries[0].id));
        }


        [Test]
        public void GetCouchPotatoStatus()
        {
            var result = Service.GetCouchPotatoStatus("a", "api");
            Assert.That(result, Is.Not.Null);
            Assert.That(result.success, Is.EqualTo(CouchPotatoStatus.success));
        }

        [Test]
        public void GetNzbGetHistory()
        {
            var result = Service.GetNzbGetHistory("a", "api","pass");
            Assert.That(result, Is.Not.Null);
            Assert.That(result.result, Is.EqualTo(NzbGetHistory.result));
        }

        [Test]
        public void GetNzbGetList()
        {
            var result = Service.GetNzbGetList("a", "api", "pass");
            Assert.That(result, Is.Not.Null);
            Assert.That(result.result, Is.EqualTo(NzbGetList.result));
        }

        [Test]
        public void GetNzbGetStatus()
        {
            var result = Service.GetNzbGetStatus("a", "api", "pass");
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.EqualTo(NzbGetStatus.Result));
        }

        [Test]
        public void GetSabNzbHistory()
        {
            var result = Service.GetSabNzbHistory("a", "api");
            Assert.That(result, Is.Not.Null);
            Assert.That(result.History, Is.EqualTo(SabNzbHistory.History));
        }

        [Test]
        public void GetSanNzbQueue()
        {
            var result = Service.GetSanNzbQueue("a", "api");
            Assert.That(result, Is.Not.Null);
            Assert.That(result.diskspace1, Is.EqualTo(SabNzbQueue.diskspace1));
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetCouchPotatoMovies()
        {
            Service.GetCouchPotatoMovies("a", "api");
        }
    }
}
