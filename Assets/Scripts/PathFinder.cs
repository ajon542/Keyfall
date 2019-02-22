using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public interface IPathHeuristic
{
    int Heuristic(Vector3Int start, Vector3Int goal);
}

public interface IWeightedGraph
{
    int Cost(Vector3Int node);
    List<Vector3Int> Neighbors(Vector3Int node);
}

public class ManhattanHeuristic : IPathHeuristic
{
    public int Heuristic(Vector3Int start, Vector3Int goal)
    {
        int dx = Math.Abs(start.x - goal.x);
        int dy = Math.Abs(start.y - goal.y);
        return dx + dy;
    }
}

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

public class PathFinder
{
    private IWeightedGraph _graph;
    private IPathHeuristic _pathHeuristic;

    private Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();

    private Dictionary<Vector3Int, int> costSoFar = new Dictionary<Vector3Int, int>();

    public PathFinder(IWeightedGraph graph, IPathHeuristic pathHeuristic)
    {
        _graph = graph;
        _pathHeuristic = pathHeuristic;
    }

    public List<Vector3Int> GetPath(Vector3Int start, Vector3Int goal)
    {
        var frontier = new PriorityQueue<Vector3Int>();
        frontier.Enqueue(start, 0);

        cameFrom.Add(start, start);
        costSoFar.Add(start, 0);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            if (current.Equals(goal))
                break;

            var neighbors = _graph.Neighbors(current);
            foreach (var neighbour in neighbors)
            {
                // Calculate the new cost of moving to the neighbouring location.
                int newCost = costSoFar[current] + _graph.Cost(neighbour);

                // Make sure we haven't already visited that neighbour unless
                // the new cost is better by visiting that neighbour again.
                if (!costSoFar.ContainsKey(neighbour) || newCost < costSoFar[neighbour])
                {
                    // Add the neighbour with the cost.
                    costSoFar.Add(neighbour, newCost);

                    // Calculate and add the neighbour at the given priority.
                    int priority = newCost + _pathHeuristic.Heuristic(neighbour, goal);
                    frontier.Enqueue(neighbour, priority);

                    // Update how we reached this neighbour for path construction.
                    cameFrom.Add(neighbour, current);
                }
            }
        }

        // Check if the goal was reached.
        if (!cameFrom.ContainsKey(goal))
        {
            // Could not find a path to goal.
            Clear();
            return null;
        }

        List<Vector3Int> path = new List<Vector3Int>();
        path.Add(goal);

        Vector3Int previous = cameFrom[goal];
        while ((previous.x != start.x) || (previous.y != start.y))
        {
            path.Add(previous);
            previous = cameFrom[previous];
        }

        path.Add(start);
        path.Reverse();

        Clear();
        return path;
    }

    private void Clear()
    {
        cameFrom.Clear();
        costSoFar.Clear();
    }
}