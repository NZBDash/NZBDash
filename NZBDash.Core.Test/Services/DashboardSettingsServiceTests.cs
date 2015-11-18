using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using NZBDash.Common.Models.Data.Models;
using NZBDash.Common.Models.Data.Models.Settings;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.SettingsService;
using NZBDash.DataAccess.Interfaces;

namespace NZBDash.Core.Test.Services
{
    [TestFixture]
    public class DashboardSettingsServiceTests
    {
        private Mock<IRepository<Applications>> MockRepo { get; set; }
        private List<Applications> ExpectedGetLinks { get; set; }
        private DashboardSettingsService Service { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mockRepo = new Mock<IRepository<Applications>>();
            ExpectedGetLinks = new List<Applications> { new Applications { Id = 1, ApplicationName = "NZBGet", Enabled = true, ShowOnDashboard = true, IpAddress = "192", Port = 25} };
            mockRepo.Setup(x => x.GetAll()).Returns(ExpectedGetLinks).Verifiable();
            MockRepo = mockRepo;
            Service = new DashboardSettingsService(mockRepo.Object);
        }

        [Test]
        public void GetSettings()
        {
            var result = Service.GetEnabledDashlets().ToList();
            Assert.That(result, Is.Not.Null);
            Assert.That(result[0].ApplicationName, Is.EqualTo(ExpectedGetLinks[0].ApplicationName));
            Assert.That(result[0].Enabled, Is.EqualTo(ExpectedGetLinks[0].Enabled));
            Assert.That(result[0].Id, Is.EqualTo(ExpectedGetLinks[0].Id));
            Assert.That(result[0].Port, Is.EqualTo(ExpectedGetLinks[0].Port));
            Assert.That(result[0].ShowOnDashboard, Is.EqualTo(ExpectedGetLinks[0].ShowOnDashboard));
            Assert.That(result[0].IpAddress, Is.EqualTo(ExpectedGetLinks[0].IpAddress));
            MockRepo.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
