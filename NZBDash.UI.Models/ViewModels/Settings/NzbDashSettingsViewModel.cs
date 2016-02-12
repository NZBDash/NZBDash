using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models.ViewModels.Settings
{
    public class NzbDashSettingsViewModel
    {
        [Display(Name = "Settings_NzbDash_Authenticate", ResourceType = typeof(Resources.Resources))]
        public bool Authenticate { get; set; }
        public int Id { get; set; }

        public bool UserExist { get; set; }

        /// <summary>
        /// A value indicating whether the user has finished the intro.
        /// </summary>
        [Display(Name = "Settings_NzbDash_FinishedIntro", ResourceType = typeof(Resources.Resources))]
        public bool FinishedIntro { get; set; }
    }
}