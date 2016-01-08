using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web.Mvc;

using NZBDash.Common;
using NZBDash.Common.Mapping;
using NZBDash.Core;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.Services;
using NZBDash.Core.SettingsService;
using NZBDash.DataAccessLayer.Configuration;
using NZBDash.DataAccessLayer.Models;
using NZBDash.DataAccessLayer.Models.Settings;
using NZBDash.DataAccessLayer.Repository;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.UI.Models.Dashboard;
using NZBDash.UI.Models.Hardware;

using Omu.ValueInjecter;

namespace NZBDash.UI.Controllers
{
    public class DashboardController : BaseController
    {
        private IThirdPartyService Api { get; set; }
        private IHardwareService Service { get; set; }
        private ILinksConfiguration LinksConfiguration { get; set; }

        public DashboardController(IHardwareService service, IThirdPartyService api, ILinksConfiguration linksConfiguration)
            : base(typeof(DashboardController))
        {
            Api = api;
            Service = service;
            LinksConfiguration = linksConfiguration;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDriveInformation()
        {
            var drives = Service.GetDrives();
            return PartialView("Partial/_DriveInformation", drives);
        }

        public ActionResult GetLinks()
        {
            var allLinks = LinksConfiguration.GetLinks();

            var model = allLinks.Select(link =>
                (DashboardLinksViewModel)new DashboardLinksViewModel().InjectFrom(link))
                .ToList();

            return PartialView("Partial/_Links", model);
        }

        public ActionResult GetRam()
        {
            var ramModel = Service.GetRam();

            return PartialView("Partial/_Ram", ramModel);
        }

        public ActionResult GetServerInformation()
        {
            var ramInfo = Service.GetRam();

            var model = new ServerInformationViewModel();
            model.InjectFromJson<ServerInformationViewModel>(ramInfo);
            model.CpuPercentage = Service.GetCpuPercentage();
            model.AvailableMemory = Service.GetAvailableRam();
            model.Uptime = Service.GetUpTime();


            return PartialView("Partial/_ServerInformation", model);
        }

        public ActionResult UpdateGrid(GridsterModel[] s)
        {
            var j = s;
            return Json("",JsonRequestBehavior.AllowGet);
        }
    }
}