namespace NZBDash.DataAccess.Models.Settings
{
    public class SabNzbSettings : SettingsEntity
    {
        public string IpAddress { get; set; }
        public string ApiKey { get; set; }
        public int Port { get; set; }
    }
}
