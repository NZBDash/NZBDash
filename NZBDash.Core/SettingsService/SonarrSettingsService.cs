using System.Linq;

using NZBDash.Common.Models.Data.Models.Settings;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.DataAccess.Repository.Settings;

namespace NZBDash.Core.SettingsService
{
    public class SonarrSettingsService : ISettingsService<SonarrSettingsViewModelDto>
    {
        public SonarrSettingsViewModelDto GetSettings()
        {
            var repo = new SonarrRepository();
            var result = repo.GetAll();
            var setting = result.FirstOrDefault();
            if (setting == null)
            {
                return new SonarrSettingsViewModelDto();
            }

            return new SonarrSettingsViewModelDto
            {
                ApiKey = setting.ApiKey,
                Enabled = setting.Enabled,
                Id = setting.Id,
                IpAddress = setting.IpAddress,
                Port = setting.Port,
                ShowOnDashboard = setting.ShowOnDashboard
            };
        }

        public bool SaveSettings(SonarrSettingsViewModelDto model)
        {
            var repo = new SonarrRepository();

            var entity = repo.Find(model.Id);

            if (entity == null)
            {
                var newEntity = new SonarrSettings
                {
                    Port = model.Port,
                    Enabled = model.Enabled,
                    IpAddress = model.IpAddress,
                    Id = model.Id,
                    ApiKey = model.ApiKey,
                    ShowOnDashboard = model.ShowOnDashboard
                };

                var insertResult = repo.Insert(newEntity);
                return insertResult != null;
            }

            entity.Enabled = model.Enabled;
            entity.IpAddress = model.IpAddress;
            entity.Port = model.Port;
            entity.ApiKey = model.ApiKey;
            entity.Id = model.Id;
            entity.ShowOnDashboard = model.ShowOnDashboard;

            var result = repo.Modify(entity);

            return result == 1;
        }
    }
}
