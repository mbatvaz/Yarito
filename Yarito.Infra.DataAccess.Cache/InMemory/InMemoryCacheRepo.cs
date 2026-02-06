using Microsoft.Extensions.Caching.Memory;
using Yarito.Domain.Core.Contracts._Common.Repository;

namespace Yarito.Infra.DataAccess.Cache.InMemory
{
    public class InMemoryCacheRepo (IMemoryCache memoryCache) : IInMemoryCacheRepo
    {
        public void Set<T>(string key, T data, TimeSpan time)
        {
            var option = new MemoryCacheEntryOptions()
            {
                SlidingExpiration = time
            };
            memoryCache.Set(key, data, option);
        }

        public T Get<T>(string key)
        {
            return memoryCache.Get<T>(key)!;
        }

        public void Remove(string key)
        {
            memoryCache.Remove(key);
        }
    }
}
