using System.ComponentModel.DataAnnotations;

using NLog;

namespace NZBDash.UI.Models
{
    public class LogViewModel
    {
        [Display(Name = "Log Level")]
        public LoggingLevel LogLevel { get; set; }
    }
}