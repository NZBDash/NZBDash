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
    public class SabNzbSettingsServiceTest
    {
        private Mock<IRepository<SabNzbSettings>> MockRepo { get; set; }
        private List<SabNzbSettings> ExpectedGetLinks { get; set; }
        private SabNzbSettings ExpectedLink { get; set; }
        private SabNzbSettingsService Service { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mockRepo = new Mock<IRepository<SabNzbSettings>>();
            ExpectedLink = new SabNzbSettings
            {
                Id = 1,
                Enabled = true,
                IpAddress = "192",
                Port = 25,
                ShowOnDashboard = true,
                ApiKey = "abc"
            };
            ExpectedGetLinks = new List<SabNzbSettings>
            {
                ExpectedLink,
            };


            mockRepo.Setup(x => x.GetAll()).Returns(ExpectedGetLinks).Verifiable();
            mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(ExpectedGetLinks).Verifiable();

            mockRepo.Setup(x => x.Find(1)).Returns(ExpectedLink).Verifiable();
            mockRepo.Setup(x => x.FindAsync(1)).ReturnsAsync(ExpectedLink).Verifiable();

            mockRepo.Setup(x => x.Modify(It.IsAny<SabNzbSettings>())).Returns(1).Verifiable();
            mockRepo.Setup(x => x.ModifyAsync(It.IsAny<SabNzbSettings>())).ReturnsAsync(1).Verifiable();

            mockRepo.Setup(x => x.Insert(It.IsAny<SabNzbSettings>())).Returns(ExpectedLink).Verifiable();
            mockRepo.Setup(x => x.InsertAsync(It.IsAny<SabNzbSettings>())).ReturnsAsync(ExpectedLink).Verifiable();


            MockRepo = mockRepo;
            Service = new SabNzbSettingsService(MockRepo.Object);
        }

        [Test]
        public void GetSettings()
        {
            var result = Service.GetSettings();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Enabled, Is.EqualTo(ExpectedGetLinks[0].Enabled));
            Assert.That(result.ShowOnDashboard, Is.EqualTo(ExpectedGetLinks[0].ShowOnDashboard));
            Assert.That(result.Port, Is.EqualTo(ExpectedGetLinks[0].Port));
            Assert.That(result.ApiKey, Is.EqualTo(ExpectedGetLinks[0].ApiKey));
            Assert.That(result.IpAddress, Is.EqualTo(ExpectedGetLinks[0].IpAddress));
            MockRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void InsertSettings()
        {
            var dto = new SabNzbSettingsDto { Id = 2 };
            var result = Service.SaveSettings(dto);

            Assert.That(result, Is.True);
            MockRepo.Verify(x => x.Find(2), Times.Once);
            MockRepo.Verify(x => x.Insert(It.IsAny<SabNzbSettings>()), Times.Once);
            MockRepo.Verify(x => x.Modify(It.IsAny<SabNzbSettings>()), Times.Never);
        }

        [Test]
        public void ModifySettings()
        {
            var dto = new SabNzbSettingsDto { Id = 1 };
            var result = Service.SaveSettings(dto);

            Assert.That(result, Is.True);
            MockRepo.Verify(x => x.Find(1), Times.Once);
            MockRepo.Verify(x => x.Insert(It.IsAny<SabNzbSettings>()), Times.Never);
            MockRepo.Verify(x => x.Modify(It.IsAny<SabNzbSettings>()), Times.Once);
        }
    }
}
