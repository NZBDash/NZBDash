using System;
using System.Linq;
using System.Threading.Tasks;

using Dapper.Contrib.Extensions;

using Microsoft.AspNet.Identity;

using NZBDash.Common.Interfaces;
using NZBDash.DataAccessLayer.Interfaces;
using NZBDash.DataAccessLayer.Models;

namespace NZBDash.DataAccessLayer.Store
{
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>, IUserSecurityStampStore<User>, IQueryableUserStore<User>
    {
        private ILogger Logger { get; set; }
        private ISqliteConfiguration Db { get; set; }

        public UserStore(ILogger logger, ISqliteConfiguration config)
        {
            Db = config;
            Logger = logger;
        }

        public void Dispose()
        {
            // Nothing to Dispose
        }

        public async Task CreateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            using (var db = Db.DbConnection())
            {
                await db.InsertAsync(user);
            }
        }

        public async Task UpdateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            using (var db = Db.DbConnection())
            {
                await db.UpdateAsync(user);
            }
        }

        public async Task DeleteAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            using (var db = Db.DbConnection())
            {
                await db.DeleteAsync(user);
            }
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException("userId");

            using (var db = Db.DbConnection())
            {
                return await db.GetAsync<User>(userId);
            }
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("userName");

            using (var db = Db.DbConnection())
            {
                var allUsers = await db.GetAllAsync<User>();
                return allUsers.SingleOrDefault(x => x.UserName == userName);
            }
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        public async Task<string> GetPasswordHashAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return await Task.FromResult(user.PasswordHash);
        }

        public async Task<bool> HasPasswordAsync(User user)
        {
            return await Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetSecurityStampAsync(User user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult(user.SecurityStamp);
        }

        public IQueryable<User> Users
        {
            get
            {
                using (var db = Db.DbConnection())
                {
                    var allUsers = db.GetAll<User>();
                    return allUsers.AsQueryable();
                }
            }
        }
    }
}
