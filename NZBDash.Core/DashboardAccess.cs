using System.Linq;

using NZBDash.Core.Model.DTO;
using NZBDash.DataAccess.Models;
using NZBDash.DataAccess.Repository;

namespace NZBDash.Core
{
    public class DashboardAccess
    {

        public bool UpdateDashboard(DashboardGridDto[] dto)
        {

            var entity = dto.Select(gridDto =>
                new DashboardGrid
                {
                    Col = gridDto.Col,
                    GridId = gridDto.GridId,
                    HtmlContent = gridDto.HtmlContent,
                    Row = gridDto.Row,
                    SizeX = gridDto.SizeX,
                    SizeY = gridDto.SizeY
                }).ToList();

            var repo = new DashboardGridRepository();
            repo.RemoveAll();
            var result = repo.Insert(entity);

            return result != null;

        }


        public DashboardGridDto[] GetDashboard()
        {
            var repo = new DashboardGridRepository();
            var entity = repo.GetAll();

            var model = entity.Select(x => new DashboardGridDto
            {
                Col = x.Col,
                HtmlContent = x.HtmlContent,
                SizeX = x.SizeX,
                Row = x.Row,
                SizeY = x.SizeY,
                GridId = x.GridId
            }).ToArray();

            return model;
        }
    }
}
