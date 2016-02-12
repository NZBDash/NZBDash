#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: DashboardControllerTests.cs
//  Created By: Jamie Rees
//
//  Permission is hereby granted, free of charge, to any person obtaining
//  a copy of this software and associated documentation files (the
//  "Software"), to deal in the Software without restriction, including
//  without limitation the rights to use, copy, modify, merge, publish,
//  distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to
//  the following conditions:
//
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//  LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//  WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ***********************************************************************
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Moq;

using NUnit.Framework;

using NZBDash.Common.Helpers;
using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.DTO;
using NZBDash.Core.Models.Settings;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.ThirdParty.Api.Models.Api;
using NZBDash.ThirdParty.Api.Models.Api.SabNzbd;
using NZBDash.UI.Controllers;
using NZBDash.UI.Models.Hardware;
using NZBDash.UI.Models.ViewModels.Dashboard;

using Ploeh.AutoFixture;

using TestStack.FluentMVCTesting;

namespace NZBDash.UI.Test.Controllers
{
    [TestFixture]
    public class DashboardControllerTests
    {
        private DashboardController _controller;
        private Fixture F { get; set; }
        private Mock<IThirdPartyService> ThirdPartyApi { get; set; }
        private Mock<IHardwareService> HardwareServiceMock { get; set; }
        private Mock<ISettingsService<NzbGetSettingsDto>> NzbGetMock { get; set; }
        private Mock<ISettingsService<NzbDashSettingsDto>> NzbDashSettings { get; set; }

        private List<LinksConfigurationDto> LinksDto { get; set; }
        private RamModel RamModel { get; set; }
        private List<DriveModel> DriveModel { get; set; }

        [SetUp]
        public void MockSetup()
        {
            F = new Fixture();

            var mockHardware = new Mock<IHardwareService>();
            var mockLog = new Mock<ILogger>();
            var mockApi = new Mock<IThirdPartyService>();
            var mocknzbGet = new Mock<ISettingsService<NzbGetSettingsDto>>();
            NzbDashSettings = new Mock<ISettingsService<NzbDashSettingsDto>>();



            var nzbDashSettingsDto = F.Create<NzbDashSettingsDto>();
            RamModel = F.Create<RamModel>();
            DriveModel = F.CreateMany<DriveModel>().ToList();
            var linksDto = F.CreateMany<LinksConfigurationDto>();

            mockHardware.Setup(x => x.GetUpTime()).Returns(new TimeSpan(2, 12, 33, 59, 200));
            mockHardware.Setup(x => x.GetRam()).Returns(RamModel);
            mockHardware.Setup(x => x.GetDrives()).Returns(DriveModel);

            NzbDashSettings.Setup(x => x.GetSettings()).Returns(nzbDashSettingsDto);
            

            HardwareServiceMock = mockHardware;
            ThirdPartyApi = mockApi;
            NzbGetMock = mocknzbGet;
            LinksDto = linksDto.ToList();

            _controller = new DashboardController(HardwareServiceMock.Object, ThirdPartyApi.Object,   mockLog.Object, null, null, NzbDashSettings.Object);
        }


        [Test]
        public void EnsureThatIndexReturnsDefaultView()
        {
            _controller.WithCallTo(x => x.Index()).ShouldRenderDefaultView();
        }

        [Test]
        public void GetServerInformation()
        {
            var uptime = new TimeSpan(2, 12, 33, 59, 200);
            var result = (PartialViewResult)_controller.GetServerInformation();
            var model = (ServerInformationViewModel)result.Model;


            HardwareServiceMock.Verify(x => x.GetCpuPercentage(), Times.Once());
            HardwareServiceMock.Verify(x => x.GetAvailableRam(), Times.Once());
            HardwareServiceMock.Verify(x => x.GetUpTime(), Times.Once());

            _controller.WithCallTo(x => x.GetServerInformation()).ShouldRenderPartialView("Partial/_ServerInformation").WithModel<ServerInformationViewModel>();

            Assert.That(model.OsPlatform, Is.EqualTo(RamModel.OSPlatform));
            Assert.That(model.OsFullName, Is.EqualTo(RamModel.OSFullName));
            Assert.That(model.OsVersion, Is.EqualTo(RamModel.OSVersion));
            Assert.That(model.Uptime, Is.EqualTo(uptime));
        }

        [Test]
        public void GetRam()
        {
            var result = (PartialViewResult)_controller.GetRam();
            var model = (RamModel)result.Model;

            HardwareServiceMock.Verify(x => x.GetRam(), Times.Once());

            _controller.WithCallTo(x => x.GetRam()).ShouldRenderPartialView("Partial/_Ram").WithModel<RamModel>();

            Assert.That(model.AvailablePhysicalMemory, Is.EqualTo(RamModel.AvailablePhysicalMemory));
            Assert.That(model.AvailableVirtualMemory, Is.EqualTo(RamModel.AvailableVirtualMemory));
            Assert.That(model.VirtualPercentageFilled, Is.EqualTo(RamModel.VirtualPercentageFilled));
            Assert.That(model.PhysicalPercentageFilled, Is.EqualTo(RamModel.PhysicalPercentageFilled));
            Assert.That(model.OSFullName, Is.EqualTo(RamModel.OSFullName));
            Assert.That(model.OSPlatform, Is.EqualTo(RamModel.OSPlatform));
            Assert.That(model.OSVersion, Is.EqualTo(RamModel.OSVersion));
            Assert.That(model.TotalPhysicalMemory, Is.EqualTo(RamModel.TotalPhysicalMemory));
            Assert.That(model.TotalVirtualMemory, Is.EqualTo(RamModel.TotalVirtualMemory));
            Assert.That(model.VirtualPercentageFilled, Is.EqualTo(RamModel.VirtualPercentageFilled));
        }

        [Test]
        public void GetDriveInformation()
        {
            var result = (PartialViewResult)_controller.GetDriveInformation();
            var model = (List<DriveModel>)result.Model;

            HardwareServiceMock.Verify(x => x.GetDrives(), Times.Once());

            _controller.WithCallTo(x => x.GetDriveInformation()).ShouldRenderPartialView("Partial/_DriveInformation").WithModel<IEnumerable<DriveModel>>();

            Assert.That(model[0].PercentageFilled, Is.EqualTo(DriveModel[0].PercentageFilled));
            Assert.That(model[0].IsReady, Is.EqualTo(DriveModel[0].IsReady));
            Assert.That(model[0].Name, Is.EqualTo(DriveModel[0].Name));
            Assert.That(model[0].DriveType, Is.EqualTo(DriveModel[0].DriveType));
            Assert.That(model[0].DriveFormat, Is.EqualTo(DriveModel[0].DriveFormat));
            Assert.That(model[0].AvailableFreeSpace, Is.EqualTo(DriveModel[0].AvailableFreeSpace));
            Assert.That(model[0].VolumeLabel, Is.EqualTo(DriveModel[0].VolumeLabel));
            Assert.That(model[0].TotalSize, Is.EqualTo(DriveModel[0].TotalSize));
            Assert.That(model[0].TotalFreeSpace, Is.EqualTo(DriveModel[0].TotalFreeSpace));
            Assert.That(model[0].PercentageFilled, Is.EqualTo(DriveModel[0].PercentageFilled));
        }

        [Test]
        public void GetSabDownloadCount()
        {
            var mockNzbGet = new Mock<ISettingsService<NzbGetSettingsDto>>();
            var mockSab = new Mock<ISettingsService<SabNzbdSettingsDto>>();
            
            var sabSettings = F.Create<SabNzbdSettingsDto>();
            var sabQueue = F.Create<SabNzbdQueue>();

            mockSab.Setup(x => x.GetSettings()).Returns(sabSettings).Verifiable();
            ThirdPartyApi.Setup(x => x.GetSabNzbdQueue(It.IsAny<string>(),It.IsAny<string>())).Returns(sabQueue).Verifiable();
            _controller = new DashboardController(HardwareServiceMock.Object, ThirdPartyApi.Object,   new Mock<ILogger>().Object, mockNzbGet.Object, mockSab.Object, NzbDashSettings.Object);


            var result = (PartialViewResult)_controller.GetDownloads();
            var model = (DashboardDownloadViewModel)result.Model;

            Assert.That(model.Application, Is.EqualTo("Sabnzbd"));
            Assert.That(model.DownloadItems, Is.EqualTo(sabQueue.jobs.Count));

            mockNzbGet.Verify(x => x.GetSettings(), Times.Once);
            mockSab.Verify(x => x.GetSettings(), Times.Once);
            ThirdPartyApi.Verify(x => x.GetNzbGetList(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            ThirdPartyApi.Verify(x => x.GetSabNzbdQueue(It.IsAny<string>(),It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetNzbGetDownloadCount()
        {
            var mockNzbGet = new Mock<ISettingsService<NzbGetSettingsDto>>();
            var mockSab = new Mock<ISettingsService<SabNzbdSettingsDto>>();

            var nzbGetSettings = F.Create<NzbGetSettingsDto>();
            var nzbList = F.Create<NzbGetList>();

            mockNzbGet.Setup(x => x.GetSettings()).Returns(nzbGetSettings).Verifiable();
            ThirdPartyApi.Setup(x => x.GetNzbGetList(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(nzbList).Verifiable();
            _controller = new DashboardController(HardwareServiceMock.Object, ThirdPartyApi.Object,   new Mock<ILogger>().Object, mockNzbGet.Object, mockSab.Object, NzbDashSettings.Object);
            
            var result = (PartialViewResult)_controller.GetDownloads();
            var model = (DashboardDownloadViewModel)result.Model;

            Assert.That(model.Application, Is.EqualTo("NzbGet"));
            Assert.That(model.DownloadItems, Is.EqualTo(nzbList.result.Count));

            mockNzbGet.Verify(x => x.GetSettings(), Times.Once);
            mockSab.Verify(x => x.GetSettings(), Times.Once);
            ThirdPartyApi.Verify(x => x.GetNzbGetList(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            ThirdPartyApi.Verify(x => x.GetSabNzbdQueue(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void GetNoUsenetDownloaderDownloadCount()
        {
            var mockNzbGet = new Mock<ISettingsService<NzbGetSettingsDto>>();
            var mockSab = new Mock<ISettingsService<SabNzbdSettingsDto>>();
            var logger = new Mock<ILogger>();

            _controller = new DashboardController(HardwareServiceMock.Object, ThirdPartyApi.Object,   logger.Object, mockNzbGet.Object, mockSab.Object, NzbDashSettings.Object);
            
            var result = (PartialViewResult)_controller.GetDownloads();
            var model = (DashboardDownloadViewModel)result.Model;

            Assert.That(model.Application, Is.Null);
            logger.Verify(x => x.Trace(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetNzbGetTabDownloads()
        {
            var mockNzbGet = new Mock<ISettingsService<NzbGetSettingsDto>>();
            var mockSab = new Mock<ISettingsService<SabNzbdSettingsDto>>();
            var logger = new Mock<ILogger>();

            var nzbGetSettings = F.Build<NzbGetSettingsDto>().With(x => x.Enabled).Create();
            var nzbList = F.Create<NzbGetList>();
            var status = F.Create<NzbGetStatus>();

            mockNzbGet.Setup(x => x.GetSettings()).Returns(nzbGetSettings).Verifiable();
            ThirdPartyApi.Setup(x => x.GetNzbGetList(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(nzbList).Verifiable();
           ThirdPartyApi.Setup(x => x.GetNzbGetStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(status).Verifiable();

            _controller = new DashboardController(HardwareServiceMock.Object, ThirdPartyApi.Object,   logger.Object, mockNzbGet.Object, mockSab.Object, NzbDashSettings.Object);

            var result = (PartialViewResult)_controller.GetTabDownloads();
            var model = (TabDownloadViewModel)result.Model;

            Assert.That(model.Application, Is.EqualTo("NzbGet"));
            Assert.That(model.DownloadSpeed, Is.EqualTo(MemorySizeConverter.SizeSuffix(status.Result.DownloadRate / 1024)));
            Assert.That(model.Downloads.Count, Is.EqualTo(nzbList.result.Count));
            Assert.That(model.Downloads[0].DownloadName, Is.EqualTo(nzbList.result[0].NZBName));
            Assert.That(model.Downloads[0].DownloadPercentage, Is.Not.Null);
            Assert.That(model.Downloads[0].Status, Is.Not.Null);
            Assert.That(model.Downloads[0].ProgressBarClass, Is.Not.Null);
        }

        [Test]
        public void GetSabTabDownloads()
        {
            var mockNzbGet = new Mock<ISettingsService<NzbGetSettingsDto>>();
            var mockSab = new Mock<ISettingsService<SabNzbdSettingsDto>>();

            var sabSettings = F.Build<SabNzbdSettingsDto>().With(x => x.Enabled, true).Create();
            var sabQueue = F.Create<SabNzbdQueue>();
            
            mockSab.Setup(x => x.GetSettings()).Returns(sabSettings).Verifiable();
            ThirdPartyApi.Setup(x => x.GetSabNzbdQueue(It.IsAny<string>(), It.IsAny<string>())).Returns(sabQueue).Verifiable();

            _controller = new DashboardController(HardwareServiceMock.Object, ThirdPartyApi.Object,   new Mock<ILogger>().Object, mockNzbGet.Object, mockSab.Object, NzbDashSettings.Object);

            var result = (PartialViewResult)_controller.GetTabDownloads();
            var model = (TabDownloadViewModel)result.Model;

            Assert.That(model.Application, Is.EqualTo("Sabnzbd"));
            Assert.That(model.Downloads.Count, Is.EqualTo(sabQueue.jobs.Count));
            Assert.That(model.DownloadSpeed, Is.Not.Null);
            Assert.That(model.Downloads[0].DownloadPercentage, Is.Not.Null);
            Assert.That(model.Downloads[0].Status, Is.Not.Null);
            Assert.That(model.Downloads[0].ProgressBarClass, Is.Not.Null);
        }
    }
}
