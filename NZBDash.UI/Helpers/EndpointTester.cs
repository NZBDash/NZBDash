using System;

using NZBDash.Api.Controllers;
using NZBDash.Common;

namespace NZBDash.UI.Helpers
{
    public class EndpointTester
    {
        public EndpointTester()
        {
            Api = new StatusApiController();
        }

        public StatusApiController Api { get; set; }

        public bool TestApplicationConnectivity(Applications application, string apiKey, string ipAddress, string password, string userName)
        {
            switch (application)
            {
                case Applications.SabNZB:
                    return SabNzbConnection(ipAddress, apiKey);
                case Applications.Sickbeard:
                    throw new NotImplementedException();
                case Applications.CouchPotato:
                    return CouchPotatoConnection(ipAddress, apiKey);
                case Applications.Kodi:
                    throw new NotImplementedException();
                case Applications.Sonarr:
                    return SonarrConnection(ipAddress, apiKey);
                case Applications.Plex:
                    return PlexConnection(ipAddress);
                case Applications.NzbGet:
                    return NzbGetConnection(ipAddress, userName, password);
                case Applications.Headphones:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException("application");
            }
        }


        public bool NzbGetConnection(string ip, string username, string password)
        {
            var result = Api.GetNzbGetStatus(ip, username, password);
            return result.version != null;
        }

        public bool SabNzbConnection(string ip, string apiKey)
        {
            try
            {
                var result = Api.GetSabNzb(ip, apiKey);

                return result.QueueObject.state != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CouchPotatoConnection(string ip, string api)
        {
            try
            {
                var result = Api.GetCouchPotatoStatus(ip, api);
                return result.success;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SonarrConnection(string ip, string api)
        {
            try
            {
                var result = Api.GetSonarrSystemStatus(ip, api);
                return result.version != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool PlexConnection(string ip)
        {
            try
            {
                var result = Api.GetPlexServers(ip);
                return result.Server.Name != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}