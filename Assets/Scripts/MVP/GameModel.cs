using UnityEngine;

/// <summary>
/// Concrete implementation of the IGameModel abstract base class.
/// </summary>
public class GameModel : IGameModel
{
    /// <summary>
    /// Minimum room width or length.
    /// </summary>
    public int minRoomDimensions = 3;

    /// <summary>
    /// Maximum room width or length. Ideally, this should
    /// be less than the gridSize.
    /// </summary>
    public int maxRoomDimensions = 15;

    /// <summary>
    /// Minimum gap between the rooms.
    /// </summary>
    public int minRoomGap = 3;

    /// <summary>
    /// The level dimensions.
    /// </summary>
    public int levelDimensions = 5;

    /// <summary>
    /// Probability of generating a room.
    /// </summary>
    public int roomProbability = 100;

    /// <summary>
    /// Split the level into a grid. A room can be placed in each grid location
    /// to ensure there is not room overlap.
    /// </summary>
    private int gridSize;

    private IGraph<Room> roomGraph;

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
        gridSize = maxRoomDimensions + minRoomGap;
        roomGraph = new RoomGraph<Room>();
        rooms = new Room[levelDimensions, levelDimensions];

        GenerateRoomLayout();
        GenerateRoomGraph();

        // Publish the floor plan message.
        FloorPlanMsg msg = new FloorPlanMsg();
        msg.RoomGraph = roomGraph;
        msg.Width = levelDimensions*gridSize;
        msg.Length = levelDimensions*gridSize;
        presenter.PublishMsg(msg);
    }

    /// <summary>
    /// Determine the positions, widths and lengths of all the rooms.
    /// </summary>
    private void GenerateRoomLayout()
    {
        // Generate a room in each grid location.
        System.Random rnd = new System.Random();
        for (int gridLocationX = 0; gridLocationX < levelDimensions; gridLocationX++)
        {
            for (int gridLocationZ = 0; gridLocationZ < levelDimensions; gridLocationZ++)
            {
                // Generate room based on a given percentage.
                if (rnd.Next(100) < roomProbability)
                {
                    // Generate a room width and length.
                    int width = rnd.Next(minRoomDimensions, maxRoomDimensions);
                    int length = rnd.Next(minRoomDimensions, maxRoomDimensions);

                    // Position the room within the grid location.
                    int positionX = rnd.Next(0, gridSize - width) + gridLocationX * gridSize;
                    int positionZ = rnd.Next(0, gridSize - length) + gridLocationZ * gridSize;

                    rooms[gridLocationX, gridLocationZ] = new Room(positionX, positionZ, width, length);
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
        for (int gridLocationX = 0; gridLocationX < levelDimensions; gridLocationX++)
        {
            for (int gridLocationZ = 0; gridLocationZ < levelDimensions - 1; gridLocationZ++)
            {
                roomGraph.AddEdge(rooms[gridLocationX, gridLocationZ], rooms[gridLocationX, gridLocationZ + 1]);
            }
        }

        for (int gridLocationZ = 0; gridLocationZ < levelDimensions; gridLocationZ++)
        {
            for (int gridLocationX = 0; gridLocationX < levelDimensions - 1; gridLocationX++)
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