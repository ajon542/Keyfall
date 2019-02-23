using UnityEngine;

public interface IGraphNodeCost
{
    NodeCost Cost(Vector3Int node);
}