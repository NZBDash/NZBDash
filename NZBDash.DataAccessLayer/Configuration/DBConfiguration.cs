﻿#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: DbConfiguration.cs
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
using System.Data;
using System.Data.Common;
using System.IO;

using NZBDash.Common.Interfaces;
using NZBDash.DataAccessLayer.Interfaces;

namespace NZBDash.DataAccessLayer.Configuration
{
    public abstract class DbConfiguration : ISqliteConfiguration
    {
        protected DbConfiguration(ILogger logger, DbProviderFactory provider)
        {
            Logger = logger;
            Factory = provider;
        }

        private DbProviderFactory Factory { get; set; }
        private ILogger Logger { get; set; }

        public virtual void CheckDb()
        {
            if (!File.Exists(DbFile()))
            {
                Logger.Trace("Could not find the DB, so we will create it.");
                CreateDatabase();
            }
        }

        public abstract string DbFile();

        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <returns><see cref="IDbConnection"/></returns>
        /// <exception cref="System.Exception">Factory returned null</exception>
        public virtual IDbConnection DbConnection()
        {
            var fact = Factory.CreateConnection();
            if (fact == null)
            {
                throw new Exception("Factory returned null");
            }
            fact.ConnectionString = "Data Source=" + DbFile();
            return fact;
        }

        /// <summary>
        /// Create the database file.
        /// </summary>
        public virtual void CreateDatabase()
        {
            try
            {
                var fs = File.Create(DbFile());
                fs.Close();
                Logger.Trace("Created and Closed the FileStream");
            }
            catch (Exception e)
            {
                Logger.Fatal(e);
                throw;
            }
        }
    }
}
