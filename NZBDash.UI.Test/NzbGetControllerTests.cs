using Moq;

using NUnit.Framework;

using NZBDash.Api.Controllers;
using NZBDash.Api.Models;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.UI.Controllers.Application;
using NZBDash.UI.Models.NzbGet;

using TestStack.FluentMVCTesting;

namespace NZBDash.UI.Test
{
    [TestFixture]
    public class NzbGetControllerTests
    {
        private NzbGetController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new NzbGetController();
        }

        [Test]
        public void EnsureThatIndexReturnsPopulatedModel()
        {
            var expectedSettings = new NzbGetSettingsDto
            {
                Username = "test",
                Id = 2,
                Password = "1",
                ShowOnDashboard = true,
                Enabled = true,
                IpAddress = "192.168.0.1",
                Port = 25
            };

            var expectedApi = new NzbGetStatus
            {
                Result = new NzbGetStatusResult
                    {
                        DownloadRate = 20000,
                        ServerPaused = true,
                    },
                version = "1"
            };

            var mockSettings = new Mock<ISettings<NzbGetSettingsDto>>();
            mockSettings.Setup(x => x.GetSettings()).Returns(expectedSettings);

            var mockApi = new Mock<IStatusApi>();
            mockApi.Setup(x => x.GetNzbGetStatus("http://192.168.0.1/", "test", "1")).Returns(expectedApi);

            _controller = new NzbGetController(mockSettings.Object, mockApi.Object);

            _controller.WithCallTo(x => x.Index()).ShouldRenderDefaultView().WithModel<NzbGetViewModel>();
        }
    }
}
