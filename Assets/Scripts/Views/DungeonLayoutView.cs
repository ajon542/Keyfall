using UnityEngine;
using System.Collections;

public class DungeonLayoutView : IGameView
{
    /// <summary>
    /// The prefab used for the floor.
    /// </summary>
    public GameObject floorTile;

    /// <summary>
    /// The array storing the total floor area.
    /// </summary>
    private GameObject[,] floorArea;

    /// <summary>
    /// Gets the width of the room.
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// Gets the length of the room.
    /// </summary>
    public int Length { get; private set; }

    private DungeonLayout[,] floorplan;

    [RecvMsgMethod]
    public void HandleFloorPlanMsg(FloorPlanMsg msg)
    {
        Debug.Log("Received Floor Plan");

        // Keep track of the floor plan properties.
        Width = msg.Width;
        Length = msg.Length;
        floorplan = msg.Floorplan;

        // Generate each floor tile.
        floorArea = new GameObject[Width, Length];

        for (int i = 0; i < msg.Width; ++i)
        {
            for (int j = 0; j < msg.Length; ++j)
            {
                floorArea[i, j] = Instantiate(floorTile, new Vector3(i, 0, j), new Quaternion(1, 0, 0, 1)) as GameObject;
            }
        }
    }

    public override void UpdateView()
    {
    }
}
