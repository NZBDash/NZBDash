using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NZBDash.Core.Model.DTO;
using NZBDash.DataAccess.Models;
using NZBDash.DataAccess.Repository;

namespace NZBDash.Core.Configuration
{
    public class AdminConfiguration
    {
        public async Task<AdminConfigurationDto> GetConfigurationAsync()
        {
            var config = new AdminConfigurationRepository();
            var data = await config.GetAllAsync(); // We should only have ever 1 item in the DB for config
            var retVal = data.FirstOrDefault();
            if (retVal == null) return new AdminConfigurationDto();

            return MapToDto(retVal);
        }

        public IEnumerable<ApplicationConfigurationDto> GetApplicationSettings()
        {
            var config = new ApplicationConfigurationRepository();
            var result = config.GetAll();
            return result.Select(r => new ApplicationConfigurationDto
            {
                Id = r.Id,
                ApplicationName = r.ApplicationName,
                ApiKey = r.ApiKey,
                ApplicationId = r.ApplicationId,
                IpAddress = r.IpAddress,
                Password = r.Password,
                Username = r.Username
            }).ToList();
        }

        public async Task<IEnumerable<ApplicationConfigurationDto>> GetApplicationSettingsAsync()
        {
            var config = new ApplicationConfigurationRepository();
            var result = await config.GetAllAsync();
            return result.Select(r => new ApplicationConfigurationDto
            {
                Id = r.Id,
                ApplicationName = r.ApplicationName,
                ApiKey = r.ApiKey,
                ApplicationId = r.ApplicationId,
                IpAddress = r.IpAddress,
                Password = r.Password,
                Username = r.Username
            }).ToList();
        }

        public AdminConfigurationDto GetConfiguration()
        {
            var config = new AdminConfigurationRepository();
            var data = config.GetAll(); // We should only have ever 1 item in the DB for config
            var retVal = data.FirstOrDefault();
            if (retVal == null) return new AdminConfigurationDto();

            return MapToDto(retVal);
        }

        public async Task<IEnumerable<SupportedApplicationsDto>> GetSupportedApplicationsAsync()
        {
            var repo = new SupportedApplicationsRepository();
            var apps = await repo.GetAllAsync();

            return apps.Select(app => new SupportedApplicationsDto { Id = app.Id, Name = app.Name }).ToList();
        }


        public IEnumerable<SupportedApplicationsDto> GetSupportedApplications()
        {
            var repo = new SupportedApplicationsRepository();
            var apps = repo.GetAll();

            return apps.Select(app => new SupportedApplicationsDto { Id = app.Id, Name = app.Name }).ToList();
        }


        public ApplicationConfigurationDto AddApplication(ApplicationConfigurationDto app)
        {
            var config = new ApplicationConfigurationRepository();
            var newItem = new ApplicationConfiguration
            {
                ApiKey = app.ApiKey,
                ApplicationName = app.ApplicationName,
                ApplicationId = app.ApplicationId,
                IpAddress = app.IpAddress,
                Password = app.Password,
                Username = app.Username,
            };
            var result = config.Insert(newItem);
            return MapToDto(result);
        }

        public bool CheckIfConfigurationExists(int applicationid)
        {
            var repo = new ApplicationConfigurationRepository();
            var results = repo.GetAll();

            var exist = results.FirstOrDefault(x => x.ApplicationId == applicationid);

            return exist != null;
        }

        public bool DeleteApplication(int id)
        {
            var repo = new ApplicationConfigurationRepository();
            var itemToRemove = repo.Find(id);
            var result = repo.Remove(itemToRemove);

            return result.Equals(1);
        }

        public async Task<bool> UpdateApplicationConfigurationAsync(ApplicationConfigurationDto updatedConfig)
        {
            var config = new ApplicationConfigurationRepository();
            var original = await config.FindAsync(updatedConfig.Id);

            original.ApiKey = updatedConfig.ApiKey;
            original.ApplicationName = updatedConfig.ApplicationName;
            original.ApplicationId = updatedConfig.ApplicationId;
            original.IpAddress = updatedConfig.IpAddress;
            original.Password = updatedConfig.Password;
            original.Username = updatedConfig.Username;

            var result = await config.ModifyAsync(original);

            return result.Equals(1);
        }

        public bool UpdateApplicationConfiguration(ApplicationConfigurationDto updatedConfig)
        {
            var config = new ApplicationConfigurationRepository();
            var original = config.Find(updatedConfig.Id);

            original.ApiKey = updatedConfig.ApiKey;
            original.ApplicationName = updatedConfig.ApplicationName;
            original.ApplicationId = updatedConfig.ApplicationId;
            original.IpAddress = updatedConfig.IpAddress;
            original.Password = updatedConfig.Password;
            original.Username = updatedConfig.Username;

            var result = config.Modify(original);

            return result.Equals(1);
        }

        public AdminConfigurationDto MapToDto(DataAccess.Models.AdminConfiguration data)
        {
            var mappedResult = new AdminConfigurationDto()
             {
                 Id = data.Id,
                 Authentication = data.Authentication,
             };

            return mappedResult;
        }

        public ApplicationConfigurationDto MapToDto(ApplicationConfiguration data)
        {
            var config = new ApplicationConfigurationDto
            {
                Id = data.Id,
                Password = data.Password,
                Username = data.Username,
                ApplicationName = data.ApplicationName,
                ApplicationId = data.ApplicationId,
                ApiKey = data.ApiKey,
                IpAddress = data.IpAddress
            };
            return config;
        }
    }
}