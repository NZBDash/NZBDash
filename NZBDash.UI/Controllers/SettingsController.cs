using System.Web.Mvc;
using System.Web.Services.Description;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.Settings;
using NZBDash.UI.Models.Settings;

namespace NZBDash.UI.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NzbGetSettings()
        {
            var save = new NzbGetSettingsConfiguration();
            var dto = save.GetSettings();
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

            var save = new NzbGetSettingsConfiguration();
            var result = save.SaveSettings(dto);
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

            var save = new SabNzbSettingsConfiguration();
            var result = save.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("SabNzbSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult SonarrSettings()
        {
            var save = new SonarrSettingsConfiguration();
            var dto = save.GetSettings();
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

            var save = new SonarrSettingsConfiguration();
            var result = save.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("SonarrSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult CouchPotatoSettings()
        {
            var save = new CouchPotatoSettingsConfiguration();
            var dto = save.GetSettings();
            var model = new CouchPotatoSettingsViewModel
            {
                Port = dto.Port,
                Enabled = dto.Enabled,
                Id = dto.Id,
                IpAddress = dto.IpAddress,
                ApiKey = dto.ApiKey,
                ShowOnDashboard = dto.ShowOnDashboard,
                Password = dto.Password,
                Username = dto.Username
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

            var save = new CouchPotatoSettingsConfiguration();
            var result = save.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("CouchPotatoSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult PlexSettings()
        {
            var save = new PlexSettingsConfiguration();
            var dto = save.GetSettings();
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

            var save = new PlexSettingsConfiguration();
            var result = save.SaveSettings(dto);
            if (result)
            {
                return RedirectToAction("PlexSettings");
            }

            return View("Error");
        }
    }
}