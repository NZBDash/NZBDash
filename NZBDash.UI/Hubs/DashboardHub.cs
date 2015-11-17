using HtmlAgilityPack;

using Microsoft.AspNet.SignalR;

using NZBDash.Api.Controllers;
using NZBDash.Core;
using NZBDash.Core.Model.DTO;
using NZBDash.UI.Models.Dashboard;

namespace NZBDash.UI.Hubs
{
    public class DashboardHub : Hub
    {
        private IStatusApi Api { get; set; }

        public DashboardHub(IStatusApi statusApi)
        {
            Api = statusApi;
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