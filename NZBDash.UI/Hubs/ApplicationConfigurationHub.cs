using System;
using System.Web.Script.Serialization;

using Microsoft.AspNet.SignalR;

using NZBDash.Common;
using NZBDash.Core.Configuration;
using NZBDash.Core.Model.DTO;
using NZBDash.UI.Controllers;
using NZBDash.UI.Helpers;
using NZBDash.UI.Models;

namespace NZBDash.UI.Hubs
{
    public class ApplicationConfigurationHub : Hub
    {
        public void DeleteApplication(int id)
        {
            try
            {
                var config = new AdminConfiguration();
                var result = config.DeleteApplication(id);

                if (result)
                {
                    Clients.All.removeSuccess(id, string.Format("Deleted Application!"));
                }
                else
                {
                    Clients.All.failed("We could not remove the application");
                }

            }
            catch (Exception e)
            {
                Clients.All.failed(e.Message);
            }

        }

        public void AddApplication(int application, string apiKey, string ipAddress, string password, string userName)
        {

            if (!Validate(ipAddress))
                return;

            var selectedApp = (Applications)application;

            
            var config = new ApplicationConfigurationViewModel()
            {
                ApplicationId = (int)selectedApp,
                ApplicationName = EnumHelper<Applications>.GetDisplayValue(selectedApp),
                ApiKey = apiKey,
                IpAddress = ipAddress,
                Password = password,
                Username = userName,
            };
            var admin = new AdminConfiguration();
            var dto = ApplicationConfigurationController.MapToDto(config);

            // We should have one 1 config per application
            var exists = admin.CheckIfConfigurationExists(dto.ApplicationId);
            if (exists)
            {
                Clients.All.failed(string.Format("You already have configuration settings for {0}.", dto.ApplicationName));
                return;
            }

            var result = admin.AddApplication(dto);

            if (result != null)
            {
                Clients.All.appMessage(string.Format("Added application {0}!", dto.ApplicationName), result.Id);
            }
            else
            {
                Clients.All.failed(string.Format("Sorry we failed to add application {0}, Please try again.", dto.ApplicationName));
            }

        }

        public void TestConnection(int application, string apiKey, string ipAddress, string password, string userName)
        {
            var selectedApp = (Applications)application;

            var tester = new EndpointTester(); //Need to work out how to identify the tester type

            var uri = UrlHelper.ReturnUri(ipAddress);
            if (uri == null)
            {
                Clients.All.failed(string.Format("Incorrect IP Address"));
                return;
            }

            try
            {
                var result = tester.TestApplicationConnectivity((Applications)application, apiKey, uri.ToString(), password, userName);

                if (!result)
                {
                    Clients.All.failed(string.Format("Could not connect to {0}", selectedApp));
                    return;
                }

                Clients.All.message(string.Format("Connection to {0} is successful", selectedApp));
            }
            catch (Exception e)
            {
                Clients.All.failed(string.Format("{0}", e.Message));
            }
        }

        // This is not used.
        public void AddApplication(string applicationJson)
        {
            var model = new JavaScriptSerializer().Deserialize<ApplicationConfigurationViewModel>(applicationJson);

            var admin = new AdminConfiguration();
            var dto = MapToDto(model);
            var result = admin.UpdateApplicationConfiguration(dto);
            if (result)
            {
                Clients.All.message(string.Format("Successfully updated {0}", model.ApplicationName));
            }
            Clients.All.failed(string.Format("Could not update {0}", model.ApplicationName));
        }

        private static ApplicationConfigurationDto MapToDto(ApplicationConfigurationViewModel model)
        {
            var dto = new ApplicationConfigurationDto
            {
                Id = model.Id,
                Password = model.Password,
                Username = model.Username,
                ApiKey = model.ApiKey,
                IpAddress = model.IpAddress,
                ApplicationName = model.ApplicationName,
                ApplicationId = model.ApplicationId
            };

            return dto;
        }

        private bool Validate(string ipAddress)
        {
            var errors = false;
            if (string.IsNullOrEmpty(ipAddress))
            {
                Clients.All.failed("Please enter in an IP Address");
                errors = true;
            }

            return !errors;
        }
    }
}