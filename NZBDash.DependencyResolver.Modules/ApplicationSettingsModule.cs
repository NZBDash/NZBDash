using Ninject.Modules;

using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.SettingsService;

namespace NZBDash.DependencyResolver.Modules
{
    public class ApplicationSettingsModule : NinjectModule
    {
        public override void Load()
        {
           Bind<ISettingsService<NzbGetSettingsDto>>().To<NzbGetSettingsService>();
           Bind<ISettingsService<SabNzbSettingsDto>>().To<SabNzbSettingsService>();
           Bind<ISettingsService<SonarrSettingsViewModelDto>>().To<SonarrSettingsService>();
           Bind<ISettingsService<CouchPotatoSettingsDto>>().To<CouchPotatoSettingsService>();
           Bind<ISettingsService<PlexSettingsDto>>().To<PlexSettingsService>();
        }
    }
}
