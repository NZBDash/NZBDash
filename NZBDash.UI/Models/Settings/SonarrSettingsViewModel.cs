using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models.Settings
{
    public class SonarrSettingsViewModel
    {
        public int Id { get; set; }

        public bool Enabled { get; set; }
        [Required]
        [Display(Name = "IP Address")]
        public string IpAddress { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Api Key")]
        public string ApiKey { get; set; }
        [Required]
        public int Port { get; set; }
    }
}