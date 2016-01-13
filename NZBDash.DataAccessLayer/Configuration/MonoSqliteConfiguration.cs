#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: MonoSqliteConfiguration.cs
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

using NZBDash.Common.Interfaces;

namespace NZBDash.DataAccessLayer.Configuration
{
    public class MonoSqliteConfiguration : DbConfiguration
    {
        public MonoSqliteConfiguration(ILogger logger, DbProviderFactory provider) : base(logger,provider)
        {
        }

        /// <summary>
        /// Gets the database file.
        /// </summary>
        /// <value>
        /// The database file.
        /// </value>
        public override string DbFile()
        {
            return ApplicationDataLocation() + @"\\NZBDash.sqlite";
        }

        /// <summary>
        /// Applications the data location.
        /// </summary>
        /// <returns></returns>
        public override string ApplicationDataLocation()
        {
            return "App_Data";
        }
    }
}
