using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeCost
{
    public int Cost { get; set; }
}

public interface IGraphNodeCost
{
    NodeCost Cost(Vector3Int node);
}

public class FloorGraphNodeCost : IGraphNodeCost
{
    private Tilemap _tilemap;
    
    public FloorGraphNodeCost(Tilemap tilemap)
    {
        _tilemap = tilemap;
    }
    
    public NodeCost Cost(Vector3Int node)
    {
        if (_tilemap.HasTile(node))
            return new NodeCost {Cost = 1};
        return null;
    }
}

public class ObstacleGraphNodeCost : IGraphNodeCost
{
    private Tilemap _tilemap;
    
    public ObstacleGraphNodeCost(Tilemap tilemap)
    {
        _tilemap = tilemap;
    }
    
    public NodeCost Cost(Vector3Int node)
    {
        if (_tilemap.HasTile(node))
            return new NodeCost {Cost = 100};
        return null;
    }
}

public class NodeCostChainOfResponsibilities : IGraphNodeCost
{
    private List<IGraphNodeCost> _graphNodeCosts;
    public NodeCostChainOfResponsibilities(List<IGraphNodeCost> graphNodeCosts)
    {
        _graphNodeCosts = graphNodeCosts;
    }
    
    public NodeCost Cost(Vector3Int node)
    {
        foreach (var graphNodeCost in _graphNodeCosts)
        {
            var cost = graphNodeCost.Cost(node);
            if (cost != null)
                return cost;   
        }
        throw new Exception("could not find cost for tile");
    }
}

public interface IGraph
{
    List<Vector3Int> Neighbors(Vector3Int node);
}

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