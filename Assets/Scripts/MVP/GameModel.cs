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

    /// <summary>
    /// The floor plan of the dungeon. This layout is passed and interpreted by
    /// the DungeonLayoutView to generate the graphical representation. 
    /// </summary>
    private DungeonLayout[,] floorplan;

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
        floorplan = new DungeonLayout[levelDimensions * gridSize, levelDimensions * gridSize];

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

                    // Create the floor.
                    GenerateFloor(new Vector3(positionX, 0, positionZ), width, length);
                }
            }
        }

        // Publish the floor plan message.
        FloorPlanMsg msg = new FloorPlanMsg();
        msg.Floorplan = floorplan;
        msg.Width = levelDimensions*gridSize;
        msg.Length = levelDimensions*gridSize;
        presenter.PublishMsg(msg);
    }

    private void GenerateFloor(Vector3 position, int width, int length)
    {
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < length; ++j)
            {
                floorplan[(int)position.x + i, (int)position.z + j] = DungeonLayout.Floor;
            }
        }
    }

    public override void UpdateModel()
    {
        presenter.PublishMsg(new SampleMsg());
    }
}