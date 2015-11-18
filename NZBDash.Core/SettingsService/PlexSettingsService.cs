using System.Linq;

using NZBDash.Common.Models.Data.Models.Settings;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.DataAccess.Repository.Settings;

namespace NZBDash.Core.SettingsService
{
    public class PlexSettingsService : ISettingsService<PlexSettingsDto>
    {
        public PlexSettingsDto GetSettings()
        {
            var repo = new PlexRepository();

            var result = repo.GetAll();
            var setting = result.FirstOrDefault();
            if (setting == null)
            {
                return new PlexSettingsDto();
            }

            var model = new PlexSettingsDto
            {
                Enabled = setting.Enabled,
                Id = setting.Id,
                IpAddress = setting.IpAddress,
                Password = setting.Password,
                Port = setting.Port,
                Username = setting.Username,
                ShowOnDashboard = setting.ShowOnDashboard,
            };

            return model;
        }

        public bool SaveSettings(PlexSettingsDto model)
        {
            var repo = new PlexRepository();

            var entity = repo.Find(model.Id);

            if (entity == null)
            {
                var newEntity = new PlexSettings
                {
                    Port = model.Port,
                    Enabled = model.Enabled,
                    IpAddress = model.IpAddress,
                    Id = model.Id,
                    ShowOnDashboard = model.ShowOnDashboard,
                    Username = model.Username,
                    Password = model.Password
                };

                var insertResult = repo.Insert(newEntity);
                return insertResult != null;
            }

            entity.Enabled = model.Enabled;
            entity.IpAddress = model.IpAddress;
            entity.Port = model.Port;
            entity.Id = model.Id;
            entity.ShowOnDashboard = model.ShowOnDashboard;
            entity.Username = model.Username;
            entity.Password = model.Password;

            var result = repo.Modify(entity);

            return result == 1;
        }
    }
}
