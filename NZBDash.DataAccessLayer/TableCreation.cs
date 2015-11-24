#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: TableCreation.cs
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
using System.Data.SQLite;

using Dapper;

namespace NZBDash.DataAccessLayer
{
    public static class TableCreation
    {
        /// <summary>
        /// Creates the links configuration table.
        /// </summary>
        /// <param name="connection">The connection.</param>
		public static void CreateLinksConfigurationTable(SqliteConnectionWrapper connection)
        {
			using (var con = connection.GetConnection())
            {
                con.Open();
                con.Execute(
                    @"create table LinksConfigurations
              (
                 ID                                  integer primary key AUTOINCREMENT,
                 LinkName                           varchar(100) not null,
                 LinkEndpoint                            varchar(1024) not null
              )");
            }
        }

		public static void CreateNzbGetSettingsTable(SqliteConnectionWrapper connection)
		{
			using (var con = connection.GetConnection())
			{
				con.Open();
				con.Execute(
					@"create table NzbGetSettings
              (
                 ID                                  integer primary key AUTOINCREMENT,
                 IpAddress                           varchar(100) not null,
                 Port                            integer not null,
				 Enabled           					 integer not null,
				 ShowOnDashboard					 integer not null,
				 Username							 varchar(100) not null,
				 Password							 varchar(100) not null	
              )");
			}
		}

		public static void CreateSonarrSettingsTable(SqliteConnectionWrapper connection)
		{
			using (var con = connection.GetConnection())
			{
				con.Open();
				con.Execute(
					@"create table SonarrSettings
              (
                 ID                                  integer primary key AUTOINCREMENT,
                 IpAddress                           varchar(100) not null,
                 Port                            integer not null,
				 Enabled           					 integer not null,
				 ShowOnDashboard					 integer not null,
				 ApiKey					 varchar(100) not null
				 
              )");
			}
		}
    }
}
