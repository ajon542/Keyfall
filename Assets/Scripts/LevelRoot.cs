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
    public int maxRoomDimensions = 10;

    /// <summary>
    /// The level dimensions.
    /// </summary>
    public int levelDimensions = 100;

    /// <summary>
    /// The number of rooms in this level.
    /// </summary>
    public int numberOfRooms = 10;

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

        for (int roomCount = 0; roomCount < numberOfRooms; ++roomCount)
        {
            int width = rnd.Next(minRoomDimensions, maxRoomDimensions);
            int length = rnd.Next(minRoomDimensions, maxRoomDimensions);

            int positionX = rnd.Next(levelDimensions);
            int positionZ = rnd.Next(levelDimensions);

            rooms = new List<Room>();
            rooms.Add(CreateRoom(new Vector2(positionX, positionZ), width, length));
        }
    }

    private Room CreateRoom(Vector2 position, int width, int length)
    {
        Room obj = Instantiate(room, new Vector3(0, 0, 0), Quaternion.identity) as Room;
        obj.GenerateRoom(position, width, length);
        return obj;
    }
}
