using UnityEngine;
using System.Collections.Generic;

public class TownLayoutView : IGameView
{
    public List<GameObject> prefabs;

    private GameObject RoomsGrid { get; set; }

    public int Width { get; private set; }
    public int Length { get; private set; }

    [RecvMsgMethod]
    public void HandleGenerateTownMsg(GenerateTown msg)
    {
        Debug.Log("Received GenerateTownMsg");

        Dictionary<string, GameObject> prefabMap = new Dictionary<string, GameObject>();
        foreach(GameObject go in prefabs)
        {
            prefabMap[go.name] = go;
        }

        // Keep track of the floor plan properties.
        Width = msg.TownLayout.GetLength(0);
        Length = msg.TownLayout.GetLength(1);

        // Create a root game object to hold all the rooms.
        RoomsGrid = new GameObject { name = "RoomsGrid" };

        for (int i = 0; i < Width; ++i)
        {
            for (int j = 0; j < Length; ++j)
            {
                if (msg.TownLayout[i, j][0] == "Floor")
                {
                    if(prefabMap.ContainsKey("Floor"))
                    {
                        GameObject obj =
                            Instantiate(prefabMap["Floor"], new Vector3(i, 0, j), new Quaternion(1, 0, 0, 1)) as GameObject;
                        obj.name = "FloorTile";
                        obj.transform.parent = RoomsGrid.transform;
                    }
                    else
                    {
                        Debug.LogWarning("Could not find [Floor] prefab");
                    }
                }
            }
        }
    }
}
