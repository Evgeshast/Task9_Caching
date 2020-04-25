using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindLibrary;
using System.Runtime.Caching;

namespace CachingSolutionsSamples
{
	public class MemoryCache<T> : ICache<T>
	{
		ObjectCache cache = MemoryCache.Default;

        public T Get(string key)
        {
            var cacheValue = cache.Get(key);
            if (cacheValue == null)
            {
                return default(T);
            }

            return (T)cacheValue;
        }

		public void Set(string key, T categories, DateTimeOffset expirationDateTime)
		{
			cache.Set(key, categories, expirationDateTime);
		}

        public void Set(string key, T value, CacheItemPolicy policy)
        {
            cache.Set( key, value, policy);
        }
    }
}
