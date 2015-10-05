using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NZBDash.Core;
using NZBDash.Core.Model.Settings;
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
            var save = new SettingsSaver();
            var dto = save.GetNzbGetSettings();
            var model = new NzbGetSettingsViewModel
            {
                Password = dto.Password,
                Port = dto.Port,
                Username = dto.Username,
                Enabled = dto.Enabled,
                Id = dto.Id,
                IpAddress = dto.IpAddress,
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult NzbGetSettings(NzbGetSettingsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            var dto = new NzbGetSettingsDto
            {
                IpAddress = viewModel.IpAddress,
                Password = viewModel.Password,
                Port = viewModel.Port,
                Username = viewModel.Username,
                Enabled = viewModel.Enabled,
                Id = viewModel.Id
            };

            var save = new SettingsSaver();
            var result = save.SaveNzbGetSettings(dto);
            if (result)
            {
                RedirectToAction("GetSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult SabNzbSettings()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SonarrSettings()
        {
            var save = new SettingsSaver();
            var dto = save.GetSonarrSettings();
            var model = new SonarrSettingsViewModel
            {
                Port = dto.Port,
                Enabled = dto.Enabled,
                Id = dto.Id,
                IpAddress = dto.IpAddress,
                ApiKey = dto.ApiKey
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult SonarrSettings(SonarrSettingsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            var dto = new SonarrSettingsViewModelDto
            {
                IpAddress = viewModel.IpAddress,
                Port = viewModel.Port,
                Enabled = viewModel.Enabled,
                Id = viewModel.Id,
                ApiKey = viewModel.ApiKey
            };

            var save = new SettingsSaver();
            var result = save.SaveSonarrSettings(dto);
            if (result)
            {
                RedirectToAction("GetSettings");
            }

            return View("Error");
        }

        [HttpGet]
        public ActionResult CouchPotatoSettings()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PlexSettings()
        {
            return View();
        }
    }
}