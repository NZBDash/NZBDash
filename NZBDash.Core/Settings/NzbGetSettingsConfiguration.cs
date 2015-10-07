using System;
using System.Linq;

using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.DataAccess.Models.Settings;
using NZBDash.DataAccess.Repository.Settings;

namespace NZBDash.Core.Settings
{
    public class NzbGetSettingsConfiguration : ISettings<NzbGetSettingsDto>
    {
        public NzbGetSettingsDto GetSettings()
        {
            var repo = new NzbGetRepository();

            var result = repo.GetAll();
            var setting = result.FirstOrDefault();
            if (setting == null)
            {
                return new NzbGetSettingsDto();
            }

            var model = new NzbGetSettingsDto
            {
                Enabled = setting.Enabled,
                Id = setting.Id,
                IpAddress = setting.IpAddress,
                Password = setting.Password,
                Port = setting.Port,
                Username = setting.Username,
                ShowOnDashboard = setting.ShowOnDashboard
            };

            return model;
        }

        public bool SaveSettings(NzbGetSettingsDto model)
        {
            var repo = new NzbGetRepository();

            var entity = repo.Find(model.Id);

            if (entity == null)
            {
                var newEntity = new NzbGetSettings
                {
                    Port = model.Port,
                    Username = model.Username,
                    Enabled = model.Enabled,
                    IpAddress = model.IpAddress,
                    Password = model.Password,
                    ShowOnDashboard = model.ShowOnDashboard
                };

                var insertResult = repo.Insert(newEntity);
                return insertResult != null;
            }

            entity.Enabled = model.Enabled;
            entity.IpAddress = model.IpAddress;
            entity.Password = model.Password;
            entity.Port = model.Port;
            entity.Username = model.Username;
            entity.ShowOnDashboard = model.ShowOnDashboard;

            var result = repo.Modify(entity);

            return result == 1;
        }
    }
}
