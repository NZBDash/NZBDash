using System.ComponentModel.DataAnnotations;

namespace NZBDash.Common
{
    public enum Applications
    {
        [Display(Name = "SabNZBD")]
        SabNZBD = 0,
        [Display(Name = "Sickbeard")]
        Sickbeard = 1,
        [Display(Name = "CouchPotato")]
        CouchPotato = 2,
        [Display(Name = "Kodi")]
        Kodi = 3,
        [Display(Name = "Sonarr")]
        Sonarr = 4,
        [Display(Name = "Plex")]
        Plex = 5,
        [Display(Name = "NzbGet")]
        NzbGet = 6,
        [Display(Name = "Headphones")]
        Headphones = 7

    }
}
