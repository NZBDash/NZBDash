using Dapper.Contrib.Extensions;

using Microsoft.AspNet.Identity;

namespace NZBDash.DataAccessLayer.Models
{
    [Table("Users")]
    public class User : IUser
    {
        [Key]
        public int UserID { get; set; }

        [Computed]
        public string Id
        {
            get { return UserID.ToString(); }
        }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
    }
}
