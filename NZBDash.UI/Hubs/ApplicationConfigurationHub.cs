using System;
using System.Web.Script.Serialization;

using Microsoft.AspNet.SignalR;

using NZBDash.Common;
using NZBDash.Core.Configuration;
using NZBDash.Core.Model.DTO;
using NZBDash.UI.Helpers;
using NZBDash.UI.Models;

namespace NZBDash.UI.Hubs
{
    public class ApplicationConfigurationHub : Hub
    {
        public void TestNzbGetConnection(string ipAddress, int port, string username, string password)
        {
            var selectedApp = Applications.NzbGet;
            var tester = new EndpointTester();
            var uri = UrlHelper.ReturnUri(ipAddress, port);


        }

        public void TestConnection(int application, string apiKey, string ipAddress, string password, string userName)
        {
            var selectedApp = (Applications)application;

            var tester = new EndpointTester(); // Need to work out how to identify the tester type

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