using UnityEngine;
using System.Collections;

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

        // Set everything to floor for a test.
        for (int i = 0; i < 100; ++i)
        {
            for (int j = 0; j < 100; ++j)
            {
                floorplan[i,j] = DungeonLayout.Floor;
            }
        }

        // Publish the floor plan message.
        FloorPlanMsg msg = new FloorPlanMsg();
        msg.Floorplan = floorplan;
        msg.Width = levelDimensions*gridSize;
        msg.Length = levelDimensions*gridSize;
        presenter.PublishMsg(msg);
    }

    public override void UpdateModel()
    {
        presenter.PublishMsg(new SampleMsg());
    }
}