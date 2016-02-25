using UnityEngine;
using System.Collections.Generic;

public class TownLayoutView : IGameView
{
    public List<GameObject> prefabs;
    Dictionary<string, GameObject> prefabMap;

    private GameObject RoomsGrid { get; set; }

    public int Width { get; private set; }
    public int Length { get; private set; }

    [RecvMsgMethod]
    public void HandleGenerateTownMsg(GenerateTown msg)
    {
        Debug.Log("Received GenerateTownMsg");

        prefabMap = new Dictionary<string, GameObject>();
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
                // TODO: Handle the list of tiles.
                string tileName = msg.TownLayout[i, j][0];
                AddTile(tileName, RoomsGrid.transform, i, j);
            }
        }
    }

    private void AddTile(string tileName, Transform parent, int x, int y)
    {
        if (prefabMap.ContainsKey(tileName))
        {
            GameObject obj =
                Instantiate(prefabMap[tileName], new Vector3(x, 0, y), new Quaternion(1, 0, 0, 1)) as GameObject;
            obj.name = tileName;
            obj.transform.parent = parent;
        }
        else
        {
            Debug.LogWarning("Could not find [" + tileName + "] prefab");
        }
    }
}
