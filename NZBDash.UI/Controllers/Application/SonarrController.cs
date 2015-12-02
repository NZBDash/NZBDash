#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: SonarrController.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using NZBDash.Common.Mapping;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.UI.Models.ViewModels.Sonarr;

using Omu.ValueInjecter;

using UrlHelper = NZBDash.Common.Helpers.UrlHelper;

namespace NZBDash.UI.Controllers.Application
{
    public class SonarrController : BaseController
    {
        private IThirdPartyService ApiService { get; set; }
        private ISettingsService<SonarrSettingsViewModelDto> SettingsService { get; set; }
        private SonarrSettingsViewModelDto Settings { get; set; }

        public SonarrController(IThirdPartyService apiService, ISettingsService<SonarrSettingsViewModelDto> settingsService)
            : base(typeof(SonarrController))
        {
            ApiService = apiService;
            SettingsService = settingsService;
            Settings = SettingsService.GetSettings();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetSeries()
        {
            if (!Settings.HasSettings)
            {
                ViewBag.Error = Resources.Resources.Settings_Missing_Sonarr;
                return PartialView("DashletError");
            }

            var formattedUri = UrlHelper.ReturnUri(Settings.IpAddress, Settings.Port).ToString();

            var series = ApiService.GetSonarrSeries(formattedUri, Settings.ApiKey);
            var viewModel = new List<SonarrSeriesViewModel>();

            foreach (var s in series)
            {
                var seriesViewModel = new SonarrSeriesViewModel();
                var mappedResult = (SonarrSeriesViewModel)seriesViewModel
                    .InjectFrom(new JsonSerializerTargetMapper<SonarrSeriesViewModel>(), s);

                var images = s.images.Select(x => x.url);
                if (images.Any())
                {
                    mappedResult.ImageUrls = new List<string>();
                    foreach (var format in images.Select(image => string.Format("{0}{1}", formattedUri, image)))
                    {
                        mappedResult.ImageUrls.Add(format);
                    }
                }
                viewModel.Add(mappedResult);
            }

            return PartialView("SeriesList", viewModel);
        }

        [HttpGet]
        public ActionResult GetEpisodes(int id)
        {
            if (!Settings.HasSettings)
            {
                ViewBag.Error = Resources.Resources.Settings_Missing_Sonarr;
                return PartialView("DashletError");
            }

            var formattedUri = UrlHelper.ReturnUri(Settings.IpAddress, Settings.Port).ToString();

            var episodes = ApiService.GetSonarrEpisodes(formattedUri, Settings.ApiKey, id);
            var viewModel = new List<SonarrEpisodeViewModel>();
            foreach (var e in episodes)
            {
                var episodeViewModel = new SonarrEpisodeViewModel();
                var mappedResult = (SonarrEpisodeViewModel)episodeViewModel.InjectFrom(new SonarrEpisodeMapper(), e);

                viewModel.Add(mappedResult);
            }

            return PartialView("Episodes", viewModel);
        }
    }
}