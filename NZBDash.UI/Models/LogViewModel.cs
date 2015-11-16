using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models
{
    public class LogViewModel
    {
        [Display(Name = "Log Level")]
        public LoggingLevel LogLevel { get; set; }

        public IEnumerable<string> Lines { get; set; }
    }
}