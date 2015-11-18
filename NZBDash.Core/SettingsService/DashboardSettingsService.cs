using System.Collections.Generic;
using System.Linq;

using NZBDash.Common.Models.Data.Models;
using NZBDash.Core.Model.Settings;
using NZBDash.DataAccess.Interfaces;

using Omu.ValueInjecter;

namespace NZBDash.Core.SettingsService
{
    public class DashboardSettingsService
    {
        private IRepository<Applications> Repo { get; set; }

        public DashboardSettingsService(IRepository<Applications> repo)
        {
            Repo = repo;
        }

        public IEnumerable<EnabledDashletsDto> GetEnabledDashlets()
        {
            var all = Repo.GetAll();
            var showOnDash = all.Where(x => x.ShowOnDashboard);

            var dto = new List<EnabledDashletsDto>();

            foreach (var application in showOnDash)
            {
                if (application.ShowOnDashboard)
                {
                    var newDashletDto = new EnabledDashletsDto();
                    newDashletDto.InjectFrom(application);
                    dto.Add(newDashletDto);
                }
            }

            return dto;
        }
    }
}
