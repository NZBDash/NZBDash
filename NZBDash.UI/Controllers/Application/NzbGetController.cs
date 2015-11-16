using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

using NZBDash.Api.Controllers;
using NZBDash.Common;
using NZBDash.Common.Mapping;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.UI.Helpers;
using NZBDash.UI.Models.Dashboard;
using NZBDash.UI.Models.NzbGet;

using Omu.ValueInjecter;

using UrlHelper = NZBDash.UI.Helpers.UrlHelper;

namespace NZBDash.UI.Controllers.Application
{
    public class NzbGetController : BaseController
    {
        public NzbGetController(ISettings<NzbGetSettingsDto> settings, IStatusApi api)
            : base(typeof(NzbGetController))
        {
            Settings = settings;
            Api = api;
        }

        private ISettings<NzbGetSettingsDto> Settings { get; set; }
        private IStatusApi Api { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetNzbGetStatus()
        {
            Logger.Trace("Getting Config");
            var config = Settings.GetSettings();
            var formattedUri = UrlHelper.ReturnUri(config.IpAddress, config.Port).ToString();
            try
            {
                Logger.Trace("Getting NzbGetStatus");
                var statusInfo = Api.GetNzbGetStatus(formattedUri, config.Username, config.Password);
                Logger.Trace(string.Format("Converting DL Speed : {0}", statusInfo.Result.DownloadRate));
                var downloadSpeed = statusInfo.Result.DownloadRate / 1024;

                var nzbModel = new NzbGetViewModel
                {
                    DownloadSpeed = downloadSpeed.ToString(CultureInfo.CurrentUICulture),
                    Status = statusInfo.Result.ServerPaused ? "Paused" : "Running",
                };

                Logger.Trace("Returning Model");
                return Json(nzbModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetNzbGetDownloadInformation()
        {
            Logger.Trace("Getting Config");
            var config = Settings.GetSettings();
            var formattedUri = UrlHelper.ReturnUri(config.IpAddress, config.Port).ToString();
            try
            {
                Logger.Trace("Getting NzbGetStatus");
                var statusInfo = Api.GetNzbGetStatus(formattedUri, config.Username, config.Password);

                Logger.Trace("Getting Current NZBGetlist");
                var downloadInfo = Api.GetNzbGetList(formattedUri, config.Username, config.Password);

                var downloadSpeed = statusInfo.Result.DownloadRate / 1024;

                var model = new DownloaderViewModel
                {
                    Application = Applications.NzbGet,
                    DownloadSpeed = downloadSpeed.ToString(CultureInfo.CurrentUICulture),
                    DownloadItem = new List<DownloadItem>()
                };

                var results = downloadInfo.result;
                Logger.Trace(string.Format("Results count : {0}", results.Count));
                foreach (var result in results)
                {
                    Logger.Trace(string.Format("Going through result {0}", result.NZBName));
                    var percentage = (result.DownloadedSizeMB / (result.RemainingSizeMB + (double)result.DownloadedSizeMB) * 100);
                    Logger.Trace(string.Format("Percentage : {0}", percentage));

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
                Logger.Error(e.Message, e);
                ViewBag.Error = e.Message;
                return PartialView("DashletError");
            }
        }

        [HttpGet]
        public ActionResult GetNzbGetDownloadHistory()
        {
            try
            {
                var config = Settings.GetSettings();
                var formattedUri = UrlHelper.ReturnUri(config.IpAddress, config.Port).ToString();
                var history = Api.GetNzbGetHistory(formattedUri, config.Username, config.Password);

                var model = new List<NzbGetHistoryViewModel>();

                foreach (var result in history.result)
                {
                    var singleItem = new NzbGetHistoryViewModel();
                    var mappedResult = (NzbGetHistoryViewModel)singleItem.InjectFrom(new NzbGetHistoryMapper(), result);
                    model.Add(mappedResult);
                }

                return PartialView("Partial/History", model);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return PartialView("DashletError");
            }
        }
    }
}