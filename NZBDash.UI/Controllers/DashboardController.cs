using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using NZBDash.Common.Helpers;
using NZBDash.Common.Interfaces;
using NZBDash.Common.Mapping;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.UI.Helpers;
using NZBDash.UI.Models;
using NZBDash.UI.Models.Dashboard;
using NZBDash.UI.Models.Hardware;
using NZBDash.UI.Models.ViewModels.Dashboard;

using Omu.ValueInjecter;

namespace NZBDash.UI.Controllers
{
    public class DashboardController : BaseController
    {
        private IThirdPartyService Api { get; set; }
        private IHardwareService Service { get; set; }
        private ILinksConfiguration LinksConfiguration { get; set; }
        private ISettingsService<NzbGetSettingsDto> NzbGet { get; set; }
        private ISettingsService<SabNzbdSettingsDto> Sab { get; set; }

        public DashboardController(IHardwareService service, IThirdPartyService api, ILinksConfiguration linksConfiguration, ILogger logger,
            ISettingsService<NzbGetSettingsDto> nzbGetSettingsService,
            ISettingsService<SabNzbdSettingsDto> sabSettingsService)
            : base(logger)
        {
            Api = api;
            Service = service;
            LinksConfiguration = linksConfiguration;
            NzbGet = nzbGetSettingsService;
            Sab = sabSettingsService;
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

        public ActionResult GetCpu()
        {
            var js = new JavaScriptSerializer().Serialize(CpuCounter.Counter.Last());
            return Json(js, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllCpu()
        {
            var js = new JavaScriptSerializer().Serialize(CpuCounter.Counter);
            return Json(js, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDownloads()
        {
            var model = new DashboardDownloadViewModel();
            var sab = Sab.GetSettings();
            var nzb = NzbGet.GetSettings();
            if (sab.HasSettings)
            {
                var formattedUri = Common.Helpers.UrlHelper.ReturnUri(sab.IpAddress, sab.Port).ToString();
                var items = Api.GetSabNzbdQueue(formattedUri,sab.ApiKey);

                model.DownloadItems = items.jobs.Count;
                model.Application = "Sabnzbd";
            }
            else if (nzb.HasSettings)
            {
                var formattedUri = Common.Helpers.UrlHelper.ReturnUri(nzb.IpAddress, nzb.Port).ToString();
                var nzbItem = Api.GetNzbGetList(formattedUri, nzb.Username, nzb.Password);

                model.DownloadItems = nzbItem.result.Count;
                model.Application = "NzbGet";
            }
            else
            {
                Logger.Trace("No settings found. Cannot display downloads on the Dashboard");
            }
            return PartialView("Partial/_Download", model);
        }

        public ActionResult GetTabDownloads()
        {
            var model = new TabDownloadViewModel();
            var sab = Sab.GetSettings();
            var nzb = NzbGet.GetSettings();
            if (sab.HasSettings)
            {
                var formattedUri = Common.Helpers.UrlHelper.ReturnUri(sab.IpAddress, sab.Port).ToString();
                var items = Api.GetSabNzbdQueue(formattedUri, sab.ApiKey);

                model.DownloadSpeed = MemorySizeConverter.SizeSuffix((long) items.kbpersec);
                model.Application = "Sabnzbd";
                foreach (var dl in items.jobs)
                {
                    var percentage = (dl.mbleft / dl.mb * 100);
                    Logger.Trace(string.Format("Percentage : {0}", percentage));

                    var status = EnumHelper<DownloadStatus>.Parse(items.paused ? "PAUSED" : "DOWNLOADING");
                    var progressBar = Bootstrap.ProgressBarDanger;
                    if (status == DownloadStatus.PAUSED || status == DownloadStatus.QUEUED)
                    {
                        progressBar = Bootstrap.ProgressBarWarning;
                    }
                    if (status == DownloadStatus.DOWNLOADING)
                    {
                        progressBar = Bootstrap.ProgressBarSuccess;
                    }
                    model.Downloads.Add(new TabDownloadItems
                    {
                        DownloadName = dl.filename,
                        Status = status.ToString(),
                        DownloadPercentage = percentage,
                        ProgressBarClass = progressBar
                    });
                }
               
            }
            else if (nzb.HasSettings)
            {
                var formattedUri = Common.Helpers.UrlHelper.ReturnUri(nzb.IpAddress, nzb.Port).ToString();
                var statusInfo = Api.GetNzbGetStatus(formattedUri, nzb.Username, nzb.Password);
                var nzbItem = Api.GetNzbGetList(formattedUri, nzb.Username, nzb.Password);

                model.DownloadSpeed = MemorySizeConverter.SizeSuffix(statusInfo.Result.DownloadRate / 1024);
                model.Application = "NzbGet";

                foreach (var dl in nzbItem.result)
                {
                    var percentage = (dl.DownloadedSizeMB / (dl.RemainingSizeMB + (double)dl.DownloadedSizeMB) * 100);
                    Logger.Trace(string.Format("Percentage : {0}", percentage));

                    var status = EnumHelper<DownloadStatus>.Parse(dl.Status);
                    var progressBar = "progress-bar-danger";
                    if (status == DownloadStatus.PAUSED || status == DownloadStatus.QUEUED)
                    {
                        progressBar = "progress-bar-warning";
                    }
                    if (status == DownloadStatus.DOWNLOADING)
                    {
                        progressBar = "progress-bar-success";
                    }
                    model.Downloads.Add(new TabDownloadItems
                    {
                        DownloadName = dl.NZBName,
                        Status = status.ToString(),
                        DownloadPercentage = percentage,
                        ProgressBarClass = progressBar
                    });
                }
            }
            else
            {
                Logger.Trace("No settings found. Cannot display downloads on the Dashboard");
            }
            return PartialView("NavbarDownloads", model);
        }
    }
}