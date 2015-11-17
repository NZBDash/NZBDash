using System;
using System.Security.Principal;

using NZBDash.Common;
using NZBDash.Core.SettingsService;

namespace NZBDash.UI.Helpers
{
    public static class IPrincipleExtensions
    {
        public static bool IsApplicationEnabled(this IPrincipal principal, Applications application)
        {
            switch (application)
            {
                case Applications.SabNZB:
                    return new SabNzbSettingsService().GetSettings().Enabled;
                case Applications.Sickbeard:
                    break;
                case Applications.CouchPotato:
                    return new CouchPotatoSettingsServiceService().GetSettings().Enabled;
                case Applications.Kodi:
                    break;
                case Applications.Sonarr:
                    return new SonarrSettingsService().GetSettings().Enabled;
                case Applications.Plex:
                    return new PlexSettingsService().GetSettings().Enabled;
                case Applications.NzbGet:
                   return new NzbGetSettingsService().GetSettings().Enabled;
                case Applications.Headphones:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("application");
            }
            return false;
        }
    }
}