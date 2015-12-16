#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: GenericRepository.cs
//  Created By: Jamie Rees
// 
//  Permission is hereby granted, free of charge, to any person obtaining
//  a copy of this software and associated documentation files (the
//  "Software"), to deal in the Software without restriction, including
//  without limitation the rights to use, copy, modify, merge, publish,
//  distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to
//  the following conditions:
// 
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
// 
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//  LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//  WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ***********************************************************************
#endregion
using System.Collections.Generic;

using Dapper.Contrib.Extensions;

using NZBDash.Common.Interfaces;
using NZBDash.Common.Models.Data;
using NZBDash.DataAccessLayer.Interfaces;

namespace NZBDash.DataAccessLayer.Repository
{
    public class GenericRepository<T> : ISqlRepository<T> where T : Entity
    {
        private ISqliteConfiguration Config { get; set; }
        private ILogger Logger { get; set; }
        private ICacheProvider Cache { get; set; }
        private string TypeName { get; set; }

        public GenericRepository(ILogger logger, ISqliteConfiguration config, ICacheProvider cacheProvider)
        {
            Config = config;
            Logger = logger;
            Logger.Trace(string.Format("Started GenericRepository<{0}>", typeof(T)));
            Cache = cacheProvider;
            TypeName = typeof(T).Name;
        }

        public long Insert(T entity)
        {
            ResetCache();
            using (var cnn = Config.DbConnection())
            {
                cnn.Open();
                return cnn.Insert(entity);
            }
        }

        public IEnumerable<T> GetAll()
        {
            var key = TypeName + "GetAll";

            var item = Cache.GetOrSet(key, () =>
                {
                    using (var db = Config.DbConnection())
                    {
                        db.Open();
                        var result = db.GetAll<T>();
                        return result;
                    }
                }, 5);
            return item;

        }

        public T Get(long id)
        {
            var key = TypeName + "Get";
            var item = Cache.GetOrSet(key, () =>
                {
                    using (var db = Config.DbConnection())
                    {
                        db.Open();
                        var result = db.Get<T>(id);
                        return result;
                    }
                }, 5);

            return item;
        }

        public void Delete(T entity)
        {
            ResetCache();
            using (var db = Config.DbConnection())
            {
                db.Open();
                db.Delete(entity);
            }
        }

        public bool Update(T entity)
        {
            ResetCache();
            using (var db = Config.DbConnection())
            {
                db.Open();
                return db.Update(entity);
            }
        }

        private void ResetCache()
        {
            Cache.Remove(TypeName + "Get");
            Cache.Remove(TypeName + "GetAll");
        }
    }
}
