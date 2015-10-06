namespace NZBDash.Core.Model.Settings
{
    public class SabNzbSettingsDto : BaseSettingsDto
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string ApiKey { get; set; }
    }
}
