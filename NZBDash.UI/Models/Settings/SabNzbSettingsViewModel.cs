using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models.Settings
{
    public class SabNzbSettingsViewModel : BaseSettingsViewModel
    {
        [Required]
        [Display(Name = "IP Address")]
        public string IpAddress { get; set; }
        [Required]
        [Display(Name = "Port")]
        public int Port { get; set; }
        [Required]
        [Display(Name = "API Key")]
        public string ApiKey { get; set; }
    }
}