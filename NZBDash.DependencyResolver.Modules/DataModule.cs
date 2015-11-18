using Ninject.Modules;

using NZBDash.Common.Models.Data.Models;
using NZBDash.DataAccess.Interfaces;
using NZBDash.DataAccess.Repository;

namespace NZBDash.DependencyResolver.Modules
{
    public class DataModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<LinksConfiguration>>().To<LinksConfigurationRepository>();
        }
    }
}
