using Microsoft.Extensions.Caching.Distributed;
using Skoda.Common;
using Skoda.Entities;
using Skoda.Helpers;

namespace Skoda.Managers;

/// <summary>
/// Manager class for tram
/// </summary>
public class TramManager(
    IDistributedCache cache,
    ILogger<TramManager> logger
    ) : ITramManager
{
    /// <summary>
    /// Set Init data
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task SetInitData(CancellationToken cancellationToken)
    {
        SortedSet<Tram> tramsInit = [];

        foreach (var index in Enumerable.Range(0, 1000))
        {
            tramsInit.Add(new() { Position = index, HasJob = index < 450 });
        }

        await SetDataInCache(tramsInit, cancellationToken);
    }

    /// <summary>
    /// Get all trams from cache
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<SortedSet<Tram>> GetTrams(CancellationToken cancellationToken) => 
        cache.GetJsonAsync<SortedSet<Tram>>(SharedCacheKeys.TRAM_CACHE_KEY, cancellationToken);


    /// <summary>
    /// Assigns tram job
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<int> AssingTramJob(CancellationToken cancellationToken)
    {
        var trams = await GetTrams(cancellationToken);    
        var firstTramWithoutJob = trams.FirstOrDefault(t => !t.HasJob);

        if (firstTramWithoutJob == null)
        {
            return -1;
        }

        firstTramWithoutJob.HasJob = true;
        trams.Add(firstTramWithoutJob);

        await SetDataInCache(trams, cancellationToken);

        return firstTramWithoutJob.Position;
    }

    /// <summary>
    /// Set data in cache
    /// </summary>
    /// <param name="trams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task SetDataInCache(SortedSet<Tram> trams, CancellationToken cancellationToken)
    {
        //Here might happen pessimistic collision, data are update by update call but can be called before update
        //Then who called can have older version of data

        var options = new DistributedCacheEntryOptions();
        await cache.SetJsonAsync<SortedSet<Tram>>(SharedCacheKeys.TRAM_CACHE_KEY, trams, options, cancellationToken);
    }
}

