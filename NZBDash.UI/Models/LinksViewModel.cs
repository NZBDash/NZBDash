using System;
using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models
{
    public class LinksViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name= "Link Name")]
        public string LinkName { get; set; }

        [Required]
        [Display(Name = "Link Address")]
        [DataType(DataType.Url)]
        public Uri LinkEndpoint { get; set; }
    }
}