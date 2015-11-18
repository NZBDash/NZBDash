using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Moq;

using NUnit.Framework;

using NZBDash.Common.Models.Data.Models;
using NZBDash.Core.Configuration;
using NZBDash.Core.Model.DTO;
using NZBDash.DataAccess.Interfaces;

namespace NZBDash.Core.Test
{
    [TestFixture]
    public class LinksConfigurationServiceTest
    {
        private Mock<IRepository<LinksConfiguration>> MockRepo { get; set; }
        private List<LinksConfiguration> ExpectedGetLinks { get; set; }
        private LinksConfiguration ExpectedLink { get; set; }
        private LinksConfigurationService Service { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mockRepo = new Mock<IRepository<LinksConfiguration>>();
            ExpectedGetLinks = new List<LinksConfiguration> { new LinksConfiguration { Id = 1, LinkEndpoint = "google.com", LinkName = "Google" } };
            ExpectedLink = new LinksConfiguration { Id = 1, LinkEndpoint = "google.com", LinkName = "Google" };

            mockRepo.Setup(x => x.GetAll()).Returns(ExpectedGetLinks).Verifiable();
            mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(ExpectedGetLinks).Verifiable();

            mockRepo.Setup(x => x.Find(1)).Returns(ExpectedLink).Verifiable();
            mockRepo.Setup(x => x.FindAsync(1)).ReturnsAsync(ExpectedLink).Verifiable();

            mockRepo.Setup(x => x.Modify(It.IsAny<LinksConfiguration>())).Returns(1).Verifiable();
            mockRepo.Setup(x => x.ModifyAsync(It.IsAny<LinksConfiguration>())).ReturnsAsync(1).Verifiable();

            mockRepo.Setup(x => x.Insert(It.IsAny<LinksConfiguration>())).Returns(ExpectedLink).Verifiable();
            mockRepo.Setup(x => x.InsertAsync(It.IsAny<LinksConfiguration>())).ReturnsAsync(ExpectedLink).Verifiable();


            mockRepo.Setup(x => x.Remove(It.IsAny<LinksConfiguration>())).Returns(1).Verifiable();
            mockRepo.Setup(x => x.RemoveAsync(It.IsAny<LinksConfiguration>())).ReturnsAsync(1).Verifiable();

            MockRepo = mockRepo;
            Service = new LinksConfigurationService(MockRepo.Object);
        }

        [Test]
        public void GetLinks()
        {
            var result = Service.GetLinks().ToList();

            Assert.That(result, Is.Not.Null);
            Assert.That(result[0].Id, Is.EqualTo(ExpectedGetLinks[0].Id));
            Assert.That(result[0].LinkEndpoint, Is.EqualTo(ExpectedGetLinks[0].LinkEndpoint));
            Assert.That(result[0].LinkName, Is.EqualTo(ExpectedGetLinks[0].LinkName));
            MockRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public async void GetLinksAsync()
        {
            var result = await Service.GetLinksAsync();
            var list = result.ToList();

            Assert.That(result, Is.Not.Null);
            Assert.That(list[0].Id, Is.EqualTo(ExpectedGetLinks[0].Id));
            Assert.That(list[0].LinkEndpoint, Is.EqualTo(ExpectedGetLinks[0].LinkEndpoint));
            Assert.That(list[0].LinkName, Is.EqualTo(ExpectedGetLinks[0].LinkName));
            MockRepo.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Test]
        public void UpdateLink()
        {
            var dto = new LinksConfigurationDto { Id = 1, LinkEndpoint = "www.microsoft.com", LinkName = "Microsoft" };
            var result = Service.UpdateLink(dto);

            Assert.That(result, Is.True);
            MockRepo.Verify(x => x.Find(It.IsAny<int>()), Times.Once);
            MockRepo.Verify(x => x.Modify(It.IsAny<LinksConfiguration>()), Times.Once);
        }

        [Test]
        public async void UpdateLinkAsync()
        {
            var dto = new LinksConfigurationDto { Id = 1, LinkEndpoint = "www.microsoft.com", LinkName = "Microsoft" };
            var result = await Service.UpdateLinkAsync(dto);

            Assert.That(result, Is.True);
            MockRepo.Verify(x => x.FindAsync(It.IsAny<int>()), Times.Once);
            MockRepo.Verify(x => x.ModifyAsync(It.IsAny<LinksConfiguration>()), Times.Once);
        }

        [Test]
        public void AddLink()
        {
            var dto = new LinksConfigurationDto { Id = 1, LinkEndpoint = "google.com", LinkName = "Google" };
            var result = Service.AddLink(dto);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(ExpectedLink.Id));
            Assert.That(result.LinkEndpoint, Is.EqualTo(ExpectedLink.LinkEndpoint));
            Assert.That(result.LinkName, Is.EqualTo(ExpectedLink.LinkName));
            MockRepo.Verify(x => x.Insert(It.IsAny<LinksConfiguration>()), Times.Once);
        }

        [Test]
        public void RemoveLink()
        {
            var result = Service.RemoveLink(1);
            Assert.That(result, Is.True);
            MockRepo.Verify(x => x.Remove(It.IsAny<LinksConfiguration>()), Times.Once);
        }

        [Test]
        public void RemoveIncorrectLink()
        {
            var result = Service.RemoveLink(2);
            Assert.That(result, Is.False);
            MockRepo.Verify(x => x.Remove(It.IsAny<LinksConfiguration>()), Times.Never);
        }
    }
}
