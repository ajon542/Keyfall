using UnityEngine;
using System;
using System.Collections.Generic;


/// <summary>
/// Concrete implementation of the IGameModel abstract base class.
/// </summary>
public class GameModel : IGameModel
{
    /// <summary>
    /// Minimum room width and length.
    /// </summary>
    [Tooltip("Minimum room width and length.")]
    public int minRoomSize = 3;

    /// <summary>
    /// Maximum room width and length.
    /// </summary>
    [Tooltip("Maximum room width and length.")]
    public int maxRoomSize = 15;

    /// <summary>
    /// Average distance between the rooms.
    /// </summary>
    [Tooltip("Average distance between the rooms.")]
    public int roomSpread = 3;

    // TODO: Level dimensions of 1,1 do not work.
    /// <summary>
    /// The level dimensions.
    /// </summary>
    [Tooltip("Number of rooms in the x-direction.")]
    public int levelDimensionsX = 5;

    /// <summary>
    /// The level dimensions.
    /// </summary>
    [Tooltip("Number of rooms in the z-direction.")]
    public int levelDimensionsZ = 5;

    /// <summary>
    /// Split the level into a grid. A room can be placed in each grid location
    /// to ensure there is not room overlap.
    /// </summary>
    private int gridSize;

    /// <summary>
    /// Represents the relationship between each of the rooms.
    /// </summary>
    private IGraph<Room> roomGraph;

    private DungeonLayout[,] dungeonLayout;

    /// <summary>
    /// Represents the grid location of each of the rooms.
    /// </summary>
    private Room[,] rooms;

    /// <summary>
    /// Initialize the dungeon level.
    /// </summary>
    /// <param name="presenter">The game presenter.</param>
    public override void Initialize(Presenter presenter)
    {
        // Let the base class do its thing.
        base.Initialize(presenter);

        // Initialize the floor plan.
        gridSize = maxRoomSize + roomSpread;
        roomGraph = new RoomGraph<Room>();
        rooms = new Room[levelDimensionsX, levelDimensionsZ];

        int width = levelDimensionsX * gridSize;
        int length = levelDimensionsZ * gridSize;
        dungeonLayout = new DungeonLayout[width, length];

        GenerateRoomLayout();
        GenerateRoomConnections();
        GenerateRoomGraph();

        // Publish the floor plan message.
        FloorPlanMsg msg = new FloorPlanMsg();
        msg.RoomGraph = roomGraph;
        msg.DungeonLayout = dungeonLayout;
        msg.Width = width;
        msg.Length = length;
        presenter.PublishMsg(msg);
    }

    /// <summary>
    /// Determine the positions, widths and lengths of all the rooms.
    /// Mark the 2D array with the floor.
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
                        dungeonLayout[i, j] = DungeonLayout.Floor;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Connect the rooms together with flooring.
    /// This is a vey basic algorithm that is bound to fail if the rooms are
    /// too small. It serves as a basis only.
    /// </summary>
    private void GenerateRoomConnections()
    {
        // Make the North-South room connections.
        for (int gridLocationZ = 0; gridLocationZ < levelDimensionsZ - 1; gridLocationZ++)
        {
            for (int gridLocationX = 0; gridLocationX < levelDimensionsX; gridLocationX++)
            {
                Room top = rooms[gridLocationX, gridLocationZ+1];
                Room bottom = rooms[gridLocationX, gridLocationZ];

                // This deals with a straight connection between rooms.
                // If the mid point of the room on the top lies between width of
                // the room on the bottom, we can join them with a straight path.
                int mid = top.PositionX + (top.Width / 2);
                if (mid >= bottom.PositionX && mid < bottom.PositionX + bottom.Width)
                {
                    List<Vector2> path = new List<Vector2>();
                    FixedRoomConnection.Generate(
                        RoomConnection.NorthSouth,
                        new Vector2(mid, bottom.PositionZ + bottom.Length - 1),
                        new Vector2(mid, top.PositionZ),
                        path);

                    foreach (Vector2 pos in path)
                    {
                        dungeonLayout[(int)pos.x, (int)pos.y] = DungeonLayout.Floor;
                    }
                }
                // This case the midpoint of the room on the top is left of the
                // room on the bottom. We need to make an L-shape path. This path
                // can be either an EastNorth or a SouthWest connection. For now,
                // Lets just make it an SouthWest connection.
                else if (mid < bottom.PositionX)
                {
                    List<Vector2> path = new List<Vector2>();
                    FixedRoomConnection.Generate(
                        RoomConnection.SouthWest,
                        new Vector2(mid, top.PositionZ + top.Length),
                        new Vector2(bottom.PositionX, bottom.PositionZ + (bottom.Length / 2)),
                        path);

                    foreach (Vector2 pos in path)
                    {
                        dungeonLayout[(int)pos.x, (int)pos.y] = DungeonLayout.Floor;
                    }
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

                int mid = left.PositionZ + (left.Length / 2);

                // This deals with a straight connection between rooms.
                // If the mid point of the room on the left lies between length of
                // the room on the right, we can join them with a straight path.
                if (mid >= right.PositionZ && mid < right.PositionZ + right.Length)
                {
                    List<Vector2> path = new List<Vector2>();
                    FixedRoomConnection.Generate(
                        RoomConnection.EastWest,
                        new Vector2(left.PositionX + left.Width - 1, mid),
                        new Vector2(right.PositionX, mid),
                        path);

                    foreach (Vector2 pos in path)
                    {
                        dungeonLayout[(int)pos.x, (int)pos.y] = DungeonLayout.Floor;
                    }
                }
                // This case the midpoint of the room on the left is above the
                // room on the right. We need to make an L-shape path. This path
                // can be either a EastNorth or a SouthWest connection. For now,
                // Lets just make it an EastNorth connection.
                else if (mid >= right.PositionZ)
                {
                    List<Vector2> path = new List<Vector2>();
                    FixedRoomConnection.Generate(
                        RoomConnection.EastNorth,
                        new Vector2(left.PositionX + left.Width - 1, mid),
                        new Vector2(right.PositionX + (right.Width / 2), right.PositionZ),
                        path);

                    foreach (Vector2 pos in path)
                    {
                        dungeonLayout[(int)pos.x, (int)pos.y] = DungeonLayout.Floor;
                    }
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
        for (int gridLocationX = 0; gridLocationX < levelDimensionsX; gridLocationX++)
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
        }
    }

    public override void UpdateModel()
    {
        presenter.PublishMsg(new SampleMsg());
    }
}