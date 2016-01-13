#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: SqliteModule.cs
//  Created By: Jamie Rees
// 
//  Permission is hereby granted, free of charge, to any person obtaining
//  a copy of this software and associated documentation files (the
//  "Software"), to deal in the Software without restriction, including
//  without limitation the rights to use, copy, modify, merge, publish,
//  distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to
//  the following conditions:
// 
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
// 
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//  LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//  WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ***********************************************************************
#endregion

using System.Data.Common;

using Ninject.Modules;

using NZBDash.Common.Models.Data.Models;
using NZBDash.DataAccessLayer.Configuration;
using NZBDash.DataAccessLayer.Interfaces;
using NZBDash.DataAccessLayer.Repository;

using System.Data.SQLite;
using NZBDash.DataAccessLayer.Models;

#if LINUX 
using Mono.Data.Sqlite;
#endif

namespace NZBDash.DependencyResolver.Modules
{
	public class SqliteModule : NinjectModule
	{
		public override void Load()
		{
            Bind<ISqlRepository<LinksConfiguration>>().To<GenericRepository<LinksConfiguration>>();
		    Bind<ISettingsRepository>().To<JsonRepository>();


            Bind<ISqliteConfiguration>().To<WindowsSqliteConfiguration>();
            Bind<DbProviderFactory>().To<SQLiteFactory>();


#if LINUX 
			Bind<ISqliteConfiguration>().To<MonoSqliteConfiguration>();
		    Bind<DbProviderFactory>().To<SqliteFactory>();
#endif
        }
	}
}

