using Ninject.Modules;

using NZBDash.Common.Models.Data.Models;
using NZBDash.Common.Models.Data.Models.Settings;
using NZBDash.DataAccess.Interfaces;
using NZBDash.DataAccess.Repository;
using NZBDash.DataAccess.Repository.Settings;

namespace NZBDash.DependencyResolver.Modules
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<LinksConfiguration>>().To<LinksConfigurationRepository>();
            Bind<IRepository<CouchPotatoSettings>>().To<CouchPotatoRepository>();
            Bind<IRepository<PlexSettings>>().To<PlexRepository>();
            Bind<IRepository<NzbGetSettings>>().To<NzbGetRepository>();
            Bind<IRepository<DashboardGrid>>().To<DashboardGridRepository>();
            Bind<IRepository<SonarrSettings>>().To<SonarrRepository>();
            Bind<IRepository<SabNzbSettings>>().To<SabNzbRepository>();
        }
    }
}
