using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Skoda.Helpers;

/// <summary>
/// Helper for cache
/// </summary>
public static class CacheHelper
{
    /// <summary>
    /// Set Json to cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cache"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task SetJsonAsync<T>(
        this IDistributedCache cache, 
        string key, 
        T value, 
        DistributedCacheEntryOptions options, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(cache, nameof(cache));
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key));
        }

        var data = JsonSerializer.SerializeToUtf8Bytes<T>(value);
        return cache.SetAsync(key, data, options, cancellationToken);
    }

    /// <summary>
    /// Get expected value from cache
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="cache"></param>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<TResult?> GetJsonAsync<TResult>(
        this IDistributedCache cache, 
        string key, 
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(cache, nameof(cache));
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key));
        }

        var data = await cache.GetAsync(key, cancellationToken);
        return data != null ? JsonSerializer.Deserialize<TResult>(data) : default(TResult);
    }
}
