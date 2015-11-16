using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models
{
    public enum LoggingLevel
    {
        Trace,
        [Display(Name = "Informational")]
        Info,
        [Display(Name = "Warning")]
        Warn,
        Debug,
        Error,
        Fatal
    }
}