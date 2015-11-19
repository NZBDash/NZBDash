using System.Linq;

using Ninject;
using Ninject.Parameters;

using NUnit.Framework;

using NZBDash.Common.Interfaces;
using NZBDash.Common.Models.Data.Models;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.DataAccess.Interfaces;
using NZBDash.DependencyResolver.Modules;

namespace NZBDash.DependencyResolver.Test
{
    [TestFixture]
    public class CustomDependencyResolverTest
    {
        [Test]
        public void ResolveServiceDependency()
        {
            var module = new ServiceModule();
            var kernal = new StandardKernel(module);

            var service = kernal.Get<IHardwareService>();
            Assert.That(service,Is.Not.Null);
        }

        [Test]
        public void ResolveApplicationDependency()
        {
            var module = new ApplicationSettingsModule();
            var repoModule = new RepositoryModule();
            var kernal = new StandardKernel(module, repoModule);

            var service = kernal.Get<ISettingsService<PlexSettingsDto>>();
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void ResolveSerializerDependency()
        {
            var module = new SerializerModule();
            var webClientModule = new ServiceModule();
            var kernal = new StandardKernel(module, webClientModule);

            var service = kernal.Get<ISerializer>();
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void ResolveDataDependency()
        {
            var module = new RepositoryModule();
            var kernal = new StandardKernel(module);

            var service = kernal.Get<IRepository<LinksConfiguration>>();
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        [Ignore]
        public void ResolveLoggerDependency()
        {
            var module = new LoggerModule();
            var kernal = new StandardKernel(module);
            var service = kernal.Get<ILogger>(new ConstructorArgument("type", typeof(CustomDependencyResolverTest)));
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void GetCustomModules()
        {
            var customResolver = new CustomDependencyResolver();
            var modules = customResolver.GetModules();

            Assert.That(modules.Count(), Is.GreaterThan(0));
        }
    }
}
