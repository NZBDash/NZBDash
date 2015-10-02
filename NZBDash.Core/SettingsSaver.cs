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
                Username = setting.Username
            };

            return model;
        }

        public bool SaveNzbGetSettings(NzbGetSettingsDto updatedModel)
        {
            var entity = new NzbGetSettings
            {
                Id = updatedModel.Id,
                IpAddress = updatedModel.IpAddress,
                Password = updatedModel.Password,
                Port = updatedModel.Port,
                Username = updatedModel.Username,
                Enabled = updatedModel.Enabled
            };

            var repo = new NzbGetRepository();

            var result = repo.Modify(entity);

            return result == 1;
        }
    }
}
