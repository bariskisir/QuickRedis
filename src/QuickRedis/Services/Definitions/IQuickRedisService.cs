using System.Text.Json;
namespace QuickRedis.Services.Definitions;
public interface IQuickRedisService
{
    /// <summary>
    /// Asynchronously gets a object from the specified cache with the specified key.
    /// </summary>
    /// <exception cref="ArgumentException">Will throw exception if key is null or empty</exception>
    /// <exception cref="JsonException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    /// <returns>A task that gets the object value from the stored cache key.</returns>
    Task<T> GetAsync<T>(string key, CancellationToken token = default) where T : class;
    /// <summary>
    /// Sets an object to the specified cache with the specified key.
    /// </summary>
    /// <exception cref="ArgumentException">Will throw exception if key is null or empty</exception>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="ArgumentNullException">Will throw exception if obj is null</exception>
    Task SetAsync<T>(string key, T obj, int expireSecond = 0, CancellationToken token = default) where T : class;
    /// <summary>
    /// Sets an result of func to the specified cache with the specified key.
    /// </summary>
    /// <exception cref="ArgumentException">Will throw exception if key is null or empty</exception>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="ArgumentNullException">Will throw exception if obj is null</exception>
    /// <returns>A task that gets result of func</returns>
    Task<T> SetFuncAsync<T>(string key, Func<T> func, int expireSecond = 0, CancellationToken token = default) where T : class;
    /// <summary>
    /// Gets or adds an result of func to the specified cache with the specified key.
    /// </summary>
    /// <exception cref="ArgumentException">Will throw exception if key is null or empty</exception>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="ArgumentNullException">Will throw exception if obj is null</exception>
    /// <exception cref="JsonException"></exception>
    /// <returns>A task that gets result of func from the stored cache key</returns>
    Task<T> AddOrGetFuncAsync<T>(string key, Func<T> func, int expireSecond = 0, CancellationToken token = default) where T : class;
    /// <summary>
    /// Removes the value with the given key.
    /// </summary>
    /// <exception cref="ArgumentException">Will throw exception if key is null or empty</exception>
    Task RemoveAsync(string key, CancellationToken token = default);
}
