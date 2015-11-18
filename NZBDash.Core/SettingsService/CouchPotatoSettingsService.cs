using System.Linq;

using NZBDash.Common.Models.Data.Models.Settings;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.DataAccess.Interfaces;

using Omu.ValueInjecter;

namespace NZBDash.Core.SettingsService
{
    public class CouchPotatoSettingsService : ISettingsService<CouchPotatoSettingsDto>
    {
        private IRepository<CouchPotatoSettings> Repo { get; set; }
        public CouchPotatoSettingsService(IRepository<CouchPotatoSettings> repo)
        {
            Repo = repo;
        }

        public CouchPotatoSettingsDto GetSettings()
        {
            var result = Repo.GetAll();
            var setting = result.FirstOrDefault();
            if (setting == null)
            {
                return new CouchPotatoSettingsDto();
            }
            var model = new CouchPotatoSettingsDto();

            model.InjectFrom(setting);

            return model;
        }

        public bool SaveSettings(CouchPotatoSettingsDto model)
        {
            var entity = Repo.Find(model.Id);

            if (entity == null)
            {
                var newEntity = new CouchPotatoSettings();
                newEntity.InjectFrom(model);

                var insertResult = Repo.Insert(newEntity);
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

            var result = Repo.Modify(entity);

            return result == 1;
        }
    }
}
