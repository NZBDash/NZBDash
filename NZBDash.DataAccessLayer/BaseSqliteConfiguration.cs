﻿#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: BaseSqliteConfiguration.cs
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
using System;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

using NZBDash.Common.Interfaces;

namespace NZBDash.DataAccessLayer
{
    public abstract class BaseSqliteConfiguration
    {
        protected BaseSqliteConfiguration(ILogger logger)
        {
            Logger = logger;
        }
        private ILogger Logger { get; set; }

        /// <summary>
        /// Checks the database.
        /// </summary>
        public virtual void CheckDb()
        {
            Logger.Trace("Checking if DB exists");
            if (!File.Exists(DbFile))
            {
                Logger.Trace("Could not find the DB, so we will create it.");
                CreateDatabase();
                return;
            }
            Logger.Trace("Db exists");
        }

        /// <summary>
        /// Gets the assembly directory.
        /// </summary>
        /// <value>
        /// The assembly directory.
        /// </value>
        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        /// <summary>
        /// Gets the database file.
        /// </summary>
        /// <value>
        /// The database file.
        /// </value>
        public virtual string DbFile
        {
            get
            {
            #if WINDOWS || DEBUG
                    return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NZBDash\\" + @"\\NZBDash.sqlite";
            #endif
            #if LINUX
                return "~\\Library\\Application Support" + "\\NZBDash\\" + @"\\NZBDash.sqlite";
            #endif
            }
        }

        /// <summary>
        /// Returns the Database connection.
        /// </summary>
        public virtual SQLiteConnection DbConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFile);
        }

        /// <summary>
        /// Creates the database.
        /// </summary>
        private void CreateDatabase()
        {
            var fs = File.Create(DbFile);
            fs.Close();
            Logger.Trace("Created and Closed the FileStream");
            Logger.Trace("Creating tables");
            try
            {
                TableCreation.CreateLinksConfigurationTable(DbConnection());
                Logger.Trace("Created LinksConfiguration");
            }
            catch (Exception e)
            {
                Logger.Fatal(e);
                throw;
            }
        }
    }
}
