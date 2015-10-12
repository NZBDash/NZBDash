using System.Web.Mvc;

using NLog;

namespace NZBDash.UI.Controllers
{
    public class LogController : BaseController
    {
        public LogController()
            : base(typeof(LogController))
        {

        }

        // GET: Log
        public ActionResult Index(string log)
        {
            return View();
        }

        public static void ReconfigureLogLevel(LogLevel level)
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

                switch (level.Name)
                {
                    case "Trace":
                        rule.EnableLoggingForLevel(LogLevel.Trace);
                        rule.EnableLoggingForLevel(LogLevel.Info);
                        rule.EnableLoggingForLevel(LogLevel.Debug);
                        rule.EnableLoggingForLevel(LogLevel.Warn);
                        rule.EnableLoggingForLevel(LogLevel.Error);
                        rule.EnableLoggingForLevel(LogLevel.Fatal);
                        break;
                    case "Info":
                        rule.EnableLoggingForLevel(LogLevel.Info);
                        rule.EnableLoggingForLevel(LogLevel.Debug);
                        rule.EnableLoggingForLevel(LogLevel.Warn);
                        rule.EnableLoggingForLevel(LogLevel.Error);
                        rule.EnableLoggingForLevel(LogLevel.Fatal);
                        break;
                    case "Debug":
                        rule.EnableLoggingForLevel(LogLevel.Debug);
                        rule.EnableLoggingForLevel(LogLevel.Warn);
                        rule.EnableLoggingForLevel(LogLevel.Error);
                        rule.EnableLoggingForLevel(LogLevel.Fatal);
                        break;
                    case "Warn":
                        rule.EnableLoggingForLevel(LogLevel.Warn);
                        rule.EnableLoggingForLevel(LogLevel.Error);
                        rule.EnableLoggingForLevel(LogLevel.Fatal);
                        break;
                    case "Error":
                        rule.EnableLoggingForLevel(LogLevel.Error);
                        rule.EnableLoggingForLevel(LogLevel.Fatal);
                        break;
                    case "Fatal":
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