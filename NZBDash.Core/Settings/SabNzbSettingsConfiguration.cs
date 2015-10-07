using System.Linq;

using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.DataAccess.Models.Settings;
using NZBDash.DataAccess.Repository.Settings;

namespace NZBDash.Core.Settings
{
    public class SabNzbSettingsConfiguration : ISettings<SabNzbSettingsDto>
    {
        public SabNzbSettingsDto GetSettings()
        {
            var repo = new SabNzbRepository();

            var result = repo.GetAll();
            var setting = result.FirstOrDefault();
            if (setting == null)
            {
                return new SabNzbSettingsDto();
            }

            var model = new SabNzbSettingsDto
            {
                Enabled = setting.Enabled,
                Id = setting.Id,
                IpAddress = setting.IpAddress,
                Port = setting.Port,
                ShowOnDashboard = setting.ShowOnDashboard,
                ApiKey = setting.ApiKey,
            };

            return model;
        }

        public bool SaveSettings(SabNzbSettingsDto model)
        {
            var repo = new SabNzbRepository();

            var entity = repo.Find(model.Id);

            if (entity == null)
            {
                var newEntity = new SabNzbSettings
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
