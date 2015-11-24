using System;
using Ninject.Modules;
using NZBDash.DataAccessLayer.Interfaces;
using NZBDash.DataAccessLayer;
using NZBDash.Common.Models.Data.Models.Settings;

namespace NZBDash.DependencyResolver.Modules
{
	public class SqliteModule : NinjectModule
	{
		public override void Load()
		{

			Bind<ISqlRepository<SonarrSettings>>().To<SonarrConfiguration> ();

			#if WINDOWS
			Bind<ISqliteConfiguration>().To<WindowsSqliteConfiguration>();
			#endif
			#if LINUX || DEBUG
			Bind<ISqliteConfiguration>().To<MonoSqliteConfiguration>();
			#endif
		}
	}
}

