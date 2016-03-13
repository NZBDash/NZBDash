#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: LogController.cs
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
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using NLog;

using NZBDash.UI.Models;

using ILogger = NZBDash.Common.Interfaces.ILogger;

namespace NZBDash.UI.Controllers
{
    public class LogController : BaseController
    {
        public LogController(ILogger logger)
            : base(logger)
        {

        }

        // GET: Log
        public ActionResult Index()
        {
            var model = new LogViewModel { LogLevel = LoggingLevel.Warn };
            if (Session["LogLevel"] != null)
            {
                model.LogLevel = (LoggingLevel)Session["LogLevel"];
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(LogViewModel model)
        {
            Session["LogLevel"] = model.LogLevel;
            ReconfigureLogLevel(model.LogLevel);
            return RedirectToAction("Index");
        }

        internal IEnumerable<string> GetLog(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                return new List<string> { "File doesn't exist" };
            }
            var lines = System.IO.File.ReadLines(path, Encoding.UTF8);
            return lines;
        }

        internal IEnumerable<string> LogFiles()
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/NZBDash/";

            return Directory.GetFiles(directory).ToList();
        }

        public static void ReconfigureLogLevel(LoggingLevel level)
        {
            foreach (var rule in LogManager.Configuration.LoggingRules)
            {
                // Remove all levels
                rule.DisableLoggingForLevel(LogLevel.Trace);
                rule.DisableLoggingForLevel(LogLevel.Info);
                rule.DisableLoggingForLevel(LogLevel.Debug);
                rule.DisableLoggingForLevel(LogLevel.Warn);
                rule.DisableLoggingForLevel(LogLevel.Error);
                rule.DisableLoggingForLevel(LogLevel.Fatal);

                switch (level)
                {
                    case LoggingLevel.Trace:
                        rule.EnableLoggingForLevel(LogLevel.Trace);
                        rule.EnableLoggingForLevel(LogLevel.Info);
                        rule.EnableLoggingForLevel(LogLevel.Debug);
                        rule.EnableLoggingForLevel(LogLevel.Warn);
                        rule.EnableLoggingForLevel(LogLevel.Error);
                        rule.EnableLoggingForLevel(LogLevel.Fatal);
                        break;
                    case LoggingLevel.Info:
                        rule.EnableLoggingForLevel(LogLevel.Info);
                        rule.EnableLoggingForLevel(LogLevel.Debug);
                        rule.EnableLoggingForLevel(LogLevel.Warn);
                        rule.EnableLoggingForLevel(LogLevel.Error);
                        rule.EnableLoggingForLevel(LogLevel.Fatal);
                        break;
                    case LoggingLevel.Debug:
                        rule.EnableLoggingForLevel(LogLevel.Debug);
                        rule.EnableLoggingForLevel(LogLevel.Warn);
                        rule.EnableLoggingForLevel(LogLevel.Error);
                        rule.EnableLoggingForLevel(LogLevel.Fatal);
                        break;
                    case LoggingLevel.Warn:
                        rule.EnableLoggingForLevel(LogLevel.Warn);
                        rule.EnableLoggingForLevel(LogLevel.Error);
                        rule.EnableLoggingForLevel(LogLevel.Fatal);
                        break;
                    case LoggingLevel.Error:
                        rule.EnableLoggingForLevel(LogLevel.Error);
                        rule.EnableLoggingForLevel(LogLevel.Fatal);
                        break;
                    case LoggingLevel.Fatal:
                        rule.EnableLoggingForLevel(LogLevel.Fatal);
                        break;
                }
            }

            //Call to update existing Loggers created with GetLogger() or
            //GetCurrentClassLogger()
            LogManager.ReconfigExistingLoggers();
        }
    }
}