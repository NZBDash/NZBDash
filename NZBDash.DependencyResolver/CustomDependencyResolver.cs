using System.Collections.Generic;

using Ninject.Modules;

using NZBDash.DependencyResolver.Modules;

namespace NZBDash.DependencyResolver
{
    public class CustomDependencyResolver : IDependencyResolver
    {
        public List<INinjectModule> GetModules()
        {
            var modules = new List<INinjectModule>
            {
                new ServiceModule(),
                new ApplicationSettingsModule(),
                new SerializerModule()
            };

            return modules;
        }
    }
}
