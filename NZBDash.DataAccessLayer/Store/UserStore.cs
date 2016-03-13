#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: UserStore.cs
//   Created By: Jamie Rees
//  
//   Permission is hereby granted, free of charge, to any person obtaining
//   a copy of this software and associated documentation files (the
//   "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish,
//   distribute, sublicense, and/or sell copies of the Software, and to
//   permit persons to whom the Software is furnished to do so, subject to
//   the following conditions:
//  
//   The above copyright notice and this permission notice shall be
//   included in all copies or substantial portions of the Software.
//  
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//   EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//   LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//   OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//   WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ************************************************************************/
#endregion
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
            GC.SuppressFinalize(this);
        }

        public async Task CreateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
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
                throw new ArgumentNullException(nameof(user));
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
                throw new ArgumentNullException(nameof(user));
            }
            using (var db = Db.DbConnection())
            {
                await db.DeleteAsync(user);
            }
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));

            using (var db = Db.DbConnection())
            {
                return await db.GetAsync<User>(userId);
            }
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException(nameof(userName));

            using (var db = Db.DbConnection())
            {
                var allUsers = await db.GetAllAsync<User>();
                return allUsers.SingleOrDefault(x => x.UserName == userName);
            }
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        public async Task<string> GetPasswordHashAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await Task.FromResult(user.PasswordHash);
        }

        public async Task<bool> HasPasswordAsync(User user)
        {
            return await Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetSecurityStampAsync(User user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

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
