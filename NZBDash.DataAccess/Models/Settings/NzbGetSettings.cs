namespace NZBDash.DataAccess.Models.Settings
{
    public class CouchPotatoSettings : SettingsEntity
    {
        public string IpAddress { get; set; }
        public string ApiKey { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
    }
}
