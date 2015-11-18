using System;
using System.Linq;

using NZBDash.Common;
using NZBDash.Common.Interfaces;
using NZBDash.Common.Models.Data.Models.Settings;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.DataAccess.Repository.Settings;

namespace NZBDash.Core.SettingsService
{
    public class NzbGetSettingsService : ISettingsService<NzbGetSettingsDto>
    {
        public NzbGetSettingsService()
            : this(new NLogLogger(typeof(NzbGetSettingsService)))
        {
        }

        public NzbGetSettingsService(ILogger logger)
        {
            _logger = logger;
        }

        private ILogger _logger { get; set; }

        public NzbGetSettingsDto GetSettings()
        {
            _logger.Trace("Started NzbGetRepository");
            var repo = new NzbGetRepository();
            try
            {
                _logger.Trace("Getting all items from NzbGetRepository");
                var result = repo.GetAll();
                var setting = result.FirstOrDefault();
                if (setting == null)
                {
                    _logger.Trace("There are no items returned from NzbGetRepository. Returning new empty DTO");
                    return new NzbGetSettingsDto();
                }

                _logger.Trace("Creating dto from the results from NzbGetRepository");
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
            catch (Exception e)
            {
                _logger.Fatal(e);
                throw new Exception(e.Message,e);
            }
        }

        public bool SaveSettings(NzbGetSettingsDto model)
        {
            _logger.Trace("Started NzbGetRepository");
            var repo = new NzbGetRepository();

            _logger.Trace(string.Format("Looking for id {0} in the NzbGetRepository", model.Id));
            var entity = repo.Find(model.Id);

            if (entity == null)
            {
                _logger.Trace("Our entity is null so we are going to insert one");
                var newEntity = new NzbGetSettings
                {
                    Port = model.Port,
                    Username = model.Username,
                    Enabled = model.Enabled,
                    IpAddress = model.IpAddress,
                    Password = model.Password,
                    ShowOnDashboard = model.ShowOnDashboard
                };
                _logger.Trace("Inserting now");
                var insertResult = repo.Insert(newEntity);

                _logger.Trace(string.Format("Our insert was {0}", insertResult != null));
                return insertResult != null;
            }

            _logger.Trace("We found an entity so we are going to modify the existing one");
            entity.Enabled = model.Enabled;
            entity.IpAddress = model.IpAddress;
            entity.Password = model.Password;
            entity.Port = model.Port;
            entity.Username = model.Username;
            entity.ShowOnDashboard = model.ShowOnDashboard;

            _logger.Trace("Updating modified record");
            var result = repo.Modify(entity);

            _logger.Trace(string.Format("Our modify was {0}", result == 1));
            return result == 1;
        }
    }
}
