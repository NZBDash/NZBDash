using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models.NzbGet
{
    public class NzbGetViewModel
    {
        public string Status { get; set; }
        [Display(Name = "Download Speed")]
        public string DownloadSpeed { get; set; }
    }
}