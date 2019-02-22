using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    private IWeightedGraph _graph;
    private IPathHeuristic _pathHeuristic;
    private Dictionary<Vector3Int, Vector3Int> _cameFrom = new Dictionary<Vector3Int, Vector3Int>();
    private Dictionary<Vector3Int, int> _costSoFar = new Dictionary<Vector3Int, int>();

    public PathFinder(IWeightedGraph graph, IPathHeuristic pathHeuristic)
    {
        _graph = graph;
        _pathHeuristic = pathHeuristic;
    }

    public List<Vector3Int> GetPath(Vector3Int start, Vector3Int goal)
    {
        var frontier = new PriorityQueue<Vector3Int>();
        frontier.Enqueue(start, 0);

        _cameFrom.Add(start, start);
        _costSoFar.Add(start, 0);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            if (current.Equals(goal))
                break;

            var neighbors = _graph.Neighbors(current);
            foreach (var neighbour in neighbors)
            {
                // Calculate the new cost of moving to the neighbouring location.
                int newCost = _costSoFar[current] + _graph.Cost(neighbour);

                // Make sure we haven't already visited that neighbour unless
                // the new cost is better by visiting that neighbour again.
                if (!_costSoFar.ContainsKey(neighbour) || newCost < _costSoFar[neighbour])
                {
                    // Add the neighbour with the cost.
                    _costSoFar[neighbour] = newCost;

                    // Calculate and add the neighbour at the given priority.
                    int priority = newCost + _pathHeuristic.Heuristic(neighbour, goal);
                    frontier.Enqueue(neighbour, priority);

                    // Update how we reached this neighbour for path construction.
                    _cameFrom[neighbour] = current;
                }
            }
        }

        // Check if the goal was reached.
        if (!_cameFrom.ContainsKey(goal))
        {
            // Could not find a path to goal.
            Clear();
            return null;
        }

        var path = new List<Vector3Int>();
        path.Add(goal);

        var previous = _cameFrom[goal];
        while ((previous.x != start.x) || (previous.y != start.y))
        {
            path.Add(previous);
            previous = _cameFrom[previous];
        }

        path.Add(start);
        path.Reverse();

        Clear();
        return path;
    }

    private void Clear()
    {
        _cameFrom.Clear();
        _costSoFar.Clear();
    }
}