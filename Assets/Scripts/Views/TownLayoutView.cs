using UnityEngine;
using System.Collections;

public class TownLayoutView : IGameView
{
    /// <summary>
    /// The prefab used for the floor.
    /// </summary>
    public GameObject floorTile;

    /// <summary>
    /// The prefab used for the wall.
    /// </summary>
    public GameObject wall;

    private GameObject RoomsGrid { get; set; }

    /// <summary>
    /// Gets the width of the room.
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// Gets the length of the room.
    /// </summary>
    public int Length { get; private set; }

    [RecvMsgMethod]
    public void HandleGenerateTownMsg(GenerateTown msg)
    {
        Debug.Log("Received GenerateTownMsg");

        // Keep track of the floor plan properties.
        Width = msg.Width;
        Length = msg.Length;

        // Create a root game object to hold all the rooms.
        RoomsGrid = new GameObject { name = "RoomsGrid" };

        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Length; ++j)
            {
                if (msg.TownLayout[i, j][0] == TownLayout.Floor)
                {
                    GameObject obj =
                        Instantiate(floorTile, new Vector3(i, 0, j), new Quaternion(1, 0, 0, 1)) as GameObject;
                    obj.name = "FloorTile";
                    obj.transform.parent = RoomsGrid.transform;
                }
            }
        }
    }
}
