using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapWeightedGraph : IWeightedGraph
{
    private Tilemap _tilemap;
    
    public TilemapWeightedGraph(Tilemap tilemap)
    {
        _tilemap = tilemap;
    }
    
    public int Cost(Vector3Int node)
    {
        if (_tilemap.HasTile(node))
            return 1;
        
        return Int32.MaxValue;
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