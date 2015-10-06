using System;

using Microsoft.AspNet.SignalR;

using NZBDash.Common;
using NZBDash.UI.Helpers;

namespace NZBDash.UI.Hubs
{
    public class ApplicationConfigurationHub : Hub
    {
        public void TestNzbGetConnection(string ipAddress, int port, string username, string password)
        {
            const Applications selectedApp = Applications.NzbGet;

            var tester = new EndpointTester();
            var uri = UrlHelper.ReturnUri(ipAddress, port);
            if (uri == null)
            {
                Clients.All.failed(string.Format("Incorrect IP Address"));
                return;
            }

            try
            {
                var result = tester.TestApplicationConnectivity(selectedApp, string.Empty, uri.ToString(), password, username);
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
    }
}