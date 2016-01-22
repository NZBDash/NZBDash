#region Copyright
// /************************************************************************
//   Copyright (c) 2016 Jamie Rees
//   File: SettingsControllerTests.cs
//   Created By: Jamie Rees
//  
//   Permission is hereby granted, free of charge, to any person obtaining
//   a copy of this software and associated documentation files (the
//   "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish,
//   distribute, sublicense, and/or sell copies of the Software, and to
//   permit persons to whom the Software is furnished to do so, subject to
//   the following conditions:
//  
//   The above copyright notice and this permission notice shall be
//   included in all copies or substantial portions of the Software.
//  
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//   EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//   LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//   OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//   WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ************************************************************************/
#endregion
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Moq;

using NUnit.Framework;

using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.Models.Settings;
using NZBDash.UI.Controllers;
using NZBDash.UI.Models.Hardware;
using NZBDash.UI.Models.ViewModels.Settings;

using Ploeh.AutoFixture;

using TestStack.FluentMVCTesting;

namespace NZBDash.UI.Test.Controllers
{
    // TODO: Rework
    [TestFixture]
    public class SettingsControllerTests
    {
        private SettingsController _controller;
        private ILogger Logger { get; set; }
        private Fixture F { get; set; }
        public SettingsControllerTests()
        {
            Logger = new Mock<ILogger>().Object;
            F = new Fixture();
        }

        [Test]
        public void GetCouchPotatoSettings()
        {
            var expectedDto = new CouchPotatoSettingsDto
            {
                Enabled = true,
                Id = 2,
                IpAddress = "192",
                ApiKey = "pass",
                Port = 2,
                ShowOnDashboard = true,
                Password = "pass",
                Username = "user"
            };
            var settingsMock = new Mock<ISettingsService<CouchPotatoSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto).Verifiable();

            _controller = new SettingsController(null, null, null, settingsMock.Object, null, null, null, null, null, Logger);
            _controller.WithCallTo(x => x.CouchPotatoSettings()).ShouldRenderDefaultView();

            var result = (ViewResult)_controller.CouchPotatoSettings();
            var model = (CouchPotatoSettingsViewModel)result.Model;

            Assert.That(model.Enabled, Is.EqualTo(expectedDto.Enabled));
            Assert.That(model.Id, Is.EqualTo(expectedDto.Id));
            Assert.That(model.IpAddress, Is.EqualTo(expectedDto.IpAddress));
            Assert.That(model.ApiKey, Is.EqualTo(expectedDto.ApiKey));
            Assert.That(model.Port, Is.EqualTo(expectedDto.Port));
            Assert.That(model.ShowOnDashboard, Is.EqualTo(expectedDto.ShowOnDashboard));
            Assert.That(model.Username, Is.EqualTo(expectedDto.Username));
            Assert.That(model.Password, Is.EqualTo(expectedDto.Password));
        }

        [Test]
        public void GetNzbDashSettingsReturnsDefaultViewWithModel()
        {
            var expectedDto = new NzbDashSettingsDto { Id = 2, Authenticate = false };
            var settingsMock = new Mock<ISettingsService<NzbDashSettingsDto>>();
            var authMock = new Mock<IAuthenticationService>();
            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto).Verifiable();

            _controller = new SettingsController(null, null, null, null, null, settingsMock.Object, authMock.Object, null, null, Logger);
            _controller.WithCallTo(x => x.NzbDashSettings()).ShouldRenderDefaultView();

            var result = (ViewResult)_controller.NzbDashSettings();
            var model = (NzbDashSettingsViewModel)result.Model;

            Assert.That(model.Authenticate, Is.EqualTo(expectedDto.Authenticate));
            Assert.That(model.Id, Is.EqualTo(expectedDto.Id));
        }

        [Test]
        public void GetNzbGetSettingsReturnsDefaultViewWithModel()
        {
            var expectedDto = new NzbGetSettingsDto { Enabled = true, Id = 2, IpAddress = "192", Password = "pass", Port = 2, ShowOnDashboard = true, Username = "user" };
            var settingsMock = new Mock<ISettingsService<NzbGetSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto).Verifiable();

            _controller = new SettingsController(settingsMock.Object, null, null, null, null, null, null, null, null, Logger);
            _controller.WithCallTo(x => x.NzbGetSettings()).ShouldRenderDefaultView();

            var result = (ViewResult)_controller.NzbGetSettings();
            var model = (NzbGetSettingsViewModel)result.Model;

            Assert.That(model.Enabled, Is.EqualTo(expectedDto.Enabled));
            Assert.That(model.Id, Is.EqualTo(expectedDto.Id));
            Assert.That(model.IpAddress, Is.EqualTo(expectedDto.IpAddress));
            Assert.That(model.Password, Is.EqualTo(expectedDto.Password));
            Assert.That(model.Port, Is.EqualTo(expectedDto.Port));
            Assert.That(model.ShowOnDashboard, Is.EqualTo(expectedDto.ShowOnDashboard));
            Assert.That(model.Username, Is.EqualTo(expectedDto.Username));
        }

        [Test]
        public void GetPlexSettings()
        {
            var expectedDto = new PlexSettingsDto { Enabled = true, Id = 2, IpAddress = "192", Port = 2, ShowOnDashboard = true, Password = "pass", Username = "user" };
            var settingsMock = new Mock<ISettingsService<PlexSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto).Verifiable();

            _controller = new SettingsController(null, null, null, null, settingsMock.Object, null, null, null, null, Logger);
            _controller.WithCallTo(x => x.PlexSettings()).ShouldRenderDefaultView();

            var result = (ViewResult)_controller.PlexSettings();
            var model = (PlexSettingsViewModel)result.Model;

            Assert.That(model.Enabled, Is.EqualTo(expectedDto.Enabled));
            Assert.That(model.Id, Is.EqualTo(expectedDto.Id));
            Assert.That(model.IpAddress, Is.EqualTo(expectedDto.IpAddress));
            Assert.That(model.Port, Is.EqualTo(expectedDto.Port));
            Assert.That(model.ShowOnDashboard, Is.EqualTo(expectedDto.ShowOnDashboard));
            Assert.That(model.Username, Is.EqualTo(expectedDto.Username));
            Assert.That(model.Password, Is.EqualTo(expectedDto.Password));
        }

        [Test]
        public void GetSabNzbdSettings()
        {
            var expectedDto = new SabNzbdSettingsDto { Enabled = true, Id = 2, IpAddress = "192", ApiKey = "pass", Port = 2, ShowOnDashboard = true };
            var settingsMock = new Mock<ISettingsService<SabNzbdSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto).Verifiable();

            _controller = new SettingsController(null, settingsMock.Object, null, null, null, null, null, null, null, Logger);
            _controller.WithCallTo(x => x.SabNzbSettings()).ShouldRenderDefaultView();

            var result = (ViewResult)_controller.SabNzbSettings();
            var model = (SabNzbSettingsViewModel)result.Model;

            Assert.That(model.Enabled, Is.EqualTo(expectedDto.Enabled));
            Assert.That(model.Id, Is.EqualTo(expectedDto.Id));
            Assert.That(model.IpAddress, Is.EqualTo(expectedDto.IpAddress));
            Assert.That(model.ApiKey, Is.EqualTo(expectedDto.ApiKey));
            Assert.That(model.Port, Is.EqualTo(expectedDto.Port));
            Assert.That(model.ShowOnDashboard, Is.EqualTo(expectedDto.ShowOnDashboard));
        }

        [Test]
        public void GetSonarrSettingsReturnsDefaultView()
        {
            var expectedDto = new SonarrSettingsDto { Enabled = true, Id = 2, IpAddress = "192", ApiKey = "pass", Port = 2, ShowOnDashboard = true };
            var settingsMock = new Mock<ISettingsService<SonarrSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto).Verifiable();

            _controller = new SettingsController(null, null, settingsMock.Object, null, null, null, null, null, null, Logger);
            _controller.WithCallTo(x => x.SonarrSettings()).ShouldRenderDefaultView();

            var result = (ViewResult)_controller.SonarrSettings();
            var model = (SonarrSettingsViewModel)result.Model;

            Assert.That(model.Enabled, Is.EqualTo(expectedDto.Enabled));
            Assert.That(model.Id, Is.EqualTo(expectedDto.Id));
            Assert.That(model.IpAddress, Is.EqualTo(expectedDto.IpAddress));
            Assert.That(model.ApiKey, Is.EqualTo(expectedDto.ApiKey));
            Assert.That(model.Port, Is.EqualTo(expectedDto.Port));
            Assert.That(model.ShowOnDashboard, Is.EqualTo(expectedDto.ShowOnDashboard));
        }


        [Test]
        public void GetHardwareSettingsReturnsDefaultView()
        {
            var expectedDto = F.Create<HardwareSettingsDto>();
            var expectedDrives = F.CreateMany<DriveModel>();
            var settingsMock = new Mock<ISettingsService<HardwareSettingsDto>>();
            var hardwareMock = new Mock<IHardwareService>();
            var expectedNic = F.Create<Dictionary<string, int>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto).Verifiable();
            hardwareMock.Setup(x => x.GetDrives()).Returns(expectedDrives).Verifiable();
            hardwareMock.Setup(x => x.GetAllNics()).Returns(expectedNic).Verifiable();

            _controller = new SettingsController(null, null, null, null, null, null, null, settingsMock.Object, hardwareMock.Object, Logger);
            _controller.WithCallTo(x => x.HardwareSettings()).ShouldRenderDefaultView();

            var result = (ViewResult)_controller.HardwareSettings();
            var model = (HardwareSettingsViewModel)result.Model;

            Assert.That(model.Drives[0].Name, Is.EqualTo(expectedDrives.ToList()[0].Name));
            Assert.That(model.Drives[0].AvailableFreeSpace, Is.EqualTo(expectedDrives.ToList()[0].AvailableFreeSpace));
            Assert.That(model.Drives[0].DriveFormat, Is.EqualTo(expectedDrives.ToList()[0].DriveFormat));
            Assert.That(model.Drives[0].DriveType, Is.EqualTo(expectedDrives.ToList()[0].DriveType));
            Assert.That(model.Drives[0].IsReady, Is.EqualTo(expectedDrives.ToList()[0].IsReady));
            Assert.That(model.Drives[0].TotalFreeSpace, Is.EqualTo(expectedDrives.ToList()[0].TotalFreeSpace));
            Assert.That(model.Drives[0].TotalSize, Is.EqualTo(expectedDrives.ToList()[0].TotalSize));
            Assert.That(model.Drives[0].VolumeLabel, Is.EqualTo(expectedDrives.ToList()[0].VolumeLabel));
            Assert.That(model.Drives.Count, Is.EqualTo(expectedDrives.Count()));

            Assert.That(model.NicDict.Count, Is.EqualTo(expectedNic.Count()));

            foreach (var nic in expectedNic)
            {
                Assert.That(model.NicDict.ContainsKey(nic.Key),Is.True);
                Assert.That(model.NicDict.ContainsValue(nic.Value),Is.True);
            }
        }

        [Test]
        public void PostCouchPotatoSettingsBadModel()
        {
            var expectedDto = new CouchPotatoSettingsDto { Enabled = true, Id = 2, IpAddress = "192", ApiKey = "pass", Port = 2, ShowOnDashboard = true };
            var settingsMock = new Mock<ISettingsService<CouchPotatoSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<CouchPotatoSettingsDto>())).Returns(true);

            _controller = new SettingsController(null, null, null, settingsMock.Object, null, null, null, null, null, Logger);

            var model = new CouchPotatoSettingsViewModel();
            _controller.WithModelErrors().WithCallTo(x => x.CouchPotatoSettings(model)).ShouldRenderDefaultView().WithModel(model);
            settingsMock.Verify(x => x.SaveSettings(It.IsAny<CouchPotatoSettingsDto>()), Times.Never);
        }

        [Test]
        public void PostCouchPotatoSettingsCouldNotSaveToDb()
        {
            var expectedDto = new CouchPotatoSettingsDto { Enabled = true, Id = 2, IpAddress = "192", ApiKey = "pass", Port = 2, ShowOnDashboard = true };
            var settingsMock = new Mock<ISettingsService<CouchPotatoSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<CouchPotatoSettingsDto>())).Returns(false);

            _controller = new SettingsController(null, null, null, settingsMock.Object, null, null, null, null, null, Logger);

            var model = new CouchPotatoSettingsViewModel();
            _controller.WithCallTo(x => x.CouchPotatoSettings(model)).ShouldRenderView("Error");
            settingsMock.Verify(x => x.SaveSettings(It.IsAny<CouchPotatoSettingsDto>()), Times.Once);
        }

        [Test]
        public void PostCouchPotatoSettingsReturnsDefaultView()
        {
            var expectedDto = new CouchPotatoSettingsDto
            {
                Enabled = true,
                Id = 2,
                IpAddress = "192",
                ApiKey = "pass",
                Port = 2,
                ShowOnDashboard = true,
                Password = "pass",
                Username = "user"
            };
            var settingsMock = new Mock<ISettingsService<CouchPotatoSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<CouchPotatoSettingsDto>())).Returns(true);

            _controller = new SettingsController(null, null, null, settingsMock.Object, null, null, null, null, null, Logger);

            var model = new CouchPotatoSettingsViewModel();
            _controller.WithCallTo(x => x.CouchPotatoSettings(model)).ShouldRedirectTo(c => c.CouchPotatoSettings);
            settingsMock.Verify(x => x.SaveSettings(It.IsAny<CouchPotatoSettingsDto>()), Times.Once);
        }

        [Test]
        public void PostNzbDashSettingsCouldNotSaveToDb()
        {
            var expectedDto = new NzbDashSettingsDto { Id = 2, Authenticate = true };
            var settingsMock = new Mock<ISettingsService<NzbDashSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<NzbDashSettingsDto>())).Returns(false).Verifiable();

            _controller = new SettingsController(null, null, null, null, null, settingsMock.Object, null, null, null, Logger);

            var model = new NzbDashSettingsViewModel();
            _controller.WithCallTo(x => x.NzbDashSettings(model)).ShouldRenderView("Error");
        }

        [Test]
        public void PostNzbDashSettingsReturnsDefaultView()
        {
            var expectedDto = new NzbDashSettingsDto { Id = 2, Authenticate = true };
            var settingsMock = new Mock<ISettingsService<NzbDashSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<NzbDashSettingsDto>())).Returns(true).Verifiable();

            _controller = new SettingsController(null, null, null, null, null, settingsMock.Object, null, null, null, Logger);

            var model = new NzbDashSettingsViewModel();
            _controller.WithCallTo(x => x.NzbDashSettings(model)).ShouldRedirectTo(c => c.NzbDashSettings);
        }

        [Test]
        public void PostNzbDashSettingsReturnsErrorWithBadModel()
        {
            var expectedDto = new NzbDashSettingsDto { Id = 2, Authenticate = true };
            var settingsMock = new Mock<ISettingsService<NzbDashSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<NzbDashSettingsDto>())).Returns(true).Verifiable();

            _controller = new SettingsController(null, null, null, null, null, settingsMock.Object, null, null, null, Logger);

            var model = new NzbDashSettingsViewModel();
            _controller.WithModelErrors().WithCallTo(x => x.NzbDashSettings(model)).ShouldRenderDefaultView().WithModel(model);
        }

        [Test]
        public void PostNzbGetSettingsCouldNotSaveToDb()
        {
            var expectedDto = new NzbGetSettingsDto { Enabled = true, Id = 2, IpAddress = "192", Password = "pass", Port = 2, ShowOnDashboard = true, Username = "user" };
            var settingsMock = new Mock<ISettingsService<NzbGetSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto).Verifiable();
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<NzbGetSettingsDto>())).Returns(false).Verifiable();

            _controller = new SettingsController(settingsMock.Object, null, null, null, null, null, null, null, null, Logger);

            var model = new NzbGetSettingsViewModel();
            _controller.WithCallTo(x => x.NzbGetSettings(model)).ShouldRenderView("Error");
        }

        [Test]
        public void PostNzbGetSettingsReturnsDefaultView()
        {
            var expectedDto = new NzbGetSettingsDto { Enabled = true, Id = 2, IpAddress = "192", Password = "pass", Port = 2, ShowOnDashboard = true, Username = "user" };
            var settingsMock = new Mock<ISettingsService<NzbGetSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto).Verifiable();
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<NzbGetSettingsDto>())).Returns(true).Verifiable();

            _controller = new SettingsController(settingsMock.Object, null, null, null, null, null, null, null, null, Logger);

            var model = new NzbGetSettingsViewModel();
            _controller.WithCallTo(x => x.NzbGetSettings(model)).ShouldRedirectTo(c => c.NzbGetSettings);
        }

        [Test]
        public void PostNzbGetSettingsReturnsErrorWithBadModel()
        {
            var expectedDto = new NzbGetSettingsDto { Enabled = true, Id = 2, IpAddress = "192", Password = "pass", Port = 2, ShowOnDashboard = true, Username = "user" };
            var settingsMock = new Mock<ISettingsService<NzbGetSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto).Verifiable();
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<NzbGetSettingsDto>())).Returns(true).Verifiable();

            _controller = new SettingsController(settingsMock.Object, null, null, null, null, null, null, null, null, Logger);
            var model = new NzbGetSettingsViewModel();
            _controller.WithModelErrors().WithCallTo(x => x.NzbGetSettings(model)).ShouldRenderDefaultView().WithModel(model);
        }

        [Test]
        public void PostPlexSettingsBadModel()
        {
            var expectedDto = new PlexSettingsDto { Enabled = true, Id = 2, IpAddress = "192", Port = 2, ShowOnDashboard = true, Password = "pass", Username = "user" };
            var settingsMock = new Mock<ISettingsService<PlexSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<PlexSettingsDto>())).Returns(true);

            _controller = new SettingsController(null, null, null, null, settingsMock.Object, null, null, null, null, Logger);

            var model = new PlexSettingsViewModel();
            _controller.WithModelErrors().WithCallTo(x => x.PlexSettings(model)).ShouldRenderDefaultView().WithModel(model);
            settingsMock.Verify(x => x.SaveSettings(It.IsAny<PlexSettingsDto>()), Times.Never);
        }

        [Test]
        public void PostPlexSettingsCouldNotSaveToDb()
        {
            var expectedDto = new PlexSettingsDto { Enabled = true, Id = 2, IpAddress = "192", Port = 2, ShowOnDashboard = true, Password = "pass", Username = "user" };
            var settingsMock = new Mock<ISettingsService<PlexSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<PlexSettingsDto>())).Returns(false);

            _controller = new SettingsController(null, null, null, null, settingsMock.Object, null, null, null, null, Logger);

            var model = new PlexSettingsViewModel();
            _controller.WithCallTo(x => x.PlexSettings(model)).ShouldRenderView("Error");
            settingsMock.Verify(x => x.SaveSettings(It.IsAny<PlexSettingsDto>()), Times.Once);
        }

        [Test]
        public void PostPlexSettingsReturnsDefaultView()
        {
            var expectedDto = new PlexSettingsDto { Enabled = true, Id = 2, IpAddress = "192", Port = 2, ShowOnDashboard = true, Password = "pass", Username = "user" };
            var settingsMock = new Mock<ISettingsService<PlexSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<PlexSettingsDto>())).Returns(true);

            _controller = new SettingsController(null, null, null, null, settingsMock.Object, null, null, null, null, Logger);

            var model = new PlexSettingsViewModel();
            _controller.WithCallTo(x => x.PlexSettings(model)).ShouldRedirectTo(c => c.PlexSettings);
            settingsMock.Verify(x => x.SaveSettings(It.IsAny<PlexSettingsDto>()), Times.Once);
        }

        [Test]
        public void PostSabNzbdSettingsBadModel()
        {
            var expectedDto = new SabNzbdSettingsDto { Enabled = true, Id = 2, IpAddress = "192", ApiKey = "pass", Port = 2, ShowOnDashboard = true };
            var settingsMock = new Mock<ISettingsService<SabNzbdSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<SabNzbdSettingsDto>())).Returns(true);

            _controller = new SettingsController(null, settingsMock.Object, null, null, null, null, null, null, null, Logger);

            var model = new SabNzbSettingsViewModel();
            _controller.WithModelErrors().WithCallTo(x => x.SabNzbSettings(model)).ShouldRenderDefaultView().WithModel(model);
            settingsMock.Verify(x => x.SaveSettings(It.IsAny<SabNzbdSettingsDto>()), Times.Never);
        }

        [Test]
        public void PostSabNzbdSettingsCouldNotSaveToDb()
        {
            var expectedDto = new SabNzbdSettingsDto { Enabled = true, Id = 2, IpAddress = "192", ApiKey = "pass", Port = 2, ShowOnDashboard = true };
            var settingsMock = new Mock<ISettingsService<SabNzbdSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<SabNzbdSettingsDto>())).Returns(false);

            _controller = new SettingsController(null, settingsMock.Object, null, null, null, null, null, null, null, Logger);

            var model = new SabNzbSettingsViewModel();
            _controller.WithCallTo(x => x.SabNzbSettings(model)).ShouldRenderView("Error");
            settingsMock.Verify(x => x.SaveSettings(It.IsAny<SabNzbdSettingsDto>()), Times.Once);
        }

        [Test]
        public void PostSabNzbdSettingsReturnsDefaultView()
        {
            var expectedDto = new SabNzbdSettingsDto { Enabled = true, Id = 2, IpAddress = "192", ApiKey = "pass", Port = 2, ShowOnDashboard = true };
            var settingsMock = new Mock<ISettingsService<SabNzbdSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<SabNzbdSettingsDto>())).Returns(true);

            _controller = new SettingsController(null, settingsMock.Object, null, null, null, null, null, null, null, Logger);

            var model = new SabNzbSettingsViewModel();
            _controller.WithCallTo(x => x.SabNzbSettings(model)).ShouldRedirectTo(c => c.SabNzbSettings);
            settingsMock.Verify(x => x.SaveSettings(It.IsAny<SabNzbdSettingsDto>()), Times.Once);
        }

        [Test]
        public void PostSonarrSettingsCouldNotSaveToDb()
        {
            var expectedDto = new SonarrSettingsDto { Enabled = true, Id = 2, IpAddress = "192", ApiKey = "pass", Port = 2, ShowOnDashboard = true };
            var settingsMock = new Mock<ISettingsService<SonarrSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<SonarrSettingsDto>())).Returns(false).Verifiable();

            _controller = new SettingsController(null, null, settingsMock.Object, null, null, null, null, null, null, Logger);

            var model = new SonarrSettingsViewModel();
            _controller.WithCallTo(x => x.SonarrSettings(model)).ShouldRenderView("Error");
        }

        [Test]
        public void PostSonarrSettingsReturnsDefaultView()
        {
            var expectedDto = new SonarrSettingsDto { Enabled = true, Id = 2, IpAddress = "192", ApiKey = "pass", Port = 2, ShowOnDashboard = true };
            var settingsMock = new Mock<ISettingsService<SonarrSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<SonarrSettingsDto>())).Returns(true).Verifiable();

            _controller = new SettingsController(null, null, settingsMock.Object, null, null, null, null, null, null, Logger);

            var model = new SonarrSettingsViewModel();
            _controller.WithCallTo(x => x.SonarrSettings(model)).ShouldRedirectTo(c => c.SonarrSettings);
        }

        [Test]
        public void PostSonarrSettingsReturnsErrorWithBadModel()
        {
            var expectedDto = new SonarrSettingsDto { Enabled = true, Id = 2, IpAddress = "192", ApiKey = "pass", Port = 2, ShowOnDashboard = true };
            var settingsMock = new Mock<ISettingsService<SonarrSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<SonarrSettingsDto>())).Returns(true).Verifiable();

            _controller = new SettingsController(null, null, settingsMock.Object, null, null, null, null, null, null, Logger);

            var model = new SonarrSettingsViewModel();
            _controller.WithModelErrors().WithCallTo(x => x.SonarrSettings(model)).ShouldRenderDefaultView().WithModel(model);
        }

        [Test]
        public void SettingsReturnsDefaultIndex()
        {
            _controller = new SettingsController(null, null, null, null, null, null, null, null, null, Logger);
            _controller.WithModelErrors().WithCallTo(x => x.Index()).ShouldRenderDefaultView();
        }
    }
}