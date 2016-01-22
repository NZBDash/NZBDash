#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: NzbGetController.cs
//  Created By: Jamie Rees
//
//  Permission is hereby granted, free of charge, to any person obtaining
//  a copy of this software and associated documentation files (the
//  "Software"), to deal in the Software without restriction, including
//  without limitation the rights to use, copy, modify, merge, publish,
//  distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to
//  the following conditions:
//
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//  LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//  WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ***********************************************************************
#endregion
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

using Grid.Mvc.Ajax.GridExtensions;

using NZBDash.Common;
using NZBDash.Common.Helpers;
using NZBDash.Common.Interfaces;
using NZBDash.Common.Mapping;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Models.Settings;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.UI.Helpers;
using NZBDash.UI.Models.Dashboard;
using NZBDash.UI.Models.ViewModels;
using NZBDash.UI.Models.ViewModels.NzbGet;

using Omu.ValueInjecter;

using UrlHelper = NZBDash.Common.Helpers.UrlHelper;

namespace NZBDash.UI.Controllers.Application
{
    public class NzbGetController : BaseController
    {
        public NzbGetController(ISettingsService<NzbGetSettingsDto> settingsService, IThirdPartyService api, ILogger logger)
        {
            SettingsService = settingsService;
            Api = api;
            Logger = logger;
            Settings = SettingsService.GetSettings();
        }

        private ISettingsService<NzbGetSettingsDto> SettingsService { get; set; }
        private NzbGetSettingsDto Settings { get; set; }
        private IThirdPartyService Api { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            var history = new List<NzbGetHistoryViewModel>().AsQueryable();
            var grid = (AjaxGrid<NzbGetHistoryViewModel>)new AjaxGridFactory().CreateAjaxGrid(history, 1, false);

            return View(new NzbGetHistoryGrid() { Grid = grid });
        }

        [HttpGet]
        public ActionResult GetNzbGetStatus()
        {
            if (!Settings.HasSettings)
            {
                ViewBag.Error = Resources.Resources.Settings_Missing_NzbGet;
                return PartialView("DashletError");
            }

            Logger.Trace("Getting Config");
            var formattedUri = UrlHelper.ReturnUri(Settings.IpAddress, Settings.Port).ToString();
            try
            {
                Logger.Trace("Getting NzbGetStatus");
                var statusInfo = Api.GetNzbGetStatus(formattedUri, Settings.Username, Settings.Password);

                var nzbModel = new NzbGetViewModel { Status = statusInfo.Result.ServerPaused ? "Paused" : "Running", };

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
            if (!Settings.HasSettings)
            {
                ViewBag.Error = Resources.Resources.Settings_Missing_NzbGet;
                return PartialView("DashletError");
            }

            Logger.Trace("Getting Config");
            var formattedUri = UrlHelper.ReturnUri(Settings.IpAddress, Settings.Port).ToString();
            try
            {
                Logger.Trace("Getting NzbGetStatus");
                var statusInfo = Api.GetNzbGetStatus(formattedUri, Settings.Username, Settings.Password);

                Logger.Trace("Getting Current NZBGetlist");
                var downloadInfo = Api.GetNzbGetList(formattedUri, Settings.Username, Settings.Password);

                var downloadSpeed = statusInfo.Result.DownloadRate / 1024;

                var model = new DownloaderViewModel
                {
                    Application = Applications.NzbGet,
                    DownloadSpeed = MemorySizeConverter.SizeSuffix(downloadSpeed),
                    DownloadItem = new List<DownloadItem>()
                };

                var results = downloadInfo.result;
                Logger.Trace(string.Format("Results count : {0}", results.Count));
                foreach (var result in results)
                {
                    Logger.Trace(string.Format("Going through result {0}", result.NZBName));
                    var percentage = (result.DownloadedSizeMB / (result.RemainingSizeMB + (double)result.DownloadedSizeMB) * 100);
                    Logger.Trace(string.Format("Percentage : {0}", percentage));

                    var status = EnumHelper<DownloadStatus>.Parse(result.Status);
                    var progressBar = "progress-bar-danger";
                    if (status == DownloadStatus.PAUSED || status == DownloadStatus.QUEUED)
                    {
                        progressBar = "progress-bar-warning";
                    }
                    if (status == DownloadStatus.DOWNLOADING)
                    {
                        progressBar = "progress-bar-success";
                    }

                    model.DownloadItem.Add(
                        new DownloadItem
                        {
                            FontAwesomeIcon = IconHelper.ChooseIcon(status),
                            DownloadPercentage = Math.Ceiling(percentage).ToString(CultureInfo.CurrentUICulture),
                            DownloadingName = result.NZBName,
                            Status = status,
                            NzbId = result.NZBID,
                            ProgressBarClass = progressBar
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
        public ActionResult Logs()
        {
            if (!Settings.HasSettings)
            {
                ViewBag.Error = Resources.Resources.Settings_Missing_NzbGet;
                return PartialView("DashletError");
            }

            var formattedUri = UrlHelper.ReturnUri(Settings.IpAddress, Settings.Port).ToString();
            var logs = Api.GetNzbGetLogs(formattedUri, Settings.Username, Settings.Password);
            if (logs.result != null)
            {
                var orderdLogs = logs.result.OrderByDescending(x => x.ID).ToList();

                var model = orderdLogs.Select(log => (NzbGetLogViewModel)new NzbGetLogViewModel().InjectFrom(new NzbGetLogMapper(), log)).Take(50).ToList();

                return PartialView("Partial/Logs", model);
            }
            
            return PartialView("Partial/Logs", new List<NzbGetLogViewModel>());
        }

        public JsonResult AjaxHistory()
        {
            var vm = GetHistory();

            var ajaxGridFactory = new AjaxGridFactory();
            var grid = ajaxGridFactory.CreateAjaxGrid(vm, 1, false);

            return Json(new { Html = grid.ToJson("Partial/History", this), grid.HasItems }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AjaxHistoryPaged(int? page)
        {
            var vm = GetHistory();

            var grid = new AjaxGridFactory().CreateAjaxGrid(vm, page ?? 1, page.HasValue);

            return Json(new { Html = grid.ToJson("Partial/History", this), grid.HasItems }, JsonRequestBehavior.AllowGet);
        }

        private IQueryable<NzbGetHistoryViewModel> GetHistory()
        {
            try
            {
                var formattedUri = UrlHelper.ReturnUri(Settings.IpAddress, Settings.Port).ToString();
                var history = Api.GetNzbGetHistory(formattedUri, Settings.Username, Settings.Password);

                var items = new List<NzbGetHistoryViewModel>();
                foreach (var result in history.result)
                {
                    var singleItem = new NzbGetHistoryViewModel();
                    var mappedResult = (NzbGetHistoryViewModel)singleItem.InjectFrom(new NzbGetHistoryMapper(), result);
                    if (!string.IsNullOrEmpty(mappedResult.FileSize))
                    {
                        long newFileSize;
                        long.TryParse(mappedResult.FileSize, out newFileSize);
                        mappedResult.FileSize = MemorySizeConverter.SizeSuffixMb(newFileSize);
                    }
                    items.Add(mappedResult);
                }

                return items.AsQueryable();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return new List<NzbGetHistoryViewModel>().AsQueryable();
            }
        }
    }
}