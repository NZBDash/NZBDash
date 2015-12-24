using NZBDash.DataAccessLayer.Models.Settings;

namespace NZBDash.Common.Models.Data.Models
{
    public class LinksConfiguration : Entity
    {
        public string LinkName { get; set; }
        public string LinkEndpoint { get; set; }
    }
}
