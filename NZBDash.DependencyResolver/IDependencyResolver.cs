using System.Collections.Generic;

using Ninject.Modules;

namespace NZBDash.DependencyResolver
{
    public interface IDependencyResolver
    {
        List<INinjectModule> GetModules();
    }
}
