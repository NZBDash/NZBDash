#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: SonarrEpisodeMapperTest.cs
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
using System.Diagnostics.CodeAnalysis;

using NUnit.Framework;

using NZBDash.Common.Mapping;
using NZBDash.ThirdParty.Api.Models.Api.Sonarr;
using NZBDash.UI.Models.ViewModels.Sonarr;

using Omu.ValueInjecter;

namespace NZBDash.Common.Tests.Mapping
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class SonarrEpisodeMapperTest
    {
        [TestCaseSource("TestCaseData")]
        [Ignore("https://ci.appveyor.com/project/tidusjar/nzbdash/build/1.0.182")]
        public void SonarrEpisodeMapper(int id, int seriesId, int epiNum, string airDate, string airDateUtc, bool downloading,
           int episodeFileId, int episodenumber, bool hasfile, bool monitoredBool, string overView, int sceneEpiNumber,
            int sceneSeasonNum, int seasonNum, string titleString, int tvdbId)
        {
            var source = new SonarrEpisode
            {
                id = id,
                seriesId = seriesId,
                absoluteEpisodeNumber = epiNum,
                airDate = airDate,
                airDateUtc = airDateUtc,
                downloading = downloading,
                episodeFileId = episodeFileId,
                episodeNumber = episodenumber,
                hasFile = hasfile,
                monitored = monitoredBool,
                overview = overView,
                sceneEpisodeNumber = sceneEpiNumber,
                sceneSeasonNumber = sceneSeasonNum,
                seasonNumber = seasonNum,
                title = titleString,
                tvDbEpisodeId = tvdbId
            };

            var target = new EpisodeViewModel();
            target.InjectFrom(new SonarrEpisodeMapper(), source);

            Assert.That(target.ID, Is.EqualTo(id));
            Assert.That(target.SeriesId, Is.EqualTo(seriesId));
            Assert.That(target.AbsoluteEpisodeNumber, Is.EqualTo(epiNum));
            Assert.That(target.AirDate, Is.EqualTo(new DateTime(2009,09,17)));
            Assert.That(target.AirDateUtc, Is.EqualTo(new DateTime(2009,09,18,03,00,00)));
            Assert.That(target.Downloading, Is.EqualTo(downloading));
            Assert.That(target.EpisodeFileId, Is.EqualTo(episodeFileId));
            Assert.That(target.EpisodeNumber, Is.EqualTo(episodenumber));
            Assert.That(target.HasFile, Is.EqualTo(hasfile));
            Assert.That(target.Monitored, Is.EqualTo(monitoredBool));
            Assert.That(target.Overview, Is.EqualTo(overView));
            Assert.That(target.SceneEpisodeNumber, Is.EqualTo(sceneEpiNumber));
            Assert.That(target.SceneSeasonNumber, Is.EqualTo(sceneSeasonNum));
            Assert.That(target.SeasonNumber, Is.EqualTo(seasonNum));
            Assert.That(target.Title, Is.EqualTo(titleString));
            Assert.That(target.TvDbEpisodeId, Is.EqualTo(tvdbId));

        }

        private static IEnumerable<ITestCaseData> TestCaseData
        {
            get
            {
                var emptyStrings = new string[0];

                yield return new TestCaseData(1, 22, 33,
                    "2009-09-17", "2009-09-18T02:00:00Z",
                    true,
                    22,
                    23,
                    false,
                    true,
                    "overview",
                    34,
                    55,
                    2,
                    "title",
                    213
                    );

            }
        }
    }
}
