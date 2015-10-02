namespace NZBDash.DataAccess.Models.Settings
{
    public class NzbGetSettings : SettingsEntity
    {
        public string IpAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
    }
}
