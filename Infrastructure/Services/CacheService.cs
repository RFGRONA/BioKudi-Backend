using Biokudi_Backend.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Biokudi_Backend.Infrastructure.Services
{
    public class CacheService(IMemoryCache memoryCache) : ICacheService
    {
        public T? Get<T>(string key)
        {
            return memoryCache.Get<T>(key);
        }

        public IEnumerable<T>? GetCollection<T>(string key)
        {
            return memoryCache.Get<IEnumerable<T>>(key) ?? null;
        }

        public void Set<T>(string key, T value, TimeSpan expirationTime)
        {
            memoryCache.Set(key, value, expirationTime);
        }

        public void SetCollection<T>(string key, IEnumerable<T> value, TimeSpan expirationTime)
        {
            memoryCache.Set(key, value.ToList(), expirationTime);
        }

        public void Remove(string key)
        {
            memoryCache.Remove(key);
        }
    }
}
