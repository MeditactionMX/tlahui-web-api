using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Tlahui.Services.Caching.simple
{
    public class InMemoryCache: ICacheService
    {
        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback, int minutes) where T : class
        {
            T item = MemoryCache.Default.Get(cacheKey) as T;
            if (item == null)
            {
                item = getItemCallback();
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(minutes));
            }
            return item;
        }

        public void RemoveItem(string cacheKey) {
            
            MemoryCache.Default.Remove(cacheKey);
        }


        public long ItemCount() {
            return MemoryCache.Default.GetCount(); ;
        }

    }
}
