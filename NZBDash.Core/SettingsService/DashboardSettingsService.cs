using System.Collections.Generic;
using System.Linq;

using NZBDash.Core.Model.Settings;
using NZBDash.DataAccess.Repository;

namespace NZBDash.Core.SettingsService
{
    public class DashboardSettingsService
    {
        public IEnumerable<EnabledDashletsDto> GetEnabledDashlets()
        {
            var repo = new ApplicationsRepository();

            var all = repo.GetAll();

            var showOnDash = all.Where(x => x.ShowOnDashboard);

            var dto = new List<EnabledDashletsDto>();

            foreach (var applicationse in showOnDash)
            {
                if(applicationse.ShowOnDashboard)
                    dto.Add(new EnabledDashletsDto
                    {
                        Id = applicationse.Id,
                        Enabled = applicationse.Enabled,
                        ShowOnDashboard = applicationse.ShowOnDashboard,
                        ApplicationName = applicationse.ApplicationName
                    });
            }

            return dto;
        }

    }
}
