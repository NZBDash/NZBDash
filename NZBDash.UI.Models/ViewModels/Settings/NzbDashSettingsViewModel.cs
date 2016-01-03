using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models.ViewModels.Settings
{
    public class NzbDashSettingsViewModel
    {
        [Required]
        [Display(Name = "Settings_NzbDash_Authenticate", ResourceType = typeof(Resources.Resources))]
        public bool Authenticate { get; set; }
        public int Id { get; set; }

        public bool UserExist { get; set; }
    }
}