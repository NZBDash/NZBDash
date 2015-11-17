using System;
using System.Web;

using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using Ninject;
using Ninject.Web.Common;

using NZBDash.Api.Controllers;
using NZBDash.Common.Helpers;
using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.Services;
using NZBDash.Core.SettingsService;
using NZBDash.ThirdParty.Api;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.UI.App_Start;

using WebActivatorEx;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace NZBDash.UI.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
#if WINDOWS
            kernel.Bind<IHardwareService>().To<WindowsHardwareService>();
            kernel.Bind<IStatusApi>().To<StatusApiController>();
#endif

#if LINUX
            kernel.Bind<IHardwareService>().To<LinuxHardwareService>();
            kernel.Bind<IStatusApi>().To<StatusApiController>();
#endif
            // Services
            kernel.Bind<IThirdPartyService>().To<ThirdPartyService>();

            // Applications
            kernel.Bind<ISettingsService<NzbGetSettingsDto>>().To<NzbGetSettingsService>();
            kernel.Bind<ISettingsService<SabNzbSettingsDto>>().To<SabNzbSettingsService>();
            kernel.Bind<ISettingsService<SonarrSettingsViewModelDto>>().To<SonarrSettingsService>();
            kernel.Bind<ISettingsService<CouchPotatoSettingsDto>>().To<CouchPotatoSettingsServiceService>();
            kernel.Bind<ISettingsService<PlexSettingsDto>>().To<PlexSettingsService>();

            // Serializer
            kernel.Bind<ISerializer>().To<ThirdPartySerializer>();

        }
    }
}
