using Skoda.Entities;

namespace Skoda.Managers;

/// <summary>
/// Interface tram manager
/// </summary>
public interface ITramManager
{
    /// <summary>
    /// Set Init data
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SetInitData(CancellationToken cancellationToken);

    /// <summary>
    /// Get all trams from cache
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<SortedSet<Tram>> GetTrams(CancellationToken cancellationToken);

    /// <summary>
    /// Assigns tram job
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> AssingTramJob(CancellationToken cancellationToken);
}
