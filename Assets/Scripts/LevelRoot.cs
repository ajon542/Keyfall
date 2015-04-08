using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The root of a level contains the rooms and connecting tunnels.
/// </summary>
public class LevelRoot : MonoBehaviour
{
    /// <summary>
    /// Minimum room width or length.
    /// </summary>
    public int minRoomDimensions = 3;

    /// <summary>
    /// Maximum room width or length. Ideally, this should
    /// be less than the gridSize.
    /// </summary>
    public int maxRoomDimensions = 15;

    /// <summary>
    /// Minimum gap between the rooms.
    /// </summary>
    public int minRoomGap = 5;

    /// <summary>
    /// The level dimensions.
    /// </summary>
    public int levelDimensions = 5;

    /// <summary>
    /// Split the level into a grid. A room can be placed
    /// in each grid location to ensure there is not room
    /// overlap.
    /// </summary>
    private int gridSize;

    /// <summary>
    /// The room prefab.
    /// </summary>
    public Room room;

    /// <summary>
    /// List of the generated rooms.
    /// </summary>
    private Room[,] rooms;

    /// <summary>
    /// Level startup.
    /// </summary>
    private void Start()
    {
        gridSize = maxRoomDimensions + minRoomGap;
        GenerateRooms();
    }

    /// <summary>
    /// Generate the rooms in this level.
    /// </summary>
    private void GenerateRooms()
    {
        System.Random rnd = new System.Random();
        rooms = new Room[levelDimensions, levelDimensions];

        // Generate a room in each grid location.
        for (int gridLocationX = 0; gridLocationX < levelDimensions; gridLocationX++)
        {
            for (int gridLocationZ = 0; gridLocationZ < levelDimensions; gridLocationZ++)
            {
                // Generate a room width and length.
                int width = rnd.Next(minRoomDimensions, maxRoomDimensions);
                int length = rnd.Next(minRoomDimensions, maxRoomDimensions);

                // Position the room within the grid location.
                int positionX = rnd.Next(0, gridSize - width) + gridLocationX * gridSize;
                int positionZ = rnd.Next(0, gridSize - length) + gridLocationZ * gridSize;

                // Create the room.
                Room room = CreateRoom(new Vector3(positionX, transform.position.y, positionZ), width, length);
                rooms[gridLocationX, gridLocationZ] = room;
            }
        }
    }

    /// <summary>
    /// Helper method to create single room.
    /// </summary>
    /// <param name="position">The position of the room.</param>
    /// <param name="width">The width of the room.</param>
    /// <param name="length">The length of the room.</param>
    /// <returns>The instantiated room.</returns>
    private Room CreateRoom(Vector3 position, int width, int length)
    {
        Room obj = Instantiate(room, new Vector3(0, 0, 0), Quaternion.identity) as Room;
        obj.GenerateRoom(position, width, length);
        return obj;
    }
}
