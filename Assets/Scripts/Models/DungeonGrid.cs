using System.Collections.Generic;

public class DungeonGrid : IWeightedGraph<Location>
{
    // TODO: These may or may not be useful for our maps type.
    public HashSet<Location> walls = new HashSet<Location>();
    public HashSet<Location> forests = new HashSet<Location>();

    /// <summary>
    /// Specifies the allowable movement directions this grid supports.
    /// </summary>
    public static readonly Location[] directions =
        {
            new Location(1, 0),
            new Location(0, -1),
            new Location(-1, 0),
            new Location(0, 1)
        };

    /// <summary>
    /// Gets the width of the grid.
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    ///  Gets the length of the grid.
    /// </summary>
    public int Length { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="DungeonGrid"/> class.
    /// </summary>
    /// <param name="width">The width of the grid.</param>
    /// <param name="length">The length of the grid.</param>
    public DungeonGrid(int width, int length)
    {
        Width = width;
        Length = length;
    }

    /// <summary>
    /// Determines whether the given location is withing the grid bounds.
    /// </summary>
    /// <param name="id">The location.</param>
    /// <returns><c>true</c> if the location is with the bounds; <c>false</c> otherwise.</returns>
    private bool InBounds(Location id)
    {
        return (0 <= id.x && id.x < Width) && 
               (0 <= id.y && id.y < Length);
    }

    /// <summary>
    /// Determines whether the given location is passable.
    /// </summary>
    /// <param name="id">The location.</param>
    /// <returns><c>true</c> if the location is passable; <c>false</c> otherwise.</returns>
    private bool Passable(Location id)
    {
        return !walls.Contains(id);
    }

    #region IWeightedGraph Implementation

    /// <summary>
    /// Determines the cost of moving onto the given location.
    /// </summary>
    /// <param name="id">The location.</param>
    /// <returns>The cost of moving onto the given location.</returns>
    public int Cost(Location id)
    {
        return forests.Contains(id) ? 5 : 1;
    }

    /// <summary>
    /// Gets the neighbours of the given location.
    /// </summary>
    /// <param name="id">The location.</param>
    /// <returns>The neighbouring locations.</returns>
    public IEnumerable<Location> Neighbours(Location id)
    {
        foreach (var dir in directions)
        {
            Location neighbour = new Location(id.x + dir.x, id.y + dir.y);
            if (InBounds(neighbour) && Passable(neighbour))
            {
                yield return neighbour;
            }
        }
    }

    #endregion
}