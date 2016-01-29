#region Copyright
// /************************************************************************
//   Copyright (c) 2016 Jamie Rees
//   File: SettingsController.cs
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
using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.Models.Settings;
using NZBDash.UI.Models.ViewModels.Settings;

using Omu.ValueInjecter;

namespace NZBDash.UI.Controllers
{
    public class SettingsController : BaseController
    {
        public SettingsController(ISettingsService<NzbGetSettingsDto> nzbGetSettingsService,
                                  ISettingsService<SabNzbdSettingsDto> sabNzbSettingsService,
                                  ISettingsService<SonarrSettingsDto> sonarSettingsService,
                                  ISettingsService<CouchPotatoSettingsDto> cpSettingsService,
                                  ISettingsService<PlexSettingsDto> plexSettingsService,
                                  ISettingsService<NzbDashSettingsDto> nzbDash,
                                  IAuthenticationService auth,
                                  ISettingsService<HardwareSettingsDto> hardwareService,
                                  IHardwareService hardware,
                                  ILogger logger)
            : base(logger)
        {
            NzbGetSettingsService = nzbGetSettingsService;
            SabNzbSettingsService = sabNzbSettingsService;
            SonarrSettingsService = sonarSettingsService;
            CpSettingsService = cpSettingsService;
            PlexSettingsService = plexSettingsService;
            NzbDashServiceSettings = nzbDash;
            Auth = auth;
            HardwareSettingsService = hardwareService;
            HardwareService = hardware;
        }

        private ISettingsService<CouchPotatoSettingsDto> CpSettingsService { get; set; }
        private ISettingsService<NzbDashSettingsDto> NzbDashServiceSettings { get; set; }
        private ISettingsService<NzbGetSettingsDto> NzbGetSettingsService { get; set; }
        private ISettingsService<PlexSettingsDto> PlexSettingsService { get; set; }
        private ISettingsService<SabNzbdSettingsDto> SabNzbSettingsService { get; set; }
        private ISettingsService<SonarrSettingsDto> SonarrSettingsService { get; set; }
        private ISettingsService<HardwareSettingsDto> HardwareSettingsService { get; set; }
        private IHardwareService HardwareService { get; set; }
        private IAuthenticationService Auth { get; set; }

        [HttpGet]
        public ActionResult CouchPotatoSettings()
        {
            return Get<CouchPotatoSettingsViewModel, CouchPotatoSettingsDto>(CpSettingsService);
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

        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NzbDashSettings()
        {
            return Get<NzbDashSettingsViewModel, NzbDashSettingsDto>(NzbDashServiceSettings);
        }

        [HttpPost]
        public ActionResult NzbDashSettings(NzbDashSettingsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = new NzbDashSettingsDto();
            dto.InjectFrom(model);

            var result = NzbDashServiceSettings.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("NzbDashSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult NzbGetSettings()
        {
            return Get<NzbGetSettingsViewModel, NzbGetSettingsDto>(NzbGetSettingsService);
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

            var result = NzbGetSettingsService.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("NzbGetSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult PlexSettings()
        {
            return Get<PlexSettingsViewModel, PlexSettingsDto>(PlexSettingsService);
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

            var result = PlexSettingsService.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("PlexSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult SabNzbSettings()
        {
            return Get<SabNzbSettingsViewModel, SabNzbdSettingsDto>(SabNzbSettingsService);
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

            var result = SabNzbSettingsService.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("SabNzbSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult SonarrSettings()
        {
            return Get<SonarrSettingsViewModel, SonarrSettingsDto>(SonarrSettingsService);
        }

        [HttpPost]
        public ActionResult SonarrSettings(SonarrSettingsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var dto = new SonarrSettingsDto();
            dto.InjectFrom(viewModel);

            var result = SonarrSettingsService.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("SonarrSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult HardwareSettings()
        {
            var dto = HardwareSettingsService.GetSettings();
            var model = Mapper.Map<HardwareSettingsViewModel>(dto);

            // Get the drives and drive information
            var drives = HardwareService.GetDrives().ToList();
            var nics = HardwareService.GetAllNics();
            model.Drives = new List<DriveSettingsViewModel>();

            foreach (var d in drives)
            {
                var driveModel = new DriveSettingsViewModel();
                driveModel.InjectFrom(d);
                model.Drives.Add(driveModel);
            }

            model.NetworkMonitoring.NicDict = new Dictionary<string, int>();
            var ddlNics = new List<string>();
            foreach (var nic in nics)
            {
                model.NetworkMonitoring.NicDict.Add(nic.Key,nic.Value);
                ddlNics.Add(nic.Key);
            }
            model.NetworkMonitoring.Nics = new SelectList(model.NetworkMonitoring.NicDict, "Value","Key",1);
            
            return View(model);
        }

        [HttpPost]
        public ActionResult HardwareSettings(HardwareSettingsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var dto = Mapper.Map<HardwareSettingsDto>(viewModel);
            
            var result = HardwareSettingsService.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("HardwareSettings");
            }

            return View("Error");
        }

        /// <summary>
        /// Gets the specified data from the specified service.
        /// </summary>
        /// <typeparam name="T">The ViewModel you want to return to the View</typeparam>
        /// <typeparam name="U">The DTO returned by the service</typeparam>
        /// <param name="service">The service.</param>
        /// <returns>Default view with the model</returns>
        private ActionResult Get<T, U>(ISettingsService<U> service) where T : new()
        {
            var dto = service.GetSettings();
            var model = new T();
            model.InjectFrom(dto);

            return View(model);
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            // Create an empty list to hold result of the operation

            // For each string in the 'elements' variable, create a new SelectListItem object
            // that has both its Value and Text properties set to a particular value.
            // This will result in MVC rendering each item as:
            //     <option value="State Name">State Name</option>

            return elements.Select(element => new SelectListItem
            {
                Value = element, Text = element
            }).ToList();
        }
    }
}