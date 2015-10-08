using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using NZBDash.Common;
using NZBDash.Core.Configuration;
using NZBDash.Core.Model.DTO;
using NZBDash.UI.Helpers;
using NZBDash.UI.Models;

namespace NZBDash.UI.Controllers
{
    public class ApplicationConfigurationController : BaseController
    {
        public ApplicationConfigurationController()
            : base(typeof(ApplicationConfigurationController))
        {
            
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var config = new AdminConfiguration();
            var result = await config.GetApplicationSettingsAsync();
            var supported = await config.GetSupportedApplicationsAsync();
            var item = supported.Select(supp => new SelectListItem { Text = supp.Name, Value = ((int) EnumHelper<Applications>.Parse(supp.Name)).ToString(CultureInfo.CurrentUICulture) }).ToList();

            var applications = new SelectList(item, "Value", "Text");

            ViewBag.Applications = applications;
            // We don't yet have any application configuration
            if (result == null)
            {
                var emptyModel = new List<ApplicationConfigurationViewModel>();
                return View(emptyModel);
            }

            var model = result
                .Select(app => new ApplicationConfigurationViewModel
                {
                    Id = app.Id,
                    Password = app.Password,
                    Username = app.Username,
                    IpAddress = app.IpAddress,
                    ApiKey = app.ApiKey,
                    ApplicationName = app.ApplicationName
                }).ToList();

             
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateApplicationConfiguation(ApplicationConfigurationViewModel config)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var admin = new AdminConfiguration();
            var dto = MapToDto(config);
            var result = await admin.UpdateApplicationConfigurationAsync(dto);
            if (result)
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }

        [HttpGet]
        public async Task<ActionResult> GetApplicationConfiguration(int id)
        {
            var config = new AdminConfiguration();
            var result = await config.GetApplicationSettingsAsync();

            var record = result.FirstOrDefault(x => x.Id == id);

            var model = MapToView(record);
            return PartialView("_Application", model);
        }

        public static ApplicationConfigurationDto MapToDto(ApplicationConfigurationViewModel model)
        {
            var dto = new ApplicationConfigurationDto
            {
                Id = model.Id,
                Password = model.Password,
                Username = model.Username,
                ApiKey = model.ApiKey,
                IpAddress = model.IpAddress,
                ApplicationName = model.ApplicationName,
                ApplicationId = model.ApplicationId
            };

            return dto;
        }

        public static ApplicationConfigurationViewModel MapToView(ApplicationConfigurationDto model)
        {
            var dto = new ApplicationConfigurationViewModel
            {
                Id = model.Id,
                Password = model.Password,
                Username = model.Username,
                ApiKey = model.ApiKey,
                IpAddress = model.IpAddress,
                ApplicationName = model.ApplicationName,
                ApplicationId = model.ApplicationId,
            };

            return dto;
        }
    }
}