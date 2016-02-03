using System;
using System.IO;

using NUnit.Framework;

using NZBDash.Common;
using NZBDash.Common.Interfaces;

namespace NZBDash.Core.Test
{
    [TestFixture]
    public class NLogLoggerTests
    {
        private ILogger Logger = new NLogLogger(typeof(NLogLoggerTests));

        [Test]
        public void AssertLoggerIsFunctioningCorrectly()
        {
            Logger.Info("info");
            Logger.Info("info{0}",1);
            Logger.Warn("Warn");
            Logger.Warn("Warn{0}",2);
            Logger.Debug("Debug");
            Logger.Debug("Debug{0}",3);
            Logger.Error("Error");
            Logger.Error("Error{1}",1);
            Logger.Error(new Exception());
            Logger.Error(new Exception(), "message{0}", 1);
            Logger.Error("Error", new Exception());
            Logger.Fatal("Fatal");
            Logger.Fatal("Fatal{1}",1);
            Logger.Fatal(new Exception());
            Logger.Fatal(new Exception(),"mssage",1);
        }
    }
}
