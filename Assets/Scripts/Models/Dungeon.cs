using System.Collections.Generic;

public class Dungeon : ILevelGenerator
{
    // House - storage of items
    // General Store - food, water, torches, medical supplies
    // Armour - legs, body, arms, helmets, boots
    // Weapons - swords, daggers, spears, axes
    // Magic / Alchemy - potions, spells, wands, teleportation devices
    // Library - books for learning about various things
    // Black market - rare items, rings, amulets

    private List<TownLayout>[,] townLayout;

    /// <summary>
    /// Represents the grid location of each of the rooms.
    /// </summary>
    private Room[,] rooms;
    int gridSize;
    int levelDimensionsX;
    int levelDimensionsZ;
    int minRoomSize = 3;
    int maxRoomSize = 10;

    public Dungeon(int gridSize, int levelDimensionsX, int levelDimensionsZ)
    {
        this.levelDimensionsX = levelDimensionsX;
        this.levelDimensionsZ = levelDimensionsZ;
        this.gridSize = gridSize;
        rooms = new Room[levelDimensionsX, levelDimensionsZ];
    }

    public List<TownLayout>[,] GenerateLevel(int width, int length)
    {
        townLayout = new List<TownLayout>[width, length];

        GenerateRoomLayout();
        GenerateRoomConnections();
        //GenerateRoomGraph();

        return townLayout;
    }

    /// <summary>
    /// Determine the positions, widths and lengths of all the rooms.
    /// </summary>
    private void GenerateRoomLayout()
    {
        // Generate a room in each grid location.
        System.Random rnd = new System.Random();
        for (int gridLocationX = 0; gridLocationX < levelDimensionsX; gridLocationX++)
        {
            for (int gridLocationZ = 0; gridLocationZ < levelDimensionsZ; gridLocationZ++)
            {
                // Generate a room width and length.
                int width = rnd.Next(minRoomSize, maxRoomSize);
                int length = rnd.Next(minRoomSize, maxRoomSize);

                // Position the room within the grid location.
                int positionX = rnd.Next(0, gridSize - width) + gridLocationX * gridSize;
                int positionZ = rnd.Next(0, gridSize - length) + gridLocationZ * gridSize;

                Room room = new Room(positionX, positionZ, width, length);
                rooms[gridLocationX, gridLocationZ] = room;

                for (int i = positionX; i < positionX + width; ++i)
                {
                    for (int j = positionZ; j < positionZ + length; ++j)
                    {
                        townLayout[i, j] = new List<TownLayout>();
                        townLayout[i, j].Add(TownLayout.Floor);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Connect the rooms together with flooring.
    /// </summary>
    private void GenerateRoomConnections()
    {
        IWeightedGraph<Location> grid = new DungeonGrid(levelDimensionsX * gridSize, levelDimensionsZ * gridSize);
        PathFinder finder = new PathFinder(grid);

        // Make the North-South room connections.
        for (int gridLocationZ = 0; gridLocationZ < levelDimensionsZ - 1; gridLocationZ++)
        {
            for (int gridLocationX = 0; gridLocationX < levelDimensionsX; gridLocationX++)
            {
                Room top = rooms[gridLocationX, gridLocationZ + 1];
                Room bottom = rooms[gridLocationX, gridLocationZ];

                int midTop = top.PositionX + (top.Width / 2);
                int midBottom = bottom.PositionX + (bottom.Width / 2);

                List<Location> path = finder.GetPath(
                    new Location(midBottom, bottom.PositionZ + bottom.Length - 1),
                    new Location(midTop, top.PositionZ));

                for (int i = 0; i < path.Count; ++i)
                {
                    townLayout[path[i].x, path[i].y] = new List<TownLayout>();
                    townLayout[path[i].x, path[i].y].Add(TownLayout.Floor);
                }
            }
        }

        // Make the East-West room connections.
        for (int gridLocationZ = 0; gridLocationZ < levelDimensionsZ; gridLocationZ++)
        {
            for (int gridLocationX = 0; gridLocationX < levelDimensionsX - 1; gridLocationX++)
            {
                Room left = rooms[gridLocationX, gridLocationZ];
                Room right = rooms[gridLocationX + 1, gridLocationZ];

                int midLeft = left.PositionZ + (left.Length / 2);
                int midRight = right.PositionZ + (right.Length / 2);

                List<Location> path = finder.GetPath(
                    new Location(left.PositionX + left.Width - 1, midLeft),
                    new Location(right.PositionX, midRight));

                for (int i = 0; i < path.Count; ++i)
                {
                    townLayout[path[i].x, path[i].y] = new List<TownLayout>();
                    townLayout[path[i].x, path[i].y].Add(TownLayout.Floor);
                }
            }
        }
    }

    /// <summary>
    /// Create the room graph.
    /// </summary>
    /// <remarks>
    /// In general, a room may be connected to another room in an adjacent gridLocation.
    /// For example a room in grid location 0,0 may be connected to a room in 0,1 and 1,0.
    /// A room must have at least one connection.
    /// </remarks>
    private void GenerateRoomGraph()
    {
        // TODO: For now just create an edge between adjacent rooms.
        /*for (int gridLocationX = 0; gridLocationX < levelDimensionsX; gridLocationX++)
        {
            for (int gridLocationZ = 0; gridLocationZ < levelDimensionsZ - 1; gridLocationZ++)
            {
                roomGraph.AddEdge(rooms[gridLocationX, gridLocationZ], rooms[gridLocationX, gridLocationZ + 1]);
            }
        }

        for (int gridLocationZ = 0; gridLocationZ < levelDimensionsZ; gridLocationZ++)
        {
            for (int gridLocationX = 0; gridLocationX < levelDimensionsX - 1; gridLocationX++)
            {
                roomGraph.AddEdge(rooms[gridLocationX, gridLocationZ], rooms[gridLocationX + 1, gridLocationZ]);
            }
        }*/
    }
}
