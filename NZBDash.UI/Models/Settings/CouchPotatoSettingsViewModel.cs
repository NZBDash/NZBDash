using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models.Settings
{
    public class CouchPotatoSettingsViewModel : BaseSettingsViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Api Key")]
        public string ApiKey { get; set; }
    }
}