using System;
using System.Security.Principal;

using NZBDash.Common;
using NZBDash.Core.Settings;

namespace NZBDash.UI.Helpers
{
    public static class IPrincipleExtensions
    {
        public static bool IsApplicationEnabled(this IPrincipal principal, Applications application)
        {
            switch (application)
            {
                case Applications.SabNZB:
                    return new SabNzbSettingsConfiguration().GetSettings().Enabled;
                case Applications.Sickbeard:
                    break;
                case Applications.CouchPotato:
                    return new CouchPotatoSettingsConfiguration().GetSettings().Enabled;
                case Applications.Kodi:
                    break;
                case Applications.Sonarr:
                    return new SonarrSettingsConfiguration().GetSettings().Enabled;
                case Applications.Plex:
                    return new PlexSettingsConfiguration().GetSettings().Enabled;
                case Applications.NzbGet:
                   return new NzbGetSettingsConfiguration().GetSettings().Enabled;
                case Applications.Headphones:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("application");
            }
            return false;
        }
    }
}