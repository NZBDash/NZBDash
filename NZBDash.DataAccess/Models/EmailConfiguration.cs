using System.Collections.Generic;

namespace NZBDash.DataAccess.Models
{
    public class EmailConfiguration : Entity
    {
        public List<string> ToAddress { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
