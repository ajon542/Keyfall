using System.Collections.Generic;

public class DungeonGrid : IWeightedGraph<Location>
{
    public static readonly Location[] directions =
        {
            new Location(1, 0),
            new Location(0, -1),
            new Location(-1, 0),
            new Location(0, 1)
        };

    public int width, height;
    public HashSet<Location> walls = new HashSet<Location>();
    public HashSet<Location> forests = new HashSet<Location>();

    public DungeonGrid(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public bool InBounds(Location id)
    {
        return 0 <= id.x && id.x < width
               && 0 <= id.y && id.y < height;
    }

    public bool Passable(Location id)
    {
        return !walls.Contains(id);
    }

    public int Cost(Location a, Location b)
    {
        return forests.Contains(b) ? 5 : 1;
    }

    public IEnumerable<Location> Neighbours(Location id)
    {
        foreach (var dir in directions)
        {
            Location next = new Location(id.x + dir.x, id.y + dir.y);
            if (InBounds(next) && Passable(next))
            {
                yield return next;
            }
        }
    }
}