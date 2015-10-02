namespace NZBDash.Core.Model.Settings
{
    public class NzbGetSettingsDto : BaseSettingsDto
    {
        public string IpAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
    }
}
