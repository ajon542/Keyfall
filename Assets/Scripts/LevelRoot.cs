using System.Collections.Generic;
using UnityEngine;

public class LevelRoot : MonoBehaviour
{
    /// <summary>
    /// Minimum room width or length.
    /// </summary>
    public int minRoomDimensions = 3;

    /// <summary>
    /// Maximum room width or length.
    /// </summary>
    public int maxRoomDimensions = 15;

    /// <summary>
    /// The level dimensions.
    /// </summary>
    public int levelDimensions = 100;

    /// <summary>
    /// Split the level dimensions into a grid to place each
    /// room in so the rooms do not overlap.
    /// </summary>
    public int gridSize = 20;

    /// <summary>
    /// The room prefab.
    /// </summary>
    public Room room;

    /// <summary>
    /// List of the generated rooms.
    /// </summary>
    private List<Room> rooms; 

    private void Start()
    {
        System.Random rnd = new System.Random();

        // Generate a room in each grid location.
        for (int gridLocationX = 0; gridLocationX < levelDimensions; gridLocationX += gridSize)
        {
            for (int gridLocationY = 0; gridLocationY < levelDimensions; gridLocationY += gridSize)
            {
                // Generate a room width and length.
                int width = rnd.Next(minRoomDimensions, maxRoomDimensions);
                int length = rnd.Next(minRoomDimensions, maxRoomDimensions);

                // Position the room within the grid location.
                int positionX = rnd.Next(0, gridSize - width) + gridLocationX;
                int positionZ = rnd.Next(0, gridSize - length) + gridLocationY;

                // Create the room.
                rooms = new List<Room>();
                rooms.Add(CreateRoom(new Vector2(positionX, positionZ), width, length));
            }
        }
    }

    private Room CreateRoom(Vector2 position, int width, int length)
    {
        Room obj = Instantiate(room, new Vector3(0, 0, 0), Quaternion.identity) as Room;
        obj.GenerateRoom(position, width, length);
        return obj;
    }
}
