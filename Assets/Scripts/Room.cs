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
    /// Generate a room with the given parameters.
    /// </summary>
    /// <param name="position">The position of the room.</param>
    /// <param name="width">The width of the room.</param>
    /// <param name="length">The length of the room.</param>
    public void GenerateRoom(Vector3 position, int width, int length)
    {
        floorArea = new GameObject[width, length];

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
