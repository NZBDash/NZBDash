namespace NZBDash.Core.Model.Settings
{
    public class SonarrSettingsViewModelDto
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public string IpAddress { get; set; }
        public string ApiKey { get; set; }
        public int Port { get; set; }
    }
}
