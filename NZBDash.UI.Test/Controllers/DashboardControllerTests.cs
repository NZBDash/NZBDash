using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Moq;

using NUnit.Framework;

using NZBDash.Api.Controllers;
using NZBDash.Common.Models.Data.Models;
using NZBDash.Common.Models.Hardware;
using NZBDash.Core.Interfaces;
using NZBDash.DataAccess.Interfaces;
using NZBDash.UI.Controllers;

using TestStack.FluentMVCTesting;

namespace NZBDash.UI.Test.Controllers
{
    [TestFixture]
    public class DashboardControllerTests
    {
        private DashboardController _controller;
        private Mock<IStatusApi> StatusApiMock { get; set; }
        private Mock<IHardwareService> HardwareServiceMock { get; set; }
        private Mock<IRepository<LinksConfiguration>> LinksConfigurationServiceMock { get; set; }

        [SetUp]
        public void MockSetup()
        {
            var mockHardware = new Mock<IHardwareService>();
            var mockApi = new Mock<IStatusApi>();
            var mockLinks = new Mock<IRepository<LinksConfiguration>>();
            var ramModel = new RamModel
            {
                AvailablePhysicalMemory = 22,
                AvailableVirtualMemory = 23,
                OSFullName = "Windows",
                OSPlatform = "10",
                OSVersion = "10.0",
                PhysicalPercentageFilled = 98,
                TotalPhysicalMemory = 1024,
                TotalVirtualMemory = 2048,
                VirtualPercentageFilled = 50
            };
            var driveModels = new List<DriveModel>()
            {
                new DriveModel
                {
                    Name = @"C:\",
                    TotalFreeSpace = 2048,
                    TotalSize = 2000000,
                    AvailableFreeSpace = 2049,
                    DriveFormat = "NTFS",
                    DriveType = "Network",
                    IsReady = true,
                    VolumeLabel = "C"
                }
            };



            mockHardware.Setup(x => x.GetUpTime()).Returns(new TimeSpan(2, 12, 33, 59, 200));
            mockHardware.Setup(x => x.GetRam()).Returns(ramModel);
            mockHardware.Setup(x => x.GetDrives()).Returns(driveModels);

            HardwareServiceMock = mockHardware;
            StatusApiMock = mockApi;
            LinksConfigurationServiceMock = mockLinks;

            _controller = new DashboardController(HardwareServiceMock.Object, StatusApiMock.Object, LinksConfigurationServiceMock.Object);
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

            _controller.WithCallTo(x => x.GetServerInformation()).ShouldRenderPartialView("Partial/_ServerInformation").WithModel<ServerInformationViewModel>();

            Assert.That(model.OsPlatform,Is.EqualTo("10"));
            Assert.That(model.OsFullName,Is.EqualTo("Windows"));
            Assert.That(model.OsVersion,Is.EqualTo("10.0"));
            Assert.That(model.Uptime,Is.EqualTo(uptime));
        }

        [Test]
        public void GetRam()
        {
            var result = (PartialViewResult)_controller.GetRam();
            var model = (RamModel)result.Model;

            _controller.WithCallTo(x => x.GetRam()).ShouldRenderPartialView("Partial/_Ram").WithModel<RamModel>();

            Assert.That(model.AvailablePhysicalMemory, Is.EqualTo(22));
            Assert.That(model.AvailableVirtualMemory, Is.EqualTo(23));
            Assert.That(model.VirtualPercentageFilled, Is.EqualTo(50));
            Assert.That(model.PhysicalPercentageFilled, Is.EqualTo(98));
        }

        [Test]
        public void GetDriveInformation()
        {
            var result = (PartialViewResult)_controller.GetDriveInformation();
            var model = (List<DriveModel>)result.Model;

            _controller.WithCallTo(x => x.GetDriveInformation()).ShouldRenderPartialView("Partial/_DriveInformation").WithModel<IEnumerable<DriveModel>>();

            Assert.That(model[0].PercentageFilled, Is.EqualTo(100));
            Assert.That(model[0].IsReady, Is.EqualTo(true));
            Assert.That(model[0].Name, Is.EqualTo(@"C:\"));
        }
    }
}
