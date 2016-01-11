#region Copyright
// /************************************************************************
//   Copyright (c) 2015 Jamie Rees
//   File: SettingsServiceTest.cs
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
using Moq;
using NUnit.Framework;
using NZBDash.Common.Interfaces;
using NZBDash.Common.Models.Settings;
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
        private Mock<ILogger> logger { get; set; }
        private Fixture F { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mockRepo = new Mock<ISettingsRepository>();
            logger = new Mock<ILogger>();
            ExpectedLink = new GlobalSettings
            {
                Id = 1,
                Content = "oDuq+yWcOzb7qgRvmyljVAS6dLdT1fydgeyMuObYkYJwzTNTXlwC4+V/Tp0O6FZuKfKbDuaonSXMzg8ndZhM118am2HiobAd37KrCFcb7X594TmzrHUFCuqflHPOl3FsZnKnZXqsvABt6QCnFZLb3eG6smE/uqQ5QVohtt2qzi8ZrGifb5pjXJIfSDwnXGj951zKfxQ7Wq84QmAUU5eqPdDBq2ODsLnRfVamrieVPIxzhhaFFKLJF9QvAE8SPm4i",
                SettingsName = "PageName",
            };

            F = new Fixture();
            ExpectedGetLinks = F.CreateMany<GlobalSettings>().ToList();


            mockRepo.Setup(x => x.GetAll()).Returns(ExpectedGetLinks).Verifiable();

            mockRepo.Setup(x => x.Get(It.IsAny<string>())).Returns(ExpectedLink).Verifiable();

            mockRepo.Setup(x => x.Update(It.IsAny<GlobalSettings>())).Returns(true).Verifiable();

            mockRepo.Setup(x => x.Insert(It.IsAny<GlobalSettings>())).Returns(1).Verifiable();


            MockRepo = mockRepo;
            Service = new SettingsService<NzbDashSettings, Setting>(MockRepo.Object, logger.Object);
        }

        [Test]
        public void GetNullSettings()
        {
            var mockRepo = new Mock<ISettingsRepository>();
            mockRepo.Setup(x => x.Get("Test")).Returns(ExpectedLink).Verifiable();
            MockRepo = mockRepo;
            
            Service = new SettingsService<NzbDashSettings, Setting>(MockRepo.Object, logger.Object);

            var result = Service.GetSettings();

            Assert.That(result.Id, Is.EqualTo(0));
            MockRepo.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetSettings()
        {
            var result = Service.GetSettings();

            Assert.That(result, Is.Not.Null);
            MockRepo.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void SaveNewSetting()
        {
            var model = F.Create<Setting>();
            var result = Service.SaveSettings(model);

            Assert.That(result, Is.True);
            MockRepo.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
            MockRepo.Verify(x => x.Update(It.IsAny<GlobalSettings>()), Times.Once);
            MockRepo.Verify(x => x.Insert(It.IsAny<GlobalSettings>()), Times.Never);
        }

        [Test]
        public void SaveExisitingSetting()
        {
            var mockRepo = new Mock<ISettingsRepository>();
            mockRepo.Setup(x => x.Get("Test")).Returns(ExpectedLink).Verifiable();
            MockRepo = mockRepo;

            Service = new SettingsService<NzbDashSettings, Setting>(MockRepo.Object, logger.Object);


            var model = F.Create<Setting>();
            var result = Service.SaveSettings(model);

            Assert.That(result, Is.True);
            MockRepo.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
            MockRepo.Verify(x => x.Update(It.IsAny<GlobalSettings>()), Times.Never);
            MockRepo.Verify(x => x.Insert(It.IsAny<GlobalSettings>()), Times.Once);
        }
    }
}
