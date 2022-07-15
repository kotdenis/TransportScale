using Microsoft.Extensions.Caching.Memory;
using TransportScale.Core.Services.Interfacies;

namespace TransportScale.Core.Services.Implementation
{
    public class CacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;
        public CacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetAsync<T>(string key, Func<T> func)
        {
            if (!_memoryCache.TryGetValue(key, out T value))
            {
                value = func();
            }
            await Task.CompletedTask;
            return value;
        }

        public async Task SetAsync<T>(string key, T value)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.UtcNow.AddMinutes(60),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(30)
            };
            _memoryCache.Set(key, value, cacheExpiryOptions);
            await Task.CompletedTask;
        }

        public async Task ClearlAsync(string key)
        {
            _memoryCache.Remove(key);
            await Task.CompletedTask;
        }
    }
}
