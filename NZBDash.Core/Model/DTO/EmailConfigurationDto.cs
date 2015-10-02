using System.Collections.Generic;

namespace NZBDash.Core.Model.DTO
{
    public class EmailConfigurationDto
    {
        public int Id { get; set; }
        public List<string> ToAddress { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
