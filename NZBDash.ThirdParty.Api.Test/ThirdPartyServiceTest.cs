using System;

using Moq;

using NUnit.Framework;

using NZBDash.Api.Models;
using NZBDash.Common.Interfaces;

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

            mock.Setup(x => x.SerializeXmlData<PlexServers>(It.IsAny<string>())).Returns(Plex);
            mock.Setup(x => x.SerializedJsonData<SonarrSystemStatus>(It.IsAny<string>())).Returns(SonarrSystemStatus);
            mock.Setup(x => x.SerializedJsonData<CouchPotatoStatus>(It.IsAny<string>())).Returns(CouchPotatoStatus);
            mock.Setup(x => x.SerializedJsonData<NzbGetHistory>(It.IsAny<string>())).Returns(NzbGetHistory);
            mock.Setup(x => x.SerializedJsonData<NzbGetList>(It.IsAny<string>())).Returns(NzbGetList);
            mock.Setup(x => x.SerializedJsonData<NzbGetStatus>(It.IsAny<string>())).Returns(NzbGetStatus);

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
        [ExpectedException(typeof(NotImplementedException))]
        public void GetCouchPotatoMovies()
        {
            Service.GetCouchPotatoMovies("a", "api");
        }
    }
}
