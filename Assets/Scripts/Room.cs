using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A room contains the floor and walls.
/// </summary>
public class Room : MonoBehaviour
{
    /// <summary>
    /// The prefab used for the floor.
    /// </summary>
    public GameObject floorTile;

    /// <summary>
    /// The prefab used for the walls.
    /// </summary>
    public GameObject wall;

    /// <summary>
    /// The array storing the total floor area.
    /// </summary>
    private GameObject[,] floorArea;

    /// <summary>
    /// The north wall of the room.
    /// </summary>
    private List<GameObject> northWall;

    /// <summary>
    /// The east wall of the room.
    /// </summary>
    private List<GameObject> eastWall;

    /// <summary>
    /// Gets the width of the room.
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// Gets the length of the room.
    /// </summary>
    public int Length { get; private set; }

    /// <summary>
    /// Generate a room with the given parameters.
    /// </summary>
    /// <param name="position">The position of the room.</param>
    /// <param name="width">The width of the room.</param>
    /// <param name="length">The length of the room.</param>
    public void GenerateRoom(Vector3 position, int width, int length)
    {
        // NOTE: The south and west walls are not needed as they are viewed
        // from behind and aren't rendered anyway.
        floorArea = new GameObject[width, length];
        northWall = new List<GameObject>();
        eastWall = new List<GameObject>();

        // Generate north walls.
        for (int i = 0; i < width; ++i)
        {
            GameObject obj = Instantiate(wall, new Vector3(position.x + i, position.y + 0.5f, position.z + length - 0.5f), Quaternion.identity) as GameObject;
            northWall.Add(obj);
        }

        // Generate east walls.
        for (int i = 0; i < length; ++i)
        {
            GameObject obj = Instantiate(wall, new Vector3(position.x + width - 0.5f, position.y + 0.5f, position.z + i), Quaternion.Euler(0, 90, 0)) as GameObject;
            eastWall.Add(obj);
        }

        // Generate a floor tile for each section of the floor.
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < length; ++j)
            {
                floorArea[i, j] = Instantiate(floorTile, new Vector3(position.x + i, position.y, position.z + j), new Quaternion(1, 0, 0, 1)) as GameObject;
            }
        }

        Width = width;
        Length = length;
    }
}
