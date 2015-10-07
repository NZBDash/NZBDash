using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models.Settings
{
    public class BaseSettingsViewModel
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        [Display(Name = "Display On Dashboard")]
        public bool ShowOnDashboard { get; set; }
        [Required]
        [Display(Name = "IP Address")]
        public string IpAddress { get; set; }
        [Required]
        [Display(Name = "Port")]
        public int Port { get; set; }
    }
}