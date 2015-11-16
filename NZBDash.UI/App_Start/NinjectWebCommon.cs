using NZBDash.Api.Controllers;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.Services;
using NZBDash.Core.Settings;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NZBDash.UI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NZBDash.UI.App_Start.NinjectWebCommon), "Stop")]

namespace NZBDash.UI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

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
            kernel.Bind<IHardwareService>().To<WindowsHardwareService>();
            kernel.Bind<IStatusApi>().To<StatusApiController>();

            // Applications
            kernel.Bind<ISettings<NzbGetSettingsDto>>().To<NzbGetSettingsConfiguration>();
            kernel.Bind<ISettings<SabNzbSettingsDto>>().To<SabNzbSettingsConfiguration>();
            kernel.Bind<ISettings<SonarrSettingsViewModelDto>>().To<SonarrSettingsConfiguration>();
            kernel.Bind<ISettings<CouchPotatoSettingsDto>>().To<CouchPotatoSettingsConfiguration>();
            kernel.Bind<ISettings<PlexSettingsDto>>().To<PlexSettingsConfiguration>();
        }
    }
}
