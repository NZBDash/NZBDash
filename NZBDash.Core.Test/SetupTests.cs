#region Copyright
// /************************************************************************
//   Copyright (c) 2016 Jamie Rees
//   File: SettingsServiceTest.cs
//   Created By: Jamie Rees
//  
//   Permission is hereby granted, free of charge, to any person obtaining
//   a copy of this software and associated documentation files (the
//   "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish,
//   distribute, sublicense, and/or sell copies of the Software, and to
//   permit persons to whom the Software is furnished to do so, subject to
//   the following conditions:
//  
//   The above copyright notice and this permission notice shall be
//   included in all copies or substantial portions of the Software.
//  
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//   EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//   LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//   OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//   WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ************************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

using Moq;

using NUnit.Framework;

using NZBDash.Common.Interfaces;
using NZBDash.Common.Models.Settings;
using NZBDash.Core.SettingsService;
using NZBDash.DataAccessLayer.Interfaces;
using NZBDash.DataAccessLayer.Models.Settings;

using Ploeh.AutoFixture;

using GlobalSettings = NZBDash.DataAccessLayer.Models.Settings.GlobalSettings;

namespace NZBDash.Core.Test
{
    [TestFixture]
    public class SetupTests
    {
        private Setup Setup { get; set; }
        private Mock<ISqliteConfiguration> Sql { get; set; }
        private Mock<DbProviderFactory> Db { get; set; }
        private Mock<IDbConnection> DbConnection { get; set; }
        private Mock<ILogger> Logger { get; set; }


        [SetUp]
        public void SetUp()
        {
            //ISqliteConfiguration sql, ILogger logger, DbProviderFactory factory
            Sql = new Mock<ISqliteConfiguration>();
            Logger = new Mock<ILogger>();
            Db = new Mock<DbProviderFactory>();

            DbConnection = new Mock<IDbConnection>();
            var dbCommand = new Mock<IDbCommand>();

            DbConnection.Setup(x => x.CreateCommand()).Returns(dbCommand.Object);

            Sql.Setup(x => x.DbConnection()).Returns(DbConnection.Object);

            Setup = new Setup(Sql.Object, Logger.Object, Db.Object);
        }

        [Test]
        public void StartTest()
        {

            var result = Setup.Start();

            Sql.Verify(x => x.CheckDb(),Times.Once);
            Sql.Verify(x => x.DbConnection(),Times.Once);

            DbConnection.Verify(x => x.Open(),Times.AtLeastOnce);
            DbConnection.Verify(x => x.Close(),Times.AtLeastOnce);
            Assert.That(result, Is.True);
        }

        [Test]
        public void StartWithExceptionLogsToLogger()
        {
            Sql.Setup(x => x.CheckDb()).Throws(new DataException());

            Setup = new Setup(Sql.Object, Logger.Object, Db.Object);
            var result = Setup.Start();

            Sql.Verify(x => x.CheckDb(), Times.Once);
            Sql.Verify(x => x.DbConnection(), Times.Never);

            DbConnection.Verify(x => x.Open(), Times.Never);
            DbConnection.Verify(x => x.Close(), Times.Never);
            Logger.Verify(x => x.Fatal(It.IsAny<string>()), Times.Once);
            Logger.Verify(x => x.Fatal(It.IsAny<Exception>()), Times.Once);
            Assert.That(result, Is.False);
        }

    }
}
