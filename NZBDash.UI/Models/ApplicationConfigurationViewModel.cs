using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models
{
    public class ApplicationConfigurationViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Application")]
        [Required]
        public string ApplicationName { get; set; }

        public int ApplicationId { get; set; }

        [Display(Name = "Api Key")]
        public string ApiKey { get; set; }

        [Required]
        [Display(Name = "IP Address")]
        public string IpAddress { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}