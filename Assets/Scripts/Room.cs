using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject floorTile;

    private GameObject[,] floorArea;

    public int Width { get; private set; }
    public int Length { get; private set; }

    public void GenerateRoom(Vector3 position, int width, int length)
    {
        Width = width;
        Length = length;

        floorArea = new GameObject[width, length];

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < length; ++j)
            {
                floorArea[i, j] = Instantiate(floorTile, new Vector3(position.x + i, position.y, position.z + j), new Quaternion(1, 0, 0, 1)) as GameObject;
            }
        }
    }
}
