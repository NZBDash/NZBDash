using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models.Dashboard
{
    public class DashboardLinksViewModel
    {
        [Required]
        [Display(Name = "Link Name")]
        public string LinkName { get; set; }

        [Required]
        [Display(Name = "Link Address")]
        [DataType(DataType.Url)]
        public string LinkEndpoint { get; set; }
    }
}