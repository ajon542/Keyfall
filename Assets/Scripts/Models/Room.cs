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
    /// Gets the x-position of the room.
    /// </summary>
    public int PositionX { get; private set; }

    /// <summary>
    /// Gets the z-position of the room.
    /// </summary>
    public int PositionZ { get; private set; }

    /// <summary>
    /// Gets the position of the room.
    /// </summary>
    public Vector3 Position { get; private set; }


    public Room(int positionX, int positionZ, int width, int length)
    {
        PositionX = positionX;
        PositionZ = positionZ;
        Width = width;
        Length = length;
    }

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
    }
}
