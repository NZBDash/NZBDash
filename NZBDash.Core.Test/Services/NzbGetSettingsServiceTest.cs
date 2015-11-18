using System.Collections.Generic;

using Moq;

using NUnit.Framework;
using NZBDash.Common.Interfaces;
using NZBDash.Common.Models.Data.Models.Settings;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.SettingsService;
using NZBDash.DataAccess.Interfaces;

namespace NZBDash.Core.Test.Services
{
    [TestFixture]
    public class NzbGetSettingsServiceTest
    {
        private Mock<IRepository<NzbGetSettings>> MockRepo { get; set; }
        private List<NzbGetSettings> ExpectedGetLinks { get; set; }
        private NzbGetSettings ExpectedLink { get; set; }
        private NzbGetSettingsService Service { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mockRepo = new Mock<IRepository<NzbGetSettings>>();
            var mockLogger = new Mock<ILogger>();
            ExpectedGetLinks = new List<NzbGetSettings>
            {
                new NzbGetSettings
                {
                    Id = 1, Enabled = true, IpAddress = "192", Password = "abc", Port = 25, Username = "bvc", ShowOnDashboard = true
                }
            };
            ExpectedLink = new NzbGetSettings
            {
                Id = 1,
                Enabled = true,
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

            mockRepo.Setup(x => x.Modify(It.IsAny<NzbGetSettings>())).Returns(1).Verifiable();
            mockRepo.Setup(x => x.ModifyAsync(It.IsAny<NzbGetSettings>())).ReturnsAsync(1).Verifiable();

            mockRepo.Setup(x => x.Insert(It.IsAny<NzbGetSettings>())).Returns(ExpectedLink).Verifiable();
            mockRepo.Setup(x => x.InsertAsync(It.IsAny<NzbGetSettings>())).ReturnsAsync(ExpectedLink).Verifiable();


            MockRepo = mockRepo;
            Service = new NzbGetSettingsService(mockRepo.Object);
        }

        [Test]
        public void GetSettings()
        {
            var result = Service.GetSettings();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Enabled, Is.EqualTo(ExpectedGetLinks[0].Enabled));
            Assert.That(result.ShowOnDashboard, Is.EqualTo(ExpectedGetLinks[0].ShowOnDashboard));
            Assert.That(result.Port, Is.EqualTo(ExpectedGetLinks[0].Port));
            Assert.That(result.Username, Is.EqualTo(ExpectedGetLinks[0].Username));
            Assert.That(result.Password, Is.EqualTo(ExpectedGetLinks[0].Password));
            Assert.That(result.IpAddress, Is.EqualTo(ExpectedGetLinks[0].IpAddress));
            MockRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void InsertSettings()
        {
            var dto = new NzbGetSettingsDto { Id = 2 };
            var result = Service.SaveSettings(dto);

            Assert.That(result, Is.True);
            MockRepo.Verify(x => x.Find(2), Times.Once);
            MockRepo.Verify(x => x.Insert(It.IsAny<NzbGetSettings>()), Times.Once);
            MockRepo.Verify(x => x.Modify(It.IsAny<NzbGetSettings>()), Times.Never);
        }

        [Test]
        public void ModifySettings()
        {
            var dto = new NzbGetSettingsDto { Id = 1 };
            var result = Service.SaveSettings(dto);

            Assert.That(result, Is.True);
            MockRepo.Verify(x => x.Find(1), Times.Once);
            MockRepo.Verify(x => x.Insert(It.IsAny<NzbGetSettings>()), Times.Never);
            MockRepo.Verify(x => x.Modify(It.IsAny<NzbGetSettings>()), Times.Once);
        }
    }
}
