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
    /// Gets the width of the room.
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// Gets the length of the room.
    /// </summary>
    public int Length { get; private set; }

    private GameObject RoomsGrid { get; set; }

    private GameObject RoomsGraph { get; set; }

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
        List<Room> rooms = roomGraph.VertexList;

        // Create a root game object to hold all the rooms.
        RoomsGrid = new GameObject { name = "RoomsGrid" };

        for(int i = 0; i < Width; ++i)
        {
            for(int j = 0;j < Length; ++j)
            {
                if(msg.DungeonLayout[i, j] == DungeonLayout.Floor)
                {
                    GameObject obj =
                        Instantiate(floorTile, new Vector3(i, 0, j), new Quaternion(1, 0, 0, 1)) as GameObject;
                    obj.name = "FloorTile";
                    obj.transform.parent = RoomsGrid.transform;
                }
            }
        }

        RoomsGraph = new GameObject { name = "RoomsGraph" };
        
        // Create the rooms based on the data sent from the model.
        foreach (Room room in rooms)
        {
            GameObject roomObj = new GameObject { name = "Room" };
            roomObj.transform.parent = RoomsGraph.transform;

            GenerateFloor(roomObj, room);
            //GenerateWalls(roomObj, room);
        }

        //GenerateTunnels();
    }

    private void GenerateFloor(GameObject parent, Room room)
    {
        GameObject floor = new GameObject { name = "Floor" };
        floor.transform.parent = parent.transform;

        for (int i = 0; i < room.Width; ++i)
        {
            for (int j = 0; j < room.Length; ++j)
            {
                // TODO: This should be delegated to a RoomView.
                GameObject obj =
                    Instantiate(floorTile, new Vector3(room.PositionX + i, 0, room.PositionZ + j), new Quaternion(1, 0, 0, 1)) as GameObject;
                obj.name = "FloorTile";
                obj.transform.parent = floor.transform;
            }
        }
    }

    private void GenerateWalls(GameObject parent, Room room)
    {
        GameObject walls = new GameObject { name = "Walls" };
        walls.transform.parent = parent.transform;

        // Generate north walls.
        for (int i = 0; i < room.Width; ++i)
        {
            GameObject obj
                = Instantiate(wall, new Vector3(room.PositionX + i, 0.5f, room.PositionZ + room.Length - 0.5f), Quaternion.identity) as GameObject;
            obj.name = "North Wall";
            obj.transform.parent = walls.transform;
        }

        // Generate east walls.
        for (int i = 0; i < room.Length; ++i)
        {
            GameObject obj
                = Instantiate(wall, new Vector3(room.PositionX + room.Width - 0.5f, 0.5f, room.PositionZ + i), Quaternion.Euler(0, 90, 0)) as GameObject;
            obj.name = "East Wall";
            obj.transform.parent = walls.transform;
        }
    }

    private void GenerateTunnels()
    {
        List<Vector2> path = new List<Vector2>();
        FixedRoomConnection.Generate(RoomConnection.NorthSouth, new Vector2(0, 0), new Vector2(0, 20), path);

        foreach (Vector2 pos in path)
        {
            GameObject obj =
                Instantiate(floorTile, new Vector3(pos.x, 0, pos.y), new Quaternion(1, 0, 0, 1)) as GameObject;
        }

        path = new List<Vector2>();
        FixedRoomConnection.Generate(RoomConnection.EastWest, new Vector2(0, 0), new Vector2(20, 0), path);

        foreach (Vector2 pos in path)
        {
            GameObject obj =
                Instantiate(floorTile, new Vector3(pos.x, 0, pos.y), new Quaternion(1, 0, 0, 1)) as GameObject;
        }
    }

    public override void UpdateView()
    {
    }
}
