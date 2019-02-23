
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGraph : IGraph
{
    private Tilemap _tilemap;
    
    public TilemapGraph(Tilemap tilemap)
    {
        _tilemap = tilemap;
    }
    
    public List<Vector3Int> Neighbors(Vector3Int node)
    {
        var neighborPositions = new List<Vector3Int>();
        var neighborPosition = new Vector3Int(node.x, node.y + 1, node.z);
        if (_tilemap.HasTile(neighborPosition))
            neighborPositions.Add(neighborPosition);

        neighborPosition = new Vector3Int(node.x, node.y - 1, node.z);
        if (_tilemap.HasTile(neighborPosition))
            neighborPositions.Add(neighborPosition);
        
        neighborPosition = new Vector3Int(node.x + 1, node.y, node.z);
        if (_tilemap.HasTile(neighborPosition))
            neighborPositions.Add(neighborPosition);
        
        neighborPosition = new Vector3Int(node.x - 1, node.y, node.z);
        if (_tilemap.HasTile(neighborPosition))
            neighborPositions.Add(neighborPosition);
        
        return neighborPositions;
    }
}