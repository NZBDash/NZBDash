using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models.ViewModels.Settings
{
    public class SabNzbSettingsViewModel : BaseSettingsViewModel
    {
        [Required]
        [Display(Name = "Settings_ApiKey", ResourceType = typeof(Resources.Resources))]
        public string ApiKey { get; set; }
    }
}