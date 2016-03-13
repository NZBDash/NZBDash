#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: LayoutController.cs
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
using System.Collections.Generic;
using System.Web.Mvc;

using NZBDash.Common;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Models.Settings;
using NZBDash.UI.Models.ViewModels;

namespace NZBDash.UI.Controllers
{
    public class LayoutController : Controller
    {
        public LayoutController(ISettingsService<SabNzbdSettingsDto> sab, ISettingsService<CouchPotatoSettingsDto> cp,
            ISettingsService<SonarrSettingsDto> sonarr, ISettingsService<PlexSettingsDto> plex, ISettingsService<NzbGetSettingsDto> nzbget)
        {
            SabService = sab;
            CpService = cp;
            SonarrService = sonarr;
            PlexService = plex;
            NzbService = nzbget;
        }

        private ISettingsService<SabNzbdSettingsDto> SabService { get; set; }
        private ISettingsService<CouchPotatoSettingsDto> CpService { get; set; }
        private ISettingsService<PlexSettingsDto> PlexService { get; set; }
        private ISettingsService<NzbGetSettingsDto> NzbService { get; set; }
        private ISettingsService<SonarrSettingsDto> SonarrService { get; set; }

        // GET: Layout
        public ActionResult ApplicationMenu()
        {
            var model = new List<LayoutModel>();
            foreach (var app in Enum.GetValues(typeof(Applications)))
            {
                switch ((Applications)app)
                {
                    case Applications.SabNZBD:
                        var sabService = SabService.GetSettings();
                        if (sabService.Enabled)
                        {
                            model.Add(
                                new LayoutModel { Name = "SabNzbd", Url = "/SabNzbd" });
                        }
                        break;
                    case Applications.Sickbeard: break;

                    case Applications.CouchPotato:
                        var cpService = CpService.GetSettings();
                        if (cpService.Enabled)
                        {
                            model.Add(
                                new LayoutModel { Name = "CouchPotato", Url = "/CouchPotato" });
                        }
                        break;
                    case Applications.Kodi: break;

                    case Applications.Sonarr:
                        var sonarrService = SonarrService.GetSettings();
                        if (sonarrService.Enabled)
                        {
                            model.Add(
                                new LayoutModel { Name = "Sonarr", Url = "/Sonarr" });
                        }
                        break;

                    case Applications.Plex:
                        var plexService = PlexService.GetSettings();
                        if (plexService.Enabled)
                        {
                            model.Add(
                                new LayoutModel { Name = "Plex", Url = "/Plex" });
                        }
                        break;

                    case Applications.NzbGet:
                        var nzbgetService = NzbService.GetSettings();
                        if (nzbgetService.Enabled)
                        {
                            model.Add(
                                new LayoutModel { Name = "NzbGet", Url = "/NzbGet" });
                        }
                        break;

                    case Applications.Headphones: break;

                    default:
                        throw new ArgumentOutOfRangeException("application");
                }
            }

            return PartialView("NavBarItems", model);
        }
    }
}