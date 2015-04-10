using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A room contains the floor and walls.
/// </summary>
public class Room
{
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

        GenerateWalls();
    }

    private void GenerateWalls()
    {
        /*northWall = new List<GameObject>();
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
        }*/
    }
}
