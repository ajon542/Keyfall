using System.Collections.Generic;
using UnityEngine;

public class LevelRoot : MonoBehaviour
{
    public Room room;

    private List<Room> rooms; 

    private void Start()
    {
        rooms = new List<Room>();
        rooms.Add(CreateRoom(new Vector2(0, 0), 3, 7));
        rooms.Add(CreateRoom(new Vector2(10, 10), 3, 4));
    }

    private Room CreateRoom(Vector2 position, int width, int length)
    {
        Room obj = Instantiate(room, new Vector3(0, 0, 0), Quaternion.identity) as Room;
        obj.GenerateRoom(position, width, length);
        return obj;
    }
}
