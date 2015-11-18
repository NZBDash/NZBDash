using Ninject;

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
            var kernal = new StandardKernel(module);

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
            var module = new DataModule();
            var kernal = new StandardKernel(module);

            var service = kernal.Get<IRepository<LinksConfiguration>>();
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void GetCustomModules()
        {
            var customResolver = new CustomDependencyResolver();
            var modules = customResolver.GetModules();

            Assert.That(modules.Count, Is.GreaterThan(0));
        }
    }
}
