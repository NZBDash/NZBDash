using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using NZBDash.Common.Models.Data.Models.Settings;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.SettingsService;
using NZBDash.DataAccess.Interfaces;

namespace NZBDash.Core.Test.Services
{
    [TestFixture]
    public class CouchPotatoSettingsServiceTest
    {
        private Mock<IRepository<CouchPotatoSettings>> MockRepo { get; set; }
        private List<CouchPotatoSettings> ExpectedGetLinks { get; set; }
        private CouchPotatoSettings ExpectedLink { get; set; }
        private CouchPotatoSettingsService Service { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mockRepo = new Mock<IRepository<CouchPotatoSettings>>();
            ExpectedGetLinks = new List<CouchPotatoSettings> { new CouchPotatoSettings { Id = 1, Enabled = true, ApiKey = "abc", IpAddress = "192", Password = "abc", Port = 25, Username = "bvc", ShowOnDashboard = true } };
            ExpectedLink = new CouchPotatoSettings
            {
                Id = 1,
                Enabled = true,
                ApiKey = "abc",
                IpAddress = "192",
                Password = "abc",
                Port = 25,
                Username = "bvc",
                ShowOnDashboard = true
            };

            mockRepo.Setup(x => x.GetAll()).Returns(ExpectedGetLinks).Verifiable();
            mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(ExpectedGetLinks).Verifiable();

            mockRepo.Setup(x => x.Find(1)).Returns(ExpectedLink).Verifiable();
            mockRepo.Setup(x => x.FindAsync(1)).ReturnsAsync(ExpectedLink).Verifiable();

            mockRepo.Setup(x => x.Modify(It.IsAny<CouchPotatoSettings>())).Returns(1).Verifiable();
            mockRepo.Setup(x => x.ModifyAsync(It.IsAny<CouchPotatoSettings>())).ReturnsAsync(1).Verifiable();

            mockRepo.Setup(x => x.Insert(It.IsAny<CouchPotatoSettings>())).Returns(ExpectedLink).Verifiable();
            mockRepo.Setup(x => x.InsertAsync(It.IsAny<CouchPotatoSettings>())).ReturnsAsync(ExpectedLink).Verifiable();


            MockRepo = mockRepo;
            Service = new CouchPotatoSettingsService(mockRepo.Object);
        }

        [Test]
        public void GetSettings()
        {
            var result = Service.GetSettings();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(ExpectedGetLinks[0].Id));
            Assert.That(result.Enabled, Is.EqualTo(ExpectedGetLinks[0].Enabled));
            Assert.That(result.ApiKey, Is.EqualTo(ExpectedGetLinks[0].ApiKey));
            Assert.That(result.IpAddress, Is.EqualTo(ExpectedGetLinks[0].IpAddress));
            Assert.That(result.Password, Is.EqualTo(ExpectedGetLinks[0].Password));
            Assert.That(result.Username, Is.EqualTo(ExpectedGetLinks[0].Username));
            Assert.That(result.Port, Is.EqualTo(ExpectedGetLinks[0].Port));
            Assert.That(result.ShowOnDashboard, Is.EqualTo(ExpectedGetLinks[0].ShowOnDashboard));
            MockRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void InsertSettings()
        {
            var dto = new CouchPotatoSettingsDto { Id = 2 };
            var result = Service.SaveSettings(dto);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.True);

            MockRepo.Verify(x => x.Find(2), Times.Once);
            MockRepo.Verify(x => x.Insert(It.IsAny<CouchPotatoSettings>()), Times.Once);
            MockRepo.Verify(x => x.Modify(It.IsAny<CouchPotatoSettings>()), Times.Never);
        }

        [Test]
        public void ModifySettings()
        {
            var dto = new CouchPotatoSettingsDto { Id = 1 };
            var result = Service.SaveSettings(dto);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.True);

            MockRepo.Verify(x => x.Find(1), Times.Once);
            MockRepo.Verify(x => x.Insert(It.IsAny<CouchPotatoSettings>()), Times.Never);
            MockRepo.Verify(x => x.Modify(It.IsAny<CouchPotatoSettings>()), Times.Once);
        }



    }
}
