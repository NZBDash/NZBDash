#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: NavBarHelper.cs
//  Created By: Jamie Rees
// 
//  Permission is hereby granted, free of charge, to any person obtaining
//  a copy of this software and associated documentation files (the
//  "Software"), to deal in the Software without restriction, including
//  without limitation the rights to use, copy, modify, merge, publish,
//  distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to
//  the following conditions:
// 
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
// 
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//  LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//  WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ***********************************************************************
#endregion
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Ninject;

using NZBDash.Common;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.UI.App_Start;

namespace NZBDash.UI.Helpers
{
    public static class NavBarHelper
    {
        public const string SabKey = "SabNzdb";
        public const string CouchPotatoKey = "CouchPotato";
        public const string SonarrKey = "Sonarr";
        public const string PlexKey = "Plex";
        public const string NzbGetKey = "NzbGet";
        private static IKernel Kernel { get; set; }
        private static ICacheService Cache { get; set; }
        static NavBarHelper()
        {
            Kernel = NinjectWebCommon.GetKernel();
            Cache = new InMemoryCache(); //TODO Add this into the IoC container
        }
        public static IHtmlString ShowApplications(this HtmlHelper html)
        {
            var sb = new StringBuilder();
            foreach (var app in Enum.GetValues(typeof(Applications)))
            {
                switch ((Applications)app)
                {
                    case Applications.SabNZBD:
                        var sabService = Cache.GetOrSet(SabKey, () => Kernel.Get<ISettingsService<SabNzbdSettingsDto>>());
                        if (sabService.GetSettings().Enabled)
                        {
                            sb.AppendLine("<li><a href=\"/SabNzbd\">SabNzbd</a></li>");
                        }
                        break;
                    case Applications.Sickbeard: break;

                    case Applications.CouchPotato:
                        var cpService = Cache.GetOrSet(CouchPotatoKey, () => Kernel.Get<ISettingsService<CouchPotatoSettingsDto>>());
                        if (cpService.GetSettings().Enabled)
                        {
                            sb.AppendLine("<li><a href=\"/CouchPotato\">CouchPotato</a></li>");
                        }
                        break;
                    case Applications.Kodi: break;

                    case Applications.Sonarr:
                        var sonarrService = Cache.GetOrSet(SonarrKey, () => Kernel.Get<ISettingsService<SonarrSettingsViewModelDto>>());
                        if (sonarrService.GetSettings().Enabled)
                        {
                            sb.AppendLine("<li><a href=\"/Sonarr\">Sonarr</a></li>");
                        }
                        break;

                    case Applications.Plex:
                        var plexService = Cache.GetOrSet(PlexKey, () => Kernel.Get<ISettingsService<PlexSettingsDto>>());
                        if (plexService.GetSettings().Enabled)
                        {
                            sb.AppendLine("<li><a href=\"/Plex\">Plex</a></li>");
                        }
                        break;

                    case Applications.NzbGet:
                        var nzbgetService = Cache.GetOrSet(NzbGetKey, () => Kernel.Get<ISettingsService<NzbGetSettingsDto>>());
                        if (nzbgetService.GetSettings().Enabled)
                        {
                            sb.AppendLine("<li><a href=\"/NzbGet\">NzbGet</a></li>");
                        }
                        break;

                    case Applications.Headphones: break;

                    default:
                        throw new ArgumentOutOfRangeException("application");
                }
            }


            return html.Raw(sb.ToString());
        }
    }
}