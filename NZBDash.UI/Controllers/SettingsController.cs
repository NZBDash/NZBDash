#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: SettingsController.cs
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
using System.Web.Mvc;

using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.UI.Models.Settings;

using Omu.ValueInjecter;

namespace NZBDash.UI.Controllers
{
    public class SettingsController : BaseController
    {
        private ISettingsService<NzbGetSettingsDto> NzbGetSettingsServiceSettingsService { get; set; }
        private ISettingsService<SabNzbdSettingsDto> SabNzbSettingsServiceSettingsService { get; set; }
        private ISettingsService<SonarrSettingsViewModelDto> SonarrSettingsServiceSettingsService { get; set; }
        private ISettingsService<CouchPotatoSettingsDto> CpSettingsService { get; set; }
        private ISettingsService<PlexSettingsDto> PlexSettingsServiceSettingsService { get; set; }

        public SettingsController(ISettingsService<NzbGetSettingsDto> nzbGetSettingsService, ISettingsService<SabNzbdSettingsDto> sabNzbSettingsService, ISettingsService<SonarrSettingsViewModelDto> sonarSettingsService,
             ISettingsService<CouchPotatoSettingsDto> cpSettingsService, ISettingsService<PlexSettingsDto> plexSettingsService)
            : base(typeof(SettingsController))
        {
            NzbGetSettingsServiceSettingsService = nzbGetSettingsService;
            SabNzbSettingsServiceSettingsService = sabNzbSettingsService;
            SonarrSettingsServiceSettingsService = sonarSettingsService;
            CpSettingsService = cpSettingsService;
            PlexSettingsServiceSettingsService = plexSettingsService;
        }

        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NzbGetSettings()
        {
            Logger.Trace("Getting settings");
            var dto = NzbGetSettingsServiceSettingsService.GetSettings();

            Logger.Trace("Converting settings into ViewModel");
            var model = new NzbGetSettingsViewModel();
            model.InjectFrom(dto);

            Logger.Trace("returning ViewModel");
            return View(model);
        }

        [HttpPost]
        public ActionResult NzbGetSettings(NzbGetSettingsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var dto = new NzbGetSettingsDto();
            dto.InjectFrom(viewModel);

            var result = NzbGetSettingsServiceSettingsService.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("NzbGetSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult SabNzbSettings()
        {
            var dto = SabNzbSettingsServiceSettingsService.GetSettings();
            var model = new SabNzbSettingsViewModel();
            model.InjectFrom(dto);

            return View(model);
        }


        [HttpPost]
        public ActionResult SabNzbSettings(SabNzbSettingsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var dto = new SabNzbdSettingsDto();
            dto.InjectFrom(viewModel);

            var result = SabNzbSettingsServiceSettingsService.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("SabNzbSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult SonarrSettings()
        {
            var dto = SonarrSettingsServiceSettingsService.GetSettings();
            var model = new SonarrSettingsViewModel();
            model.InjectFrom(dto);

            return View(model);
        }

        [HttpPost]
        public ActionResult SonarrSettings(SonarrSettingsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var dto = new SonarrSettingsViewModelDto();
            dto.InjectFrom(viewModel);

            var result = SonarrSettingsServiceSettingsService.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("SonarrSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult CouchPotatoSettings()
        {
            var dto = CpSettingsService.GetSettings();
            var model = new CouchPotatoSettingsViewModel();
            model.InjectFrom(dto);
            
            return View(model);
        }

        [HttpPost]
        public ActionResult CouchPotatoSettings(CouchPotatoSettingsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var dto = new CouchPotatoSettingsDto();
            dto.InjectFrom(viewModel);

            var result = CpSettingsService.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("CouchPotatoSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult PlexSettings()
        {
            var dto = PlexSettingsServiceSettingsService.GetSettings();
            var model = new PlexSettingsViewModel();
            model.InjectFrom(dto);

            return View(model);
        }

        [HttpPost]
        public ActionResult PlexSettings(PlexSettingsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var dto = new PlexSettingsDto();
            dto.InjectFrom(viewModel);

            var result = PlexSettingsServiceSettingsService.SaveSettings(dto);
            if (result)
            {   
                return RedirectToAction("PlexSettings");
            }

            return View("Error");
        }
    }
}