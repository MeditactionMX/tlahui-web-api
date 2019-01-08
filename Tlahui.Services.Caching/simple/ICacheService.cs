using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tlahui.Services.Caching.simple
{
    public interface ICacheService
    {
        T GetOrSet<T>(string cacheKey, Func<T> getItemCallback, int minutes) where T : class;

        void RemoveItem(string cacheKey);

        long ItemCount();
    }

}
