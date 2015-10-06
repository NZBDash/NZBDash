using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NZBDash.Core;
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
                RedirectToAction("NzbGetSettings");
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
                RedirectToAction("SabNzbSettings");
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
                RedirectToAction("SonarrSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult CouchPotatoSettings()
        {
            var save = new CouchPotatoSettingsConfiguration();
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

        [HttpGet]
        public ActionResult PlexSettings()
        {
            return View();
        }
    }
}