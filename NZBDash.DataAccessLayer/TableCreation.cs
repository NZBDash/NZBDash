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
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Dapper;
using Dapper.Contrib.Extensions;

namespace NZBDash.DataAccessLayer
{
    public static class TableCreation
    {
        /// <summary>
        /// Creates the links configuration table.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public static void CreateLinksConfigurationTable(IDbConnection connection)
        {

            connection.Open();
            connection.Execute(
                @"create table LinksConfigurations
			  (
				 ID                                  integer primary key AUTOINCREMENT,
				 LinkName                           varchar(100) not null,
				 LinkEndpoint                            varchar(1024) not null
			  )");
            connection.Close();
        }

        public static void CreateNzbGetSettingsTable(IDbConnection connection)
        {

            connection.Open();
            connection.Execute(
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
            connection.Close();

        }

        public static void CreateSonarrSettingsTable(IDbConnection connection)
        {

            connection.Open();
            connection.Execute(
                @"create table SonarrSettings
			  (
				 ID                                  integer primary key AUTOINCREMENT,
				 IpAddress                           varchar(100) not null,
				 Port                            integer not null,
				 Enabled           					 integer not null,
				 ShowOnDashboard					 integer not null,
				 ApiKey					 varchar(100) not null

			  )");
            connection.Close();
        }

        public static void CreatePlexSettingsTable(IDbConnection connection)
        {

            connection.Open();
            connection.Execute(
                @"create table PlexSettings
			  (
				 ID                                  integer primary key AUTOINCREMENT,
				 IpAddress                           varchar(100) not null,
				 Port                            integer not null,
				 Enabled           					 integer not null,
				 ShowOnDashboard					 integer not null,
				 Username							 varchar(100) not null,
				 Password							 varchar(100) not null

			  )");
            connection.Close();
        }

        public static void CreateCouchPotatoSettingsTable(IDbConnection connection)
        {

            connection.Open();
            connection.Execute(
                @"create table CouchPotatoSettings
			  (
				ID                                  integer primary key AUTOINCREMENT,
				IpAddress                           varchar(100) not null,
				Port                                integer not null,
				Enabled           					integer not null,
				ShowOnDashboard					    integer not null,
				Username							varchar(100) not null,
				Password							varchar(100) not null,
                ApiKey					            varchar(100) not null
			  )");
            connection.Close();
        }

        public static void CreateSabNzbSettingsSettingsTable(IDbConnection connection)
        {
            connection.Open();
            connection.Execute(
                @"create table SabNzbSettings
			  (
				ID                                  integer primary key AUTOINCREMENT,
				IpAddress                           varchar(100) not null,
				Port                                integer not null,
				Enabled           					integer not null,
				ShowOnDashboard					    integer not null,
                ApiKey					            varchar(100) not null
			  )");
            connection.Close();
        }

        public static List<string> GetAllTables(IDbConnection connection)
        {
            connection.Open();
            var list = new List<string>();
            var result = connection.GetAll<SqliteMasterTable>();
            var records = result.Where(x => x.type == "table");
            foreach (var record in records)
            {
                list.Add(record.name);
            }
            connection.Close();
            return list;
        }

        [Table("sqlite_master")]
        public class SqliteMasterTable
        {
            public string type { get; set; }
            public string name { get; set; }
            public string tbl_name { get; set; }
            [Key]
            public long rootpage { get; set; }
            public string sql { get; set; }
        }
    }
}
