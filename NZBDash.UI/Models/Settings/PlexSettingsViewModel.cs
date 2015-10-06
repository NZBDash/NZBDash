using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models.Settings
{
    public class PlexSettingsViewModel : BaseSettingsViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}