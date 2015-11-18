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

        [SetUp]
        public void SetUp()
        {
            var mock = new Mock<ISerializer>();
            var fixture = new Fixture();
            Plex = fixture.Create<PlexServers>();

            mock.Setup(x => x.SerializedJsonData<PlexServers>("a")).Returns(Plex);

            Mock = mock;
            Service = new ThirdPartyService(Mock.Object);
        }

        [Test]
        [Ignore]
        public void GetPlexServers()
        {
            var result = Service.GetPlexServers("a");
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Size, Is.EqualTo(Plex.Size));
        }
    }
}
