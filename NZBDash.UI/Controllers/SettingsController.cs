using System.Web.Mvc;

using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.Settings;
using NZBDash.UI.Models.Settings;

namespace NZBDash.UI.Controllers
{
    public class SettingsController : BaseController
    {
        private ISettings<NzbGetSettingsDto> NzbGetSettingsSettings { get; set; }
        private ISettings<SabNzbSettingsDto> SabNzbSettingsSettings { get; set; }
        private ISettings<SonarrSettingsViewModelDto> SonarrSettingsSettings { get; set; }
        private ISettings<CouchPotatoSettingsDto> CpSettings { get; set; }
        private ISettings<PlexSettingsDto> PlexSettingsSettings { get; set; }

        public SettingsController(ISettings<NzbGetSettingsDto> nzbGetSettings, ISettings<SabNzbSettingsDto> sabNzbSettings, ISettings<SonarrSettingsViewModelDto> sonarSettings,
             ISettings<CouchPotatoSettingsDto> cpSettings, ISettings<PlexSettingsDto> plexSettings)
            : base(typeof(SettingsController))
        {
            NzbGetSettingsSettings = nzbGetSettings;
            SabNzbSettingsSettings = sabNzbSettings;
            SonarrSettingsSettings = sonarSettings;
            CpSettings = cpSettings;
            PlexSettingsSettings = plexSettings;
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
            var dto = NzbGetSettingsSettings.GetSettings();

            Logger.Trace("Converting settings into ViewModel");
            var model = new NzbGetSettingsViewModel
            {
                Password = dto.Password,
                Port = dto.Port,
                Username = dto.Username,
                Enabled = dto.Enabled,
                Id = dto.Id,
                IpAddress = dto.IpAddress,
                ShowOnDashboard = dto.ShowOnDashboard
            };

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

            var dto = new NzbGetSettingsDto
            {
                IpAddress = viewModel.IpAddress,
                Password = viewModel.Password,
                Port = viewModel.Port,
                Username = viewModel.Username,
                Enabled = viewModel.Enabled,
                Id = viewModel.Id,
                ShowOnDashboard = viewModel.ShowOnDashboard
            };

            var result = NzbGetSettingsSettings.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("NzbGetSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult SabNzbSettings()
        {
            var save = new SabNzbSettingsConfiguration();
            var dto = save.GetSettings();
            var model = new SabNzbSettingsViewModel
            {
                ApiKey = dto.ApiKey,
                Port = dto.Port,
                Enabled = dto.Enabled,
                Id = dto.Id,
                IpAddress = dto.IpAddress,
                ShowOnDashboard = dto.ShowOnDashboard
            };

            return View(model);
        }


        [HttpPost]
        public ActionResult SabNzbSettings(SabNzbSettingsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var dto = new SabNzbSettingsDto
            {
                IpAddress = viewModel.IpAddress,
                Port = viewModel.Port,
                Enabled = viewModel.Enabled,
                Id = viewModel.Id,
                ApiKey = viewModel.ApiKey,
                ShowOnDashboard = viewModel.ShowOnDashboard
            };

            var result = SabNzbSettingsSettings.SaveSettings(dto);
            if (result)
            {
               return RedirectToAction("SabNzbSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult SonarrSettings()
        {
            var dto = SonarrSettingsSettings.GetSettings();
            var model = new SonarrSettingsViewModel
            {
                Port = dto.Port,
                Enabled = dto.Enabled,
                Id = dto.Id,
                IpAddress = dto.IpAddress,
                ApiKey = dto.ApiKey,
                ShowOnDashboard = dto.ShowOnDashboard
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult SonarrSettings(SonarrSettingsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var dto = new SonarrSettingsViewModelDto
            {
                IpAddress = viewModel.IpAddress,
                Port = viewModel.Port,
                Enabled = viewModel.Enabled,
                Id = viewModel.Id,
                ApiKey = viewModel.ApiKey,
                ShowOnDashboard = viewModel.ShowOnDashboard
            };

            var result = SonarrSettingsSettings.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("SonarrSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult CouchPotatoSettings()
        {
            var dto = CpSettings.GetSettings();
            var model = new CouchPotatoSettingsViewModel
            {
                Port = dto.Port,
                Enabled = dto.Enabled,
                Id = dto.Id,
                IpAddress = dto.IpAddress,
                ApiKey = dto.ApiKey,
                ShowOnDashboard = dto.ShowOnDashboard,
                Username = dto.Username,
                Password = dto.Password
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult CouchPotatoSettings(CouchPotatoSettingsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var dto = new CouchPotatoSettingsDto
            {
                IpAddress = viewModel.IpAddress,
                Port = viewModel.Port,
                Enabled = viewModel.Enabled,
                Id = viewModel.Id,
                ApiKey = viewModel.ApiKey,
                ShowOnDashboard = viewModel.ShowOnDashboard,
                Username = viewModel.Username,
                Password = viewModel.Password
            };

            var result = CpSettings.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("CouchPotatoSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult PlexSettings()
        {
            var dto = PlexSettingsSettings.GetSettings();
            var model = new PlexSettingsViewModel
            {
                Port = dto.Port,
                Enabled = dto.Enabled,
                Id = dto.Id,
                IpAddress = dto.IpAddress,
                ShowOnDashboard = dto.ShowOnDashboard,
                Username = dto.Username,
                Password = dto.Password
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult PlexSettings(PlexSettingsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var dto = new PlexSettingsDto
            {
                IpAddress = viewModel.IpAddress,
                Port = viewModel.Port,
                Enabled = viewModel.Enabled,
                Id = viewModel.Id,
                ShowOnDashboard = viewModel.ShowOnDashboard,
                Username = viewModel.Username,
                Password = viewModel.Password
            };

            var result = PlexSettingsSettings.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("PlexSettings");
            }

            return View("Error");
        }
    }
}