using Ninject.Modules;

using NZBDash.Api.Controllers;
using NZBDash.Common.Helpers;
using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Services;
using NZBDash.ThirdParty.Api;
using NZBDash.ThirdParty.Api.Interfaces;

namespace NZBDash.DependencyResolver.Modules
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
#if WINDOWS || DEBUG
           Bind<IHardwareService>().To<WindowsHardwareService>();
           Bind<IStatusApi>().To<StatusApiController>();
#endif

#if LINUX
           Bind<IHardwareService>().To<LinuxHardwareService>();
           Bind<IStatusApi>().To<StatusApiController>();
#endif
            Bind<IThirdPartyService>().To<ThirdPartyService>();
            Bind<IWebClient>().To<CustomWebClient>();
        }
    }
}
