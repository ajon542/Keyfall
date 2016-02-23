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

    [RecvMsgMethod]
    public void HandleGenerateDungeonMsg(GenerateDungeon msg)
    {
        Debug.Log("Received GenerateDungeonMsg");

        // Keep track of the floor plan properties.
        Width = msg.Width;
        Length = msg.Length;

        // Create a root game object to hold all the rooms.
        RoomsGrid = new GameObject { name = "RoomsGrid" };

        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Length; ++j)
            {
                if (msg.DungeonLayout[i, j] == DungeonLayout.Floor)
                {
                    GameObject obj =
                        Instantiate(floorTile, new Vector3(i, 0, j), new Quaternion(1, 0, 0, 1)) as GameObject;
                    obj.name = "FloorTile";
                    obj.transform.parent = RoomsGrid.transform;
                }
            }
        }
    }

    [RecvMsgMethod]
    public void HandleDestroyDungeonMsg(DestroyDungeon msg)
    {
        Debug.Log("Received DestroyDungeonMsg");
        Destroy(RoomsGrid);
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

    public override void UpdateView()
    {
    }
}
