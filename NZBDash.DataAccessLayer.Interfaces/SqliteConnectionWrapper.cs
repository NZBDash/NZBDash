using System;

#if WINDOWS
using System.Data.SQLite;
#endif
#if LINUX || DEBUG
using Mono.Data.Sqlite;
#endif

namespace NZBDash.DataAccessLayer
{
	#if LINUX || DEBUG
	public class SqliteConnectionWrapper
	{
		private string ConnectionString { get; set; }
		public SqliteConnectionWrapper (string connectionString)
		{
			ConnectionString = connectionString;
		}

		public SqliteConnection GetConnection()
		{
			return new SqliteConnection(ConnectionString);
		}
	}
	#endif

	#if WINDOWS
	public class SqliteConnectionWrapper
	{
		private string ConnectionString { get; set; }

		public SqliteConnectionWrapper (string connectionString)
		{
			ConnectionString = connectionString;
		}

		public SQLiteConnection GetConnection()
		{
			return new SQLiteConnection(ConnectionString);
		}
	}
	#endif
}
