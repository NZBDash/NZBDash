using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models.Settings
{
    public class SabNzbSettingsViewModel : BaseSettingsViewModel
    {
        [Required]
        [Display(Name = "API Key")]
        public string ApiKey { get; set; }
    }
}