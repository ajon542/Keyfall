using System.Collections.Generic;

public class Dungeon : ILevelGenerator
{
    private List<string>[,] townLayout;

    /// <summary>
    /// Represents the grid location of each of the rooms.
    /// </summary>
    private Room[,] rooms;
    private int levelDimensionsX = 5;
    private int levelDimensionsZ = 5;
    private int minRoomSize = 3;
    private int maxRoomSize = 10;
    private int gridSize = 13;

    public List<string>[,] GenerateLevel(int width, int length)
    {
        levelDimensionsX = width;
        levelDimensionsZ = length;
        rooms = new Room[levelDimensionsX, levelDimensionsZ];
        townLayout = new List<string>[width * gridSize, length * gridSize];

        GenerateRoomLayout();
        GenerateRoomConnections();

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
                        townLayout[i, j] = new List<string>();
                        townLayout[i, j].Add("Floor");
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
                    townLayout[path[i].x, path[i].y] = new List<string>();
                    townLayout[path[i].x, path[i].y].Add("Floor");
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
                    townLayout[path[i].x, path[i].y] = new List<string>();
                    townLayout[path[i].x, path[i].y].Add("Floor");
                }
            }
        }
    }
}
