#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: SonarrControllerTests.cs
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

using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.ThirdParty.Api.Models.Api.Sonarr;
using NZBDash.UI.Controllers.Application;
using NZBDash.UI.Models.ViewModels.Sonarr;

using Ploeh.AutoFixture;
using UrlHelper = NZBDash.Common.Helpers.UrlHelper;

namespace NZBDash.UI.Test.Controllers
{
    [TestFixture]
    public class SonarrControllerTests
    {
        private SonarrController _controller;
        private SonarrSettingsViewModelDto ExpectedSettings { get; set; }
        private List<SonarrSeries> SonarrSeries { get; set; }
        private List<SonarrEpisode> SonarrEpisode { get; set; }
        private Mock<ISettingsService<SonarrSettingsViewModelDto>> SettingsMock { get; set; }
        private Mock<IThirdPartyService> ServiceMock { get; set; }

        [SetUp]
        public void Setup()
        {
            SettingsMock = new Mock<ISettingsService<SonarrSettingsViewModelDto>>();
            ServiceMock = new Mock<IThirdPartyService>();
            var f = new Fixture();

            ExpectedSettings = f.Create<SonarrSettingsViewModelDto>();
            SonarrSeries = f.CreateMany<SonarrSeries>().ToList();
            SonarrEpisode = f.CreateMany<SonarrEpisode>().ToList();


            SettingsMock.Setup(x => x.GetSettings()).Returns(ExpectedSettings);
            ServiceMock.Setup(x => x.GetSonarrSeries(It.IsAny<string>(), It.IsAny<string>())).Returns(SonarrSeries);
            ServiceMock.Setup(x => x.GetSonarrEpisodes(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(SonarrEpisode);
            _controller = new SonarrController(ServiceMock.Object, SettingsMock.Object);
        }

        [Test]
        public void GetSeries()
        {
            var series = (PartialViewResult)_controller.GetSeries();
            var model = (List<SonarrSeriesViewModel>)series.Model;

            DateTime newDate;
            DateTime.TryParse(SonarrSeries[0].firstAired, out newDate);
   

            Assert.That(model.Count, Is.GreaterThan(0));
            Assert.That(model[0].Id, Is.EqualTo(SonarrSeries[0].id));
            Assert.That(model[0].ImdbId, Is.EqualTo(SonarrSeries[0].imdbId));
            Assert.That(model[0].Monitored, Is.EqualTo(SonarrSeries[0].monitored));
            Assert.That(model[0].FirstAired, Is.EqualTo(newDate.ToString("D")));
            Assert.That(model[0].EpisodeCount, Is.EqualTo(SonarrSeries[0].episodeCount));
            Assert.That(model[0].CleanTitle, Is.EqualTo(SonarrSeries[0].cleanTitle));
            Assert.That(model[0].AirTime, Is.EqualTo(SonarrSeries[0].airTime));
            Assert.That(model[0].Network, Is.EqualTo(SonarrSeries[0].network));
            Assert.That(model[0].NextAiring, Is.EqualTo(SonarrSeries[0].nextAiring));
            Assert.That(model[0].Overview, Is.EqualTo(SonarrSeries[0].overview));
            Assert.That(model[0].Path, Is.EqualTo(SonarrSeries[0].path));
            Assert.That(model[0].QualityProfileId, Is.EqualTo(SonarrSeries[0].qualityProfileId));
            Assert.That(model[0].Runtime, Is.EqualTo(SonarrSeries[0].runtime));
            Assert.That(model[0].SeasonCount, Is.EqualTo(SonarrSeries[0].seasonCount));
            Assert.That(model[0].SeasonFolder, Is.EqualTo(SonarrSeries[0].seasonFolder));
            Assert.That(model[0].SeriesType, Is.EqualTo(SonarrSeries[0].seriesType));
            Assert.That(model[0].Status, Is.EqualTo(SonarrSeries[0].status));
            Assert.That(model[0].Title, Is.EqualTo(SonarrSeries[0].title));
            Assert.That(model[0].TitleSlug, Is.EqualTo(SonarrSeries[0].titleSlug));
            Assert.That(model[0].Year, Is.EqualTo(SonarrSeries[0].year));
            Assert.That(model[0].ImageUrls[0], Is.EqualTo(UrlHelper.ReturnUri(ExpectedSettings.IpAddress, ExpectedSettings.Port) + SonarrSeries[0].images[0].url));

            ServiceMock.Verify(x => x.GetSonarrSeries(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            SettingsMock.Verify(x => x.GetSettings(), Times.Once);
        }

        [Test]
        public void GetSeriesNoConfig()
        {
            SettingsMock = new Mock<ISettingsService<SonarrSettingsViewModelDto>>();
            ServiceMock = new Mock<IThirdPartyService>();
            var f = new Fixture();

            ExpectedSettings = new SonarrSettingsViewModelDto();
            SonarrSeries = f.CreateMany<SonarrSeries>().ToList();
            SonarrEpisode = f.CreateMany<SonarrEpisode>().ToList();


            SettingsMock.Setup(x => x.GetSettings()).Returns(ExpectedSettings);
            ServiceMock.Setup(x => x.GetSonarrSeries(It.IsAny<string>(), It.IsAny<string>())).Returns(SonarrSeries);
            ServiceMock.Setup(x => x.GetSonarrEpisodes(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(SonarrEpisode);
            _controller = new SonarrController(ServiceMock.Object, SettingsMock.Object);

            var series = (PartialViewResult)_controller.GetSeries();
            var model = series.ViewBag;

            Assert.That(model.Error, Is.Not.Null);
        }

        [Test]
        public void GetEpisodesForSeries()
        {
            var series = (PartialViewResult)_controller.GetEpisodes(1, "stringTitle");
            var model = (SonarrEpisodeViewModel)series.Model;

            Assert.That(model.EpisodeViewModels.Count, Is.GreaterThan(0));
            Assert.That(model.SeasonTitle, Is.EqualTo("stringTitle"));
            Assert.That(model.EpisodeViewModels[SonarrEpisode[0].seasonNumber][0].Monitored, Is.EqualTo(SonarrEpisode[0].monitored));
            Assert.That(model.EpisodeViewModels[SonarrEpisode[0].seasonNumber][0].Overview, Is.EqualTo(SonarrEpisode[0].overview));
            Assert.That(model.EpisodeViewModels[SonarrEpisode[0].seasonNumber][0].Title, Is.EqualTo(SonarrEpisode[0].title));
            Assert.That(model.EpisodeViewModels[SonarrEpisode[0].seasonNumber][0].AbsoluteEpisodeNumber, Is.EqualTo(SonarrEpisode[0].absoluteEpisodeNumber));
            Assert.That(model.EpisodeViewModels[SonarrEpisode[0].seasonNumber][0].Downloading, Is.EqualTo(SonarrEpisode[0].downloading));
            Assert.That(model.EpisodeViewModels[SonarrEpisode[0].seasonNumber][0].EpisodeFileId, Is.EqualTo(SonarrEpisode[0].episodeFileId));
            Assert.That(model.EpisodeViewModels[SonarrEpisode[0].seasonNumber][0].HasFile, Is.EqualTo(SonarrEpisode[0].hasFile));
            Assert.That(model.EpisodeViewModels[SonarrEpisode[0].seasonNumber][0].ID, Is.EqualTo(SonarrEpisode[0].id));
            Assert.That(model.EpisodeViewModels[SonarrEpisode[0].seasonNumber][0].SeasonNumber, Is.EqualTo(SonarrEpisode[0].seasonNumber));


            ServiceMock.Verify(x => x.GetSonarrEpisodes(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once);
            SettingsMock.Verify(x => x.GetSettings(), Times.Once);
        }

        [Test]
        public void GetEpisodesForSeriesNoConfig()
        {
            SettingsMock = new Mock<ISettingsService<SonarrSettingsViewModelDto>>();
            ServiceMock = new Mock<IThirdPartyService>();
            var f = new Fixture();

            ExpectedSettings = new SonarrSettingsViewModelDto();
            SonarrSeries = f.CreateMany<SonarrSeries>().ToList();
            SonarrEpisode = f.CreateMany<SonarrEpisode>().ToList();


            SettingsMock.Setup(x => x.GetSettings()).Returns(ExpectedSettings);
            ServiceMock.Setup(x => x.GetSonarrSeries(It.IsAny<string>(), It.IsAny<string>())).Returns(SonarrSeries);
            ServiceMock.Setup(x => x.GetSonarrEpisodes(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(SonarrEpisode);
            _controller = new SonarrController(ServiceMock.Object, SettingsMock.Object);

            var series = (PartialViewResult)_controller.GetEpisodes(1, "title");
            var model = series.ViewBag;

            Assert.That(model.Error, Is.Not.Null);
        }
    }
}
