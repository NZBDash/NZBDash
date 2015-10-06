using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models.Settings
{
    public class NzbGetSettingsViewModel : BaseSettingsViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Username")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}