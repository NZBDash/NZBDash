using Ninject.Modules;

using NZBDash.Common.Helpers;
using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Services;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.ThirdParty.Api.Service;

namespace NZBDash.DependencyResolver.Modules
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
#if WINDOWS || DEBUG
            Bind<IHardwareService>().To<WindowsHardwareService>();
#endif

#if LINUX
           Bind<IHardwareService>().To<MonoHardwareService>();
#endif
            //Bind<IThirdPartyService>().To<ThirdPartyService>();
            Bind<IThirdPartyService>().To<ThirdPartyMockService>();
            Bind<IWebClient>().To<CustomWebClient>();
        }
    }
}
