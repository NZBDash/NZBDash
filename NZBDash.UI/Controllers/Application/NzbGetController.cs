using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

using NZBDash.Api.Controllers;
using NZBDash.Common;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.Settings;
using NZBDash.UI.Helpers;
using NZBDash.UI.Models.Dashboard;
using NZBDash.UI.Models.NzbGet;

using UrlHelper = NZBDash.UI.Helpers.UrlHelper;

namespace NZBDash.UI.Controllers.Application
{
    public class NzbGetController : BaseController
    {
        public NzbGetController() : this(new NzbGetSettingsConfiguration(), new StatusApiController())
        {
        }

        public NzbGetController(ISettings<NzbGetSettingsDto> settings, IStatusApi api) : base(typeof(NzbGetController))
        {
            Settings = settings;
            Api = api;
        }

        private ISettings<NzbGetSettingsDto> Settings { get; set; } 
        private IStatusApi Api { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            var config = Settings.GetSettings();
            var formattedUri = UrlHelper.ReturnUri(config.IpAddress, config.Port).ToString();
            try
            {
                var statusInfo = Api.GetNzbGetStatus(formattedUri, config.Username, config.Password);
                var downloadSpeed = statusInfo.Result.DownloadRate / 1024;
                var nzbModel = new NzbGetViewModel
                {
                    DownloadSpeed = downloadSpeed.ToString(CultureInfo.CurrentUICulture),
                    Status = statusInfo.Result.ServerPaused ? "Paused" : "Running",
                };

                return View(nzbModel);
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult GetNzbGetDownloadInformation()
        {
            var config = Settings.GetSettings();
            var formattedUri = UrlHelper.ReturnUri(config.IpAddress, config.Port).ToString();
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
                        FontAwesomeIcon = IconHelper.ChooseIcon(EnumHelper<DownloadStatus>.Parse(result.Status)),
                        DownloadPercentage = Math.Ceiling(percentage).ToString(CultureInfo.CurrentUICulture),
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

        [HttpGet]
        public ActionResult GetNzbGetDownloadHistory()
        {
            var config = Settings.GetSettings();
            var formattedUri = UrlHelper.ReturnUri(config.IpAddress, config.Port).ToString();
            var history = Api.GetNzbGetHistory(formattedUri, config.Username, config.Password);

            var model = history.result.Select(r => new NzbGetHistoryViewModel
            {
                Id = r.ID,
                Name = r.Name,
                Status = r.Status,
                Category = r.Category,
                FileSize = r.FileSizeMB,
                Health = r.Health,
                HistoryTime = r.HistoryTime,
            }).ToList();

            return PartialView("Partial/History", model);

        }

    }
}