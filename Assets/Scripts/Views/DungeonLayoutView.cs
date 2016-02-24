﻿using System.Collections.Generic;
using UnityEngine;

public class DungeonLayoutView : IGameView
{
    /// <summary>
    /// Minimum room width and length.
    /// </summary>
    public int minRoomSize = 3;

    /// <summary>
    /// Maximum room width and length.
    /// </summary>
    public int maxRoomSize = 10;

    /// <summary>
    /// Average distance between the rooms.
    /// </summary>
    public int roomSpread = 3;

    // TODO: Level dimensions of 1,1 do not work.
    /// <summary>
    /// The level dimensions.
    /// </summary>
    public int levelDimensionsX = 5;

    /// <summary>
    /// The level dimensions.
    /// </summary>
    public int levelDimensionsZ = 5;

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

        int width = msg.DungeonLayout.GetLength(0);
        int height = msg.DungeonLayout.GetLength(1); 

        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Length; ++j)
            {
                if (msg.DungeonLayout[i, j] != null && msg.DungeonLayout[i, j].Count > 0 && msg.DungeonLayout[i, j][0] == TownLayout.Floor)
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
