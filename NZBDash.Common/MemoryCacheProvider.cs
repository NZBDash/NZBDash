#region Copyright
// ************************************************************************
//   Copyright (c) 2015 
//   File: MemoryCacheProvider.cs
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
// ************************************************************************
#endregion
using System;
using System.Runtime.Caching;

using NZBDash.Common.Interfaces;

namespace NZBDash.Common
{
    public class MemoryCacheProvider : ICacheProvider
    {
        private ObjectCache Cache { get { return MemoryCache.Default; } }

        /// <summary>
        /// Gets the item from the cache, if the item is not present 
        /// then we will get that item and store it in the cache.
        /// </summary>
        /// <typeparam name="T">Type to store in the cache</typeparam>
        /// <param name="key">The key</param>
        /// <param name="itemCallback">The item callback. This will be called if the item is not present in the cache.
        /// NB: if the callback is null and the item is not in the cache it will throw a <see cref="NullReferenceException"/>
        /// </param>
        /// <param name="cacheTime">The amount of time we want to cache the object</param>
        /// <returns></returns>
        public T GetOrSet<T>(string key, Func<T> itemCallback, int cacheTime) where T : class
        {
            var item = Get<T>(key);
            if (item == null)
            {
                item = itemCallback();
                if (item != null)
                {
                    Set(key, item, cacheTime);
                }
            }
            return item;
        }

        /// <summary>
        /// Gets the specified item from the cache.
        /// </summary>
        /// <typeparam name="T">Type to get from the cache</typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T Get<T>(string key) where T : class
        {
            var item = Cache.Get(key) as T;
            return item;
        }

        /// <summary>
        /// Set/Store the specified object in the cache
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The object we want to store.</param>
        /// <param name="cacheTime">The amount of time we want to cache the object.</param>
        public void Set(string key, object data, int cacheTime)
        {
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime) };
            Cache.Add(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// Removes the specified object from the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            var item = Cache.Get(key);
            if (item != null)
            {
                Cache.Remove(key);
            }
        }
    }
}