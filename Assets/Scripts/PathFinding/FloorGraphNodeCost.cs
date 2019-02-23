using UnityEngine;
using UnityEngine.Tilemaps;

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
            return new NodeCost(1);
        return null;
    }
}