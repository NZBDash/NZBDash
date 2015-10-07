using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models.Settings
{
    public class SonarrSettingsViewModel : BaseSettingsViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Api Key")]
        public string ApiKey { get; set; }
    }
}