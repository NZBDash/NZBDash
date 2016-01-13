#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: NzbGetControllerTests.cs
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
using System.Net.Mime;
using System.Web.Mvc;

using Grid.Mvc.Ajax.GridExtensions;

using Moq;

using NUnit.Framework;

using NZBDash.Common;
using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.ThirdParty.Api.Models.Api;
using NZBDash.UI.Controllers.Application;
using NZBDash.UI.Models.Dashboard;
using NZBDash.UI.Models.ViewModels.NzbGet;

using Ploeh.AutoFixture;

namespace NZBDash.UI.Test.Controllers
{
    [TestFixture]
    public class NzbGetControllerTests
    {
        public NzbGetSettingsDto ExpectedSettings
        {
            get
            {
                return new NzbGetSettingsDto { Username = "test", Id = 2, Password = "1", ShowOnDashboard = true, Enabled = true, IpAddress = "192.168.0.1", Port = 25 };
            }
        }

        [Test]
        public void GetNzbGetStatusTest()
        {
            var expectedStatus = new NzbGetStatus { Result = new NzbGetStatusResult { ServerPaused = true } };

            var mockSettings = new Mock<ISettingsService<NzbGetSettingsDto>>();
            var mockApi = new Mock<IThirdPartyService>();
            var mockLogger = new Mock<ILogger>();

            mockSettings.Setup(x => x.GetSettings()).Returns(ExpectedSettings);
            mockApi.Setup(x => x.GetNzbGetStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expectedStatus);


            var controller = new NzbGetController(mockSettings.Object, mockApi.Object, mockLogger.Object);
            var result = (JsonResult)controller.GetNzbGetStatus();
            var model = (NzbGetViewModel)result.Data;


            Assert.That(model.Status, Is.EqualTo("Paused"));
        }

        [Test]
        public void GetNzbGetStatusMissingConfigTest()
        {
            var expectedSettings = new NzbGetSettingsDto();

            var mockSettings = new Mock<ISettingsService<NzbGetSettingsDto>>();
            var mockApi = new Mock<IThirdPartyService>();
            var mockLogger = new Mock<ILogger>();

            mockSettings.Setup(x => x.GetSettings()).Returns(expectedSettings);

            var controller = new NzbGetController(mockSettings.Object, mockApi.Object, mockLogger.Object);
            var result = (PartialViewResult)controller.GetNzbGetStatus();
            var model = result.ViewBag;


            Assert.That(model.Error, Is.Not.Null);
        }

        [Test]
        public void GetNzbGetLogsTest()
        {
            var expected = new NzbGetLogs
            {
                result =
                    new List<LogResult>
                    {
                        new LogResult { ID = 2, Kind = "WARNING", Text = "TEXT", Time = 22, },
                        new LogResult { ID = 9999, Kind = "WARNING", Text = "TEXT", Time = 1448544679, },
                    },
            };

            var ordered = expected.result.OrderByDescending(x => x.ID).ToList();

            var mockSettings = new Mock<ISettingsService<NzbGetSettingsDto>>();
            var mockApi = new Mock<IThirdPartyService>();
            var mockLogger = new Mock<ILogger>();

            mockSettings.Setup(x => x.GetSettings()).Returns(ExpectedSettings);
            mockApi.Setup(x => x.GetNzbGetLogs(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expected);


            var controller = new NzbGetController(mockSettings.Object, mockApi.Object, mockLogger.Object);
            var result = (PartialViewResult)controller.Logs();
            var model = (List<NzbGetLogViewModel>)result.Model;


            Assert.That(model[0].Id, Is.EqualTo(9999));
            Assert.That(model[0].Kind, Is.EqualTo(ordered[0].Kind));
            Assert.That(model[0].Text, Is.EqualTo(ordered[0].Text));
            Assert.That(model[0].Time, Is.EqualTo(new DateTime(2015, 11, 26, 13, 31, 19)));
        }

        [Test]
        public void GetNzbGetLogsNullResultTest()
        {
            var expected = new NzbGetLogs
            {
                result = null,
            };

            var mockSettings = new Mock<ISettingsService<NzbGetSettingsDto>>();
            var mockApi = new Mock<IThirdPartyService>();
            var mockLogger = new Mock<ILogger>();

            mockSettings.Setup(x => x.GetSettings()).Returns(ExpectedSettings);
            mockApi.Setup(x => x.GetNzbGetLogs(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expected);


            var controller = new NzbGetController(mockSettings.Object, mockApi.Object, mockLogger.Object);
            var result = (PartialViewResult)controller.Logs();
            var model = (List<NzbGetLogViewModel>)result.Model;


            Assert.That(model.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetNzbGetLogsNoConfigTest()
        {
            var expectedSettings = new NzbGetSettingsDto();

            var mockSettings = new Mock<ISettingsService<NzbGetSettingsDto>>();
            var mockLogger = new Mock<ILogger>();
            var mockApi = new Mock<IThirdPartyService>();

            mockSettings.Setup(x => x.GetSettings()).Returns(expectedSettings);


            var controller = new NzbGetController(mockSettings.Object, mockApi.Object, mockLogger.Object);
            var result = (PartialViewResult)controller.Logs();
            var model = result.ViewBag;

            Assert.That(model.Error, Is.Not.Null);
        }

        [Test]
        public void GetNzbGetDownloadInformation()
        {
            var f = new Fixture();
            var expectedApi = f.Create<NzbGetList>();
            var expectedStatus = f.Create<NzbGetStatus>();

            var mockSettings = new Mock<ISettingsService<NzbGetSettingsDto>>();
            var mockApi = new Mock<IThirdPartyService>();
            var mockLogger = new Mock<ILogger>();
            mockSettings.Setup(x => x.GetSettings()).Returns(ExpectedSettings).Verifiable();
            mockApi.Setup(x => x.GetNzbGetList(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expectedApi).Verifiable();
            mockApi.Setup(x => x.GetNzbGetStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expectedStatus).Verifiable();

            var controller = new NzbGetController(mockSettings.Object, mockApi.Object, mockLogger.Object);
            var result = (PartialViewResult)controller.GetNzbGetDownloadInformation();
            var model = (DownloaderViewModel)result.Model;

            Assert.That(result.Model, Is.TypeOf<DownloaderViewModel>());
            Assert.That(model.Application, Is.EqualTo(Applications.NzbGet));
            Assert.That(model.DownloadItem[0].DownloadingName, Is.EqualTo(expectedApi.result[0].NZBName));
            Assert.That(model.DownloadItem[0].NzbId, Is.EqualTo(expectedApi.result[0].NZBID));
        }

        [Test]
        public void GetNzbGetDownloadInformationNoConfig()
        {
            var badSettings = new NzbGetSettingsDto();

            var mockSettings = new Mock<ISettingsService<NzbGetSettingsDto>>();
            var mockApi = new Mock<IThirdPartyService>();
            var mockLogger = new Mock<ILogger>();
            mockSettings.Setup(x => x.GetSettings()).Returns(badSettings).Verifiable();

            var controller = new NzbGetController(mockSettings.Object, mockApi.Object, mockLogger.Object);
            var result = (PartialViewResult)controller.GetNzbGetDownloadInformation();
            var model = result.ViewBag;

            Assert.That(model.Error, Is.Not.Null);
        }
    }
}
