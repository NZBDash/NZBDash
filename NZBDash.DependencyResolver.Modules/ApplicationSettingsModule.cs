﻿#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: ApplicationSettingsModule.cs
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
using Ninject;
using Ninject.Modules;

using NZBDash.Common.Models.Data.Models;
using NZBDash.Core.Configuration;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.SettingsService;
using NZBDash.DataAccessLayer.Interfaces;
using NZBDash.DataAccessLayer.Models.Settings;

namespace NZBDash.DependencyResolver.Modules
{
    public class ApplicationSettingsModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<ISettingsService<NzbGetSettingsDto>>().To<SettingsService<NzbGetSettings, NzbGetSettingsDto>>();
            Bind<ISettingsService<SabNzbdSettingsDto>>().To<SettingsService<SabNzbSettings, SabNzbdSettingsDto>>();
            Bind<ISettingsService<SonarrSettingsDto>>().To<SettingsService<SonarrSettings, SonarrSettingsDto>>();
            Bind<ISettingsService<CouchPotatoSettingsDto>>().To<SettingsService<CouchPotatoSettings, CouchPotatoSettingsDto>>();
            Bind<ISettingsService<PlexSettingsDto>>().To<SettingsService<PlexSettings, PlexSettingsDto>>();
            Bind<ISettingsService<NzbDashSettingsDto>>().To<SettingsService<NzbDashSettings, NzbDashSettingsDto>>();

            Bind<ILinksConfiguration>().To<LinksConfigurationService>().WithConstructorArgument("repo", x => x.Kernel.Get<ISqlRepository<LinksConfiguration>>());
        }

    }
}
