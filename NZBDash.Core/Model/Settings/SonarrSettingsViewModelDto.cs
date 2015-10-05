namespace NZBDash.Core.Model.Settings
{
    public class SonarrSettingsViewModelDto : BaseSettingsDto
    {
        public string IpAddress { get; set; }
        public string ApiKey { get; set; }
        public int Port { get; set; }
    }
}
