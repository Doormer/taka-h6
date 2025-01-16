using Chat.Application.Common.Configurations;
using Chat.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Chat.Application.Common.Caching;

public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private readonly CacheConfiguration _config;

    public MemoryCacheService(
        IMemoryCache cache,
        IOptions<CacheConfiguration> config)
    {
        _cache = cache;
        _config = config.Value;
    }

    public Task<T?> GetAsync<T>(string key)
    {
        return Task.FromResult(_cache.Get<T>(key));
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var options = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(
                expiration ?? TimeSpan.FromMinutes(_config.DefaultExpirationMinutes));

        _cache.Set(key, value, options);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key)
    {
        _cache.Remove(key);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(string key)
    {
        return Task.FromResult(_cache.TryGetValue(key, out _));
    }
} 