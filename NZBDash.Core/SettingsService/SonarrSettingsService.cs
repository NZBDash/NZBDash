using System.Linq;

using NZBDash.Common.Models.Data.Models.Settings;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.DataAccess.Interfaces;

namespace NZBDash.Core.SettingsService
{
    public class SonarrSettingsService : ISettingsService<SonarrSettingsViewModelDto>
    {
        private IRepository<SonarrSettings> Repo { get; set; }
        public SonarrSettingsService(IRepository<SonarrSettings> repo)
        {
            Repo = repo;
        }

        public SonarrSettingsViewModelDto GetSettings()
        {
            var result = Repo.GetAll();
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
            var entity = Repo.Find(model.Id);

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

                var insertResult = Repo.Insert(newEntity);
                return insertResult != null;
            }

            entity.Enabled = model.Enabled;
            entity.IpAddress = model.IpAddress;
            entity.Port = model.Port;
            entity.ApiKey = model.ApiKey;
            entity.Id = model.Id;
            entity.ShowOnDashboard = model.ShowOnDashboard;

            var result = Repo.Modify(entity);

            return result == 1;
        }
    }
}
