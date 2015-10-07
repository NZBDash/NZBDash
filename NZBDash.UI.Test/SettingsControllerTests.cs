using Moq;

using NUnit.Framework;

using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.UI.Controllers;
using NZBDash.UI.Controllers.Application;
using NZBDash.UI.Models.Settings;

using TestStack.FluentMVCTesting;

namespace NZBDash.UI.Test
{
    [TestFixture]
    public class SettingsControllerTests
    {
        private SettingsController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new SettingsController();
        }

        [Test]
        [Ignore("Need to mock out the DAL")]
        public void GetNzbGetSettingsReturnsDefaultView()
        {
            _controller.WithCallTo(x => x.NzbGetSettings()).ShouldRenderDefaultView();
        }

        [Test]
        public void PostNzbGetSettingsReturnsErrorWithBadModel()
        {
            var model = new NzbGetSettingsViewModel();
            _controller.WithModelErrors().WithCallTo(x => x.NzbGetSettings(model)).ShouldRenderDefaultView().WithModel(model);
        }

        [Test]
        [Ignore("Need to mock out the DAL")]
        public void PostNzbGetSettingsReturnsDefaultView()
        {
            var model = new NzbGetSettingsViewModel();
            _controller.WithCallTo(x => x.NzbGetSettings(model)).ShouldRedirectTo("NzbGetSettings");
        }

        [Test]
        [Ignore("Need to mock out the DAL")]
        public void GetSonarrSettingsReturnsDefaultView()
        {
            _controller.WithCallTo(x => x.SonarrSettings()).ShouldRenderDefaultView();
        }

        [Test]
        public void PostSonarrSettingsReturnsErrorWithBadModel()
        {
            var model = new SonarrSettingsViewModel();
            _controller.WithModelErrors().WithCallTo(x => x.SonarrSettings(model)).ShouldRenderDefaultView().WithModel(model);
        }
    }
}
