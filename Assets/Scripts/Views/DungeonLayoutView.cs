using System.Collections.Generic;
using UnityEngine;

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

    private IGraph<Room> roomGraph;

    [RecvMsgMethod]
    public void HandleFloorPlanMsg(FloorPlanMsg msg)
    {
        Debug.Log("Received Floor Plan");

        // Keep track of the floor plan properties.
        Width = msg.Width;
        Length = msg.Length;
        roomGraph = msg.RoomGraph;
        floorArea = new GameObject[Width, Length];
        List<Room> rooms = roomGraph.VertexList;

        foreach (Room room in rooms)
        {
            GenerateRoom(room);
        }
    }

    private void GenerateRoom(Room room)
    {
        for (int i = 0; i < room.Width; ++i)
        {
            for (int j = 0; j < room.Length; ++j)
            {
                floorArea[i, j] =
                    Instantiate(floorTile, new Vector3(room.PositionX + i, 0, room.PositionZ + j), new Quaternion(1, 0, 0, 1)) as GameObject;
            }
        }
    }

    public override void UpdateView()
    {
    }
}
