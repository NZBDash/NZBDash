using System.Linq;

using NZBDash.Common.Models.Data.Models.Settings;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.DataAccess.Repository.Settings;

namespace NZBDash.Core.SettingsService
{
    public class CouchPotatoSettingsServiceService : ISettingsService<CouchPotatoSettingsDto>
    {
        public CouchPotatoSettingsDto GetSettings()
        {
            var repo = new CouchPotatoRepository();

            var result = repo.GetAll();
            var setting = result.FirstOrDefault();
            if (setting == null)
            {
                return new CouchPotatoSettingsDto();
            }

            var model = new CouchPotatoSettingsDto
            {
                Enabled = setting.Enabled,
                Id = setting.Id,
                IpAddress = setting.IpAddress,
                Password = setting.Password,
                Port = setting.Port,
                Username = setting.Username,
                ShowOnDashboard = setting.ShowOnDashboard,
                ApiKey = setting.ApiKey
            };

            return model;
        }

        public bool SaveSettings(CouchPotatoSettingsDto model)
        {
            var repo = new CouchPotatoRepository();

            var entity = repo.Find(model.Id);

            if (entity == null)
            {
                var newEntity = new CouchPotatoSettings
                {
                    Port = model.Port,
                    Enabled = model.Enabled,
                    IpAddress = model.IpAddress,
                    Id = model.Id,
                    ApiKey = model.ApiKey,
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
            entity.ApiKey = model.ApiKey;
            entity.Id = model.Id;
            entity.ShowOnDashboard = model.ShowOnDashboard;
            entity.Username = model.Username;
            entity.Password = model.Password;

            var result = repo.Modify(entity);

            return result == 1;
        }
    }
}
