using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateRandomTile : MonoBehaviour
{
    public Tilemap _floorTilemap;
    public Tilemap _targetTilemap;
    public TileBase _tile;
    public int _count;
    private List<Vector3Int> _validTilePositions = new List<Vector3Int>();

    void Start()
    {
        foreach (var pos in _floorTilemap.cellBounds.allPositionsWithin)
        {
            if (_floorTilemap.HasTile(pos))
            {
                _validTilePositions.Add(pos);
            }
        }

        for (int i = 0; i < _count; ++i)
        {
            int randomIndex = Random.Range(0, _validTilePositions.Count);
            _targetTilemap.SetTile(_validTilePositions[randomIndex], _tile);
        }
    }
}