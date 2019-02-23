using UnityEngine;
using UnityEngine.Tilemaps;

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
