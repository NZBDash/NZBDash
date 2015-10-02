namespace NZBDash.Core.Model.DTO
{
    public class ApplicationConfigurationDto
    {
        public int Id { get; set; }
        public string ApplicationName { get; set; }
        public int ApplicationId { get; set; }
        public string ApiKey { get; set; }
        public string IpAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
