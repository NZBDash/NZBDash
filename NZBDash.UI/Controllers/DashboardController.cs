using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

using NZBDash.Api.Controllers;
using NZBDash.Common;
using NZBDash.Core.Configuration;
using NZBDash.Core.Settings;
using NZBDash.UI.Helpers;
using NZBDash.UI.Models.Dashboard;

using UrlHelper = NZBDash.UI.Helpers.UrlHelper;

namespace NZBDash.UI.Controllers
{
    public class DashboardController : BaseController
    {
        public StatusApiController Api { get; set; }

        public DashboardController()
            : base(typeof(DashboardController))
        {
            Api = new StatusApiController();
        }

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult GetEnabledDashlets()
        //{
        //    var admin = new 
        //}

        public ActionResult GetNzbGetDownloadInformation()
        {
            var admin = new NzbGetSettingsConfiguration();
            var config = admin.GetSettings();
            var formattedUri = UrlHelper.ReturnUri(config.IpAddress).ToString();
            try
            {
                var statusInfo = Api.GetNzbGetStatus(formattedUri, config.Username, config.Password);

                var downloadInfo = Api.GetNzbGetList(formattedUri, config.Username, config.Password);
                
                var downloadSpeed = statusInfo.Result.DownloadRate / 1024;

                var model = new DownloaderViewModel
                {
                    Application = Applications.NzbGet,
                    DownloadSpeed = downloadSpeed.ToString(CultureInfo.CurrentUICulture),
                    DownloadItem = new List<DownloadItem>()
                };

                var results = downloadInfo.result;
                foreach (var result in results)
                {
                    var percentage = (result.DownloadedSizeMB / (result.RemainingSizeMB + (double)result.DownloadedSizeMB) * 100);

                    model.DownloadItem.Add(new DownloadItem
                    {
                        FontAwesomeIcon = ChooseIcon(EnumHelper<DownloadStatus>.Parse(result.Status)),
                        DownloadPercentage = Math.Ceiling(percentage).ToString(),
                        DownloadingName = result.NZBName,
                        Status = EnumHelper<DownloadStatus>.Parse(result.Status),
                        NzbId = result.NZBID
                    });
                }

                return PartialView("Partial/_Download", model);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return PartialView("Partial/DashletError");
            }
        }

        public static string ChooseIcon(DownloadStatus downloadStatus)
        {
            switch (downloadStatus)
            {
                case DownloadStatus.QUEUED:
                    return "fa-clock-o";
                case DownloadStatus.PAUSED:
                    return "fa-pause";
                case DownloadStatus.DOWNLOADING:
                    return "fa-download";
                case DownloadStatus.FETCHING:
                    return "fa-spinner";
                case DownloadStatus.PP_QUEUED:
                    break;
                case DownloadStatus.LOADING_PARS:
                    return "fa-spinner";
                case DownloadStatus.VERIFYING_SOURCES:
                    break;
                case DownloadStatus.REPAIRING:
                    break;
                case DownloadStatus.VERIFYING_REPAIRED:
                    break;
                case DownloadStatus.RENAMING:
                    break;
                case DownloadStatus.UNPACKING:
                    break;
                case DownloadStatus.MOVING:
                    return "fa-arrow-right";
                case DownloadStatus.EXECUTING_SCRIPT:
                    break;
                case DownloadStatus.PP_FINISHED:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("downloadStatus", downloadStatus, null);
            }
            return "fa-question";
        }

        public ActionResult GetDriveInformation()
        {
            var driveInfo = Api.GetDriveInfo();
            var model = driveInfo.Select(drive => new DrivesViewModel
            {
                AvailableFreeSpace = drive.AvailableFreeSpace,
                DriveFormat = drive.DriveFormat,
                FreeSpaceString = drive.FreeSpaceString,
                IsReady = drive.IsReady,
                Name = drive.Name,
                PercentageFilled = drive.PercentageFilled,
                TotalFreeSpace = drive.TotalFreeSpace,
                TotalSize = drive.TotalSize,
                TotalSpaceString = drive.TotalSpaceString,
                VolumeLabel = drive.VolumeLabel
            }).ToList();

            return PartialView("Partial/_DriveInformation", model);
        }

        public ActionResult GetLinks()
        {
            var config = new LinksConfiguration();
            var allLinks = config.GetLinks();

            var model = allLinks.Select(link => new DashboardLinksViewModel { LinkEndpoint = link.LinkEndpoint, LinkName = link.LinkName }).ToList();

            return PartialView("Partial/_Links", model);
        }

        public ActionResult GetRam()
        {
            var ramInfo = Api.GetRamInfo();
            var model = new RamViewModel
            {
                AvailablePhysicalMemory = ramInfo.AvailablePhysicalMemory,
                AvailableVirtualMemory = ramInfo.AvailableVirtualMemory,
                OSFullName = ramInfo.OSFullName,
                OSPlatform = ramInfo.OSPlatform,
                OSVersion = ramInfo.OSVersion,
                PhysicalPercentageFilled = ramInfo.PhysicalPercentageFilled,
                TotalPhysicalMemory = ramInfo.TotalPhysicalMemory,
                TotalVirtualMemory = ramInfo.TotalVirtualMemory,
                VirtualPercentageFilled = ramInfo.VirtualPercentageFilled
            };

            return PartialView("Partial/_Ram", model);
        }

        public ActionResult GetServerInformation()
        {
            var ramInfo = Api.GetRamInfo();
            var uptime = Api.UpTime();
            var model = new RamViewModel
            {
                OSFullName = ramInfo.OSFullName,
                OSPlatform = ramInfo.OSPlatform,
                OSVersion = ramInfo.OSVersion,
                Uptime = uptime
            };

            return PartialView("Partial/_ServerInformation", model);
        }
    }
}