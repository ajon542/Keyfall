using System.Collections.Generic;
using UnityEngine;

public class DungeonLayoutView : IGameView
{
    /// <summary>
    /// The prefab used for the floor.
    /// </summary>
    public GameObject floorTile;

    /// <summary>
    /// The prefab used for the wall.
    /// </summary>
    public GameObject wall;

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

    /// <summary>
    /// Represents the relationship between each of the rooms.
    /// </summary>
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
            GenerateWalls(room);
        }
    }

    private void GenerateRoom(Room room)
    {
        for (int i = 0; i < room.Width; ++i)
        {
            for (int j = 0; j < room.Length; ++j)
            {
                // TODO: This should be delegated to a RoomView.
                floorArea[i, j] =
                    Instantiate(floorTile, new Vector3(room.PositionX + i, 0, room.PositionZ + j), new Quaternion(1, 0, 0, 1)) as GameObject;
            }
        }
    }

    private void GenerateWalls(Room room)
    {
        // TODO: Keep track of the instantiated game objects.
        //northWall = new List<GameObject>();
        //eastWall = new List<GameObject>();

        // Generate north walls.
        for (int i = 0; i < room.Width; ++i)
        {
            GameObject obj = Instantiate(wall, new Vector3(room.PositionX + i, 0.5f, room.PositionZ + room.Length - 0.5f), Quaternion.identity) as GameObject;
            //northWall.Add(obj);
        }

        // Generate east walls.
        for (int i = 0; i < room.Length; ++i)
        {
            GameObject obj = Instantiate(wall, new Vector3(room.PositionX + room.Width - 0.5f, 0.5f, room.PositionZ + i), Quaternion.Euler(0, 90, 0)) as GameObject;
            //eastWall.Add(obj);
        }
    }

    public override void UpdateView()
    {
    }
}
