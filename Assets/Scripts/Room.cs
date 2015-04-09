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
    /// The prefab used for the doors.
    /// </summary>
    public GameObject door;

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
    /// The east wall of the room.
    /// </summary>
    private List<GameObject> doors;

    /// <summary>
    /// Gets the width of the room.
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// Gets the length of the room.
    /// </summary>
    public int Length { get; private set; }

    /// <summary>
    /// Gets the position of the room.
    /// </summary>
    public Vector3 Position { get; private set; }

    /// <summary>
    /// Generate a room with the given parameters.
    /// </summary>
    /// <param name="position">The position of the room.</param>
    /// <param name="width">The width of the room.</param>
    /// <param name="length">The length of the room.</param>
    public void GenerateRoom(Vector3 position, int width, int length)
    {
        Position = position;
        Width = width;
        Length = length;
        
        GenerateDoors();
        GenerateWalls();
        GenerateFloor();
    }

    private void GenerateDoors()
    {
        doors = new List<GameObject>();

        // Determine how many doors the room will have.
        // At least 1 and at most 4.
        System.Random rnd = new System.Random();
        int doorCount = rnd.Next(1, 5);
    }

    private void GenerateWalls()
    {
        northWall = new List<GameObject>();
        eastWall = new List<GameObject>();

        // Generate north walls.
        for (int i = 0; i < Width; ++i)
        {
            GameObject obj = Instantiate(wall, new Vector3(Position.x + i, Position.y + 0.5f, Position.z + Length - 0.5f), Quaternion.identity) as GameObject;
            northWall.Add(obj);
        }

        // Generate east walls.
        for (int i = 0; i < Length; ++i)
        {
            GameObject obj = Instantiate(wall, new Vector3(Position.x + Width - 0.5f, Position.y + 0.5f, Position.z + i), Quaternion.Euler(0, 90, 0)) as GameObject;
            eastWall.Add(obj);
        }
    }

    private void GenerateFloor()
    {
        // Generate each floor tile.
        floorArea = new GameObject[Width, Length];

        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Length; ++j)
            {
                floorArea[i, j] = Instantiate(floorTile, new Vector3(Position.x + i, Position.y, Position.z + j), new Quaternion(1, 0, 0, 1)) as GameObject;
            }
        }
    }
}
