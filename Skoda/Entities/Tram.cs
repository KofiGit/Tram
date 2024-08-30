namespace Skoda.Entities;

/// <summary>
/// Tram class
/// </summary>
public class Tram : IComparable<Tram>
{
    /// <summary>
    /// Position of tram on track
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    /// Has job assigned
    /// </summary>
    public bool HasJob { get; set; }

    /// <summary>
    /// Compare for SortedSet
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(Tram? other)
    {
       return other == null ? 1 : this.Position.CompareTo(other.Position);
    }
}
