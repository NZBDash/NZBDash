using System;
using System.Linq;

using HtmlAgilityPack;

using Microsoft.AspNet.SignalR;

using NZBDash.Api.Controllers;
using NZBDash.Core;
using NZBDash.Core.Configuration;
using NZBDash.Core.Model.DTO;
using NZBDash.UI.Controllers;
using NZBDash.UI.Helpers;
using NZBDash.UI.Models.Dashboard;

namespace NZBDash.UI.Hubs
{
    public class DashboardHub : Hub
    {
        public StatusApiController Api { get; set; }

        public DashboardHub()
        {
            Api = new StatusApiController();
        }

        public void UpdateDownloadSpeed()
        {
            var admin = new AdminConfiguration();
            var config = admin.GetApplicationSettings();
            var downloaderConfig = config.FirstOrDefault(x => x.ApplicationName == "NzbGet");
            var formattedUri = UrlHelper.ReturnUri(downloaderConfig.IpAddress).ToString();
            try
            {
                var statusInfo = Api.GetNzbGetStatus(formattedUri, downloaderConfig.Username, downloaderConfig.Password);

                var downloadSpeed = statusInfo.Result.DownloadRate / 1024;

                Clients.All.updateDownloadSpeed(downloadSpeed);
            }
            catch (Exception e)
            {
                Clients.All.error(e.Message);
            }
        }

        public void UpdateDownloadPercentage()
        {
            var admin = new AdminConfiguration();
            var config = admin.GetApplicationSettings();
            var downloaderConfig = config.FirstOrDefault(x => x.ApplicationName == "NzbGet");
            var formattedUri = UrlHelper.ReturnUri(downloaderConfig.IpAddress).ToString();
            try
            {
                var downloadInfo = Api.GetNzbGetList(formattedUri, downloaderConfig.Username, downloaderConfig.Password);

                var results = downloadInfo.result;
                foreach (var result in results)
                {

                    var percentage = Math.Ceiling(result.DownloadedSizeMB / (result.RemainingSizeMB + (double)result.DownloadedSizeMB) * 100);
                    var icon = DashboardController.ChooseIcon(EnumHelper<DownloadStatus>.Parse(result.Status));

                    Clients.All.updateDownloadPercentage(percentage, result.NZBID, icon);
                }
            }
            catch (Exception e)
            {
                Clients.All.error(e.Message);
            }
        }

        public void CheckForNewDownloadItems(int[] currentItems)
        {
            var admin = new AdminConfiguration();
            var config = admin.GetApplicationSettings();
            var downloaderConfig = config.FirstOrDefault(x => x.ApplicationName == "NzbGet");
            var formattedUri = UrlHelper.ReturnUri(downloaderConfig.IpAddress).ToString();
            try
            {
                var downloadInfo = Api.GetNzbGetList(formattedUri, downloaderConfig.Username, downloaderConfig.Password);

                var results = downloadInfo.result;
                
                var missingItems = currentItems.Except(results.Select(x => x.NZBID));
                if (missingItems.Any())
                {
                    foreach (var newItem in missingItems)
                    {
                        Clients.All.removeDownload(newItem);
                    }
                    return;
                }


                foreach (var result in results)
                {

                    // We have the download so forget it
                    if (currentItems.Contains(result.NZBID))
                    {
                        return;
                    }
                    
                    // We dont have the download so add it
                    if (!currentItems.Contains(result.NZBID))
                    {
                        Clients.All.addDownload();
                        return;
                    }

                }

                
            }
            catch (Exception e)
            {
                Clients.All.error(e.Message);
            }
        }

        
        public void GetGrid()
        {
            var access = new DashboardAccess();
            var grid = access.GetDashboard();

            if (grid.Length == 0)
            {
                Clients.All.loadDefaultGrid();
                return;
            }
            var model = new GridsterModel[grid.Length];
            for (var i = 0; i < grid.Length; i++)
            {
                model[i] = new GridsterModel
                {
                    col = grid[i].Col,
                    htmlContent = HtmlCleanup(grid[i].HtmlContent),
                    id = grid[i].GridId,
                    row = grid[i].Row,
                    size_x = grid[i].SizeX,
                    size_y = grid[i].SizeY
                };
            }

            
            Clients.All.updateGrid(model);
        }

        public void SaveGridPosition(GridsterModel[] models)
        {
            var access = new DashboardAccess();

            var dto = new DashboardGridDto[models.Length];
            for (var i = 0; i < models.Length; i++)
            {
                dto[i] = new DashboardGridDto
                {
                    GridId = models[i].id,
                    HtmlContent = HtmlCleanup(models[i].htmlContent),
                    Col = models[i].col,
                    Row = models[i].row,
                   SizeX = models[i].size_x,
                   SizeY = models[i].size_y
                };
            }

            var result = access.UpdateDashboard(dto);


            Clients.All.updateGrid(models);
        }

        private string HtmlCleanup(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var items = doc.DocumentNode;

            foreach (var item in items.ChildNodes)
            {
                if (item.Attributes["style"] != null)
                {
                    item.Attributes["style"].Remove();
                }
            }
            return doc.DocumentNode.OuterHtml;
        }
    }
}