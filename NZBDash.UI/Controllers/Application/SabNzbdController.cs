#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: SabNzbdController.cs
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
using NZBDash.UI.Models;
using NZBDash.UI.Models.Dashboard;
using NZBDash.UI.Models.ViewModels;
using NZBDash.UI.Models.ViewModels.SabNzbd;

using Omu.ValueInjecter;

using UrlHelper = NZBDash.Common.Helpers.UrlHelper;

namespace NZBDash.UI.Controllers.Application
{
    public class SabNzbdController : BaseController
    {
        public SabNzbdController(ISettingsService<SabNzbdSettingsDto> settingsService, IThirdPartyService api, ILogger logger)
        {
            SettingsService = settingsService;
            Api = api;
            Logger = logger;
            Settings = SettingsService.GetSettings();
        }

        private ISettingsService<SabNzbdSettingsDto> SettingsService { get; set; }
        private SabNzbdSettingsDto Settings { get; set; }
        private IThirdPartyService Api { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            var history = new List<SabNzbdHistoryViewModel>().AsQueryable();
            var grid = (AjaxGrid<SabNzbdHistoryViewModel>)new AjaxGridFactory().CreateAjaxGrid(history, 1, false);

            return View(new SabNzbdHistoryGrid { Grid = grid });
        }

        [HttpGet]
        public ActionResult GetSabNzbStatus()
        {
            if (!Settings.HasSettings)
            {
                return new EmptyResult();
            }

            Logger.Trace("Getting Config");
            var formattedUri = UrlHelper.ReturnUri(Settings.IpAddress, Settings.Port).ToString();
            try
            {
                Logger.Trace("Getting GetSabNzbdQueue");
                var queueInfo = Api.GetSabNzbdQueue(formattedUri, Settings.ApiKey);

                var sabNzbdModel = new SabNzbdStatusViewModel
                {
                    Status = queueInfo.paused ? "Paused" : "Running",
                };

                Logger.Trace("Returning Model");
                return Json(sabNzbdModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetSabNzbdDownloadInformation()
        {
            if (!Settings.HasSettings)
            {
                ViewBag.Error = Resources.Resources.Settings_Missing_SabNzb;
                return PartialView("DashletError");
            }

            Logger.Trace("Getting Config");
            var formattedUri = UrlHelper.ReturnUri(Settings.IpAddress, Settings.Port).ToString();
            try
            {
                Logger.Trace("Getting GetSabNzbdQueue");
                var statusInfo = Api.GetSabNzbdQueue(formattedUri, Settings.ApiKey);

                var downloadSpeed = statusInfo.kbpersec;

                var model = new DownloaderViewModel
                {
                    Application = Applications.SabNZBD,
                    DownloadSpeed = MemorySizeConverter.SizeSuffix((long)downloadSpeed),
                    DownloadItem = new List<DownloadItem>()
                };

                var results = statusInfo.jobs;
                Logger.Trace(string.Format("Results count : {0}", results.Count));
                foreach (var result in results)
                {
                    Logger.Trace(string.Format("Going through result {0}", result.id));
                    var percentage = (result.mbleft / result.mb * 100);
                    Logger.Trace(string.Format("Percentage : {0}", percentage));

                    var status = EnumHelper<DownloadStatus>.Parse(statusInfo.paused ? "PAUSED" : "DOWNLOADING");
                    var progressBar = Bootstrap.ProgressBarDanger;
                    if (status == DownloadStatus.PAUSED || status == DownloadStatus.QUEUED)
                    {
                        progressBar = Bootstrap.ProgressBarWarning;
                    }
                    if (status == DownloadStatus.DOWNLOADING)
                    {
                        progressBar = Bootstrap.ProgressBarSuccess;
                    }

                    int nzbId;
                    int.TryParse(result.id, out nzbId);

                    model.DownloadItem.Add(new DownloadItem
                    {
                        FontAwesomeIcon = IconHelper.ChooseIcon(status),
                        DownloadPercentage = Math.Ceiling(percentage).ToString(CultureInfo.CurrentUICulture),
                        DownloadingName = result.filename,
                        Status = status,
                        NzbId = nzbId,
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

        private IQueryable<SabNzbdHistoryViewModel> GetHistory()
        {
            try
            {
                var formattedUri = UrlHelper.ReturnUri(Settings.IpAddress, Settings.Port).ToString();
                var history = Api.GetSabNzbdHistory(formattedUri, Settings.ApiKey);

                var items = new List<SabNzbdHistoryViewModel>();
                foreach (var result in history.slots)
                {
                    var singleItem = new SabNzbdHistoryViewModel();
                    var mappedResult = (SabNzbdHistoryViewModel)singleItem.InjectFrom(new SabNzbdHistoryMapper(), result);
                    if (!string.IsNullOrEmpty(mappedResult.FileSize))
                    {
                        double newFileSize;
                        double.TryParse(mappedResult.FileSize, out newFileSize);
                        mappedResult.FileSize = MemorySizeConverter.SizeSuffixMb(newFileSize);
                    }
                    items.Add(mappedResult);
                }

                return items.AsQueryable();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return new List<SabNzbdHistoryViewModel>().AsQueryable();
            }
        }

        //[HttpGet]
        //public ActionResult Logs()
        //{
        //    if (!Settings.HasSettings)
        //    {
        //        ViewBag.Error = Resources.Resources.Settings_Missing_NzbGet;
        //        return PartialView("DashletError");
        //    }

        //    var formattedUri = UrlHelper.ReturnUri(Settings.IpAddress, Settings.Port).ToString();
        //    var logs = Api.GetNzbGetLogs(formattedUri, Settings.Username, Settings.Password);

        //    var orderdLogs = logs.result.OrderByDescending(x => x.ID).ToList();

        //    var model = orderdLogs.Select(log => 
        //        (NzbGetLogViewModel)new NzbGetLogViewModel()
        //        .InjectFrom(new NzbGetLogMapper(), log))
        //        .Take(50)
        //        .ToList();

        //    return PartialView("Partial/Logs",model);
        //}
    }
}