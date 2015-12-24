using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using NZBDash.Common.Interfaces;
using NZBDash.Common.Models.Settings;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.SettingsService;
using NZBDash.DataAccessLayer.Interfaces;
using NZBDash.DataAccessLayer.Models.Settings;

using Ploeh.AutoFixture;

using GlobalSettings = NZBDash.DataAccessLayer.Models.Settings.GlobalSettings;

namespace NZBDash.Core.Test.Services
{
    [TestFixture]
    public class SettingsServiceTest
    {
        private Mock<ISettingsRepository> MockRepo { get; set; }
        private List<GlobalSettings> ExpectedGetLinks { get; set; }
        private GlobalSettings ExpectedLink { get; set; }
        private SettingsService<NzbDashSettings,Setting> Service { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mockRepo = new Mock<ISettingsRepository>();
            var logger = new Mock<ILogger>();
            ExpectedLink = new GlobalSettings
            {
                Id = 1,
                Content = "aaa",
                SettingsName = "Test",
            };

            var f = new Fixture();
            ExpectedGetLinks = f.CreateMany<GlobalSettings>().ToList();


            mockRepo.Setup(x => x.GetAll()).Returns(ExpectedGetLinks).Verifiable();

            mockRepo.Setup(x => x.Get("Test")).Returns(ExpectedLink).Verifiable();

            mockRepo.Setup(x => x.Update(It.IsAny<GlobalSettings>())).Returns(true).Verifiable();

            mockRepo.Setup(x => x.Insert(It.IsAny<GlobalSettings>())).Returns(1).Verifiable();


            MockRepo = mockRepo;
            Service = new SettingsService<NzbDashSettings, Setting>(MockRepo.Object, logger.Object);
        }

        [Test]
        public void GetSettings()
        {
            var result = Service.GetSettings();

            Assert.That(result, Is.Not.Null);
            MockRepo.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
        }

       
    }
}
