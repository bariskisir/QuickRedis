using Microsoft.Extensions.Caching.Distributed;
using QuickRedis.Services.Definitions;
using System.Text.Json;
namespace QuickRedis.Services.Implementations;
public class QuickRedisService : IQuickRedisService
{
    private readonly IDistributedCache _distributedCache;
    public QuickRedisService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    public async Task<T> AddOrGetFuncAsync<T>(string key, Func<T> func, int expireSecond = 0, CancellationToken token = default) where T : class
    {
        var cachedItem = await GetAsync<T>(key, token);
        if (cachedItem != null)
        {
            return cachedItem;
        }
        else
        {
            var obj = await SetFuncAsync(key, func, expireSecond, token);
            return obj;
        }
    }
    public async Task<T> GetAsync<T>(string key, CancellationToken token = default) where T : class
    {
        ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));
        var cachedItem = await _distributedCache.GetStringAsync(key, token);
        if (string.IsNullOrEmpty(cachedItem))
        {
            return null;
        }
        return JsonSerializer.Deserialize<T>(cachedItem);
    }
    public async Task RemoveAsync(string key, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));
        await _distributedCache.RemoveAsync(key, token);
    }
    public async Task SetAsync<T>(string key, T obj, int expireSecond = 0, CancellationToken token = default) where T : class
    {
        ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));
        ArgumentNullException.ThrowIfNull(obj, nameof(obj));
        var cacheEntryOptions = expireSecond > 0 ? new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(expireSecond)) : new DistributedCacheEntryOptions();
        string serializedItem = JsonSerializer.Serialize(obj);
        await _distributedCache.SetStringAsync(key, serializedItem, cacheEntryOptions, token);
    }
    public async Task<T> SetFuncAsync<T>(string key, Func<T> func, int expireSecond = 0, CancellationToken token = default) where T : class
    {
        var obj = func.Invoke();
        await SetAsync(key, obj, expireSecond, token);
        return obj;
    }
}