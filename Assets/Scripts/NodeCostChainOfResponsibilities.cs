using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
