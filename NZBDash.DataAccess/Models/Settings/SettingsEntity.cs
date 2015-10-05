
namespace NZBDash.DataAccess.Models.Settings
{
    public abstract class SettingsEntity : Entity
    {
        public bool Enabled { get; set; }
        public bool ShowOnDashboard { get; set; }
    }
}
