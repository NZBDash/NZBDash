using System.Linq;

using NZBDash.Core.Model.Settings;
using NZBDash.DataAccess.Models.Settings;
using NZBDash.DataAccess.Repository.Settings;

namespace NZBDash.Core
{
    public class SettingsSaver
    {
        public NzbGetSettingsDto GetNzbGetSettings()
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

        public bool SaveNzbGetSettings(NzbGetSettingsDto updatedModel)
        {
            var repo = new NzbGetRepository();

            var entity = repo.Find(updatedModel.Id);

            if (entity == null)
            {
                var newEntity = new NzbGetSettings
                {
                    Port = updatedModel.Port,
                    Username = updatedModel.Username,
                    Enabled = updatedModel.Enabled,
                    IpAddress = updatedModel.IpAddress,
                    Password = updatedModel.Password,
                    ShowOnDashboard = updatedModel.ShowOnDashboard
                };

                var insertResult = repo.Insert(newEntity);
                return insertResult != null;
            }

            entity.Enabled = updatedModel.Enabled;
            entity.IpAddress = updatedModel.IpAddress;
            entity.Password = updatedModel.Password;
            entity.Port = updatedModel.Port;
            entity.Username = updatedModel.Username;
            entity.ShowOnDashboard = updatedModel.ShowOnDashboard;

            var result = repo.Modify(entity);

            return result == 1;
        }

        public SonarrSettingsViewModelDto GetSonarrSettings()
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

        public bool SaveSonarrSettings(SonarrSettingsViewModelDto updatedModel)
        {
            var repo = new SonarrRepository();

            var entity = repo.Find(updatedModel.Id);

            if (entity == null)
            {
                var newEntity = new SonarrSettings
                {
                    Port = updatedModel.Port,
                    Enabled = updatedModel.Enabled,
                    IpAddress = updatedModel.IpAddress,
                    Id = updatedModel.Id,
                    ApiKey = updatedModel.ApiKey,
                    ShowOnDashboard = updatedModel.ShowOnDashboard
                };

                var insertResult = repo.Insert(newEntity);
                return insertResult != null;
            }

            entity.Enabled = updatedModel.Enabled;
            entity.IpAddress = updatedModel.IpAddress;
            entity.Port = updatedModel.Port;
            entity.ApiKey = updatedModel.ApiKey;
            entity.Id = updatedModel.Id;
            entity.ShowOnDashboard = updatedModel.ShowOnDashboard;

            var result = repo.Modify(entity);

            return result == 1;
        }
    }
}
