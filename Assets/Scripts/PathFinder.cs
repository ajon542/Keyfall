using System;
using System.Collections.Generic;

/// <summary>
/// Implements the A* path finding algorithm.
/// </summary>
public class PathFinder
{
    /// <summary>
    /// The graph of locations.
    /// </summary>
    private IWeightedGraph<Location> graph;

    /// <summary>
    /// Maps the given location to the previous location.
    /// </summary>
    private Dictionary<Location, Location> cameFrom = new Dictionary<Location, Location>();

    /// <summary>
    /// Maps the cost so far to a given location.
    /// </summary>
    private Dictionary<Location, int> costSoFar = new Dictionary<Location, int>();

    /// <summary>
    /// Defines a Manhatten heuristic so the path ends up being an L-shape.
    /// </summary>
    /// <param name="start">The starting location.</param>
    /// <param name="goal">The target location.</param>
    /// <returns>A value indicating the cost of this location.</returns>
    private static int Heuristic(Location start, Location goal)
    {
        int dx = Math.Abs(start.x - goal.x);
        int dy = Math.Abs(start.y - goal.y);
        return dx + dy;
    }

    //private static int Heuristic(Location a, Location b)
    //{
    //    int dx = Math.Abs(a.x - b.x);
    //    int dy = Math.Abs(a.y - b.y);
    //    return Math.Max(dx, dy);
    //}

    /// <summary>
    /// Creates a new instance of the <see cref="PathFinder"/> class.
    /// </summary>
    /// <param name="graph"></param>
    public PathFinder(IWeightedGraph<Location> graph)
    {
        this.graph = graph;
    }

    /// <summary>
    /// Gets a path between the start and goal locations.
    /// </summary>
    /// <param name="start">The starting location.</param>
    /// <param name="goal">The target location.</param>
    /// <returns>The path if one exists; null otherwise.</returns>
    public List<Location> GetPath(Location start, Location goal)
    {
        // Initialize the start location.
        var frontier = new PriorityQueue<Location>();
        frontier.Enqueue(start, 0);

        cameFrom.Add(start, start);
        costSoFar.Add(start, 0);

        // Continue until there are no more locations to search.
        while (frontier.Count > 0)
        {
            // Get the next best location.
            var current = frontier.Dequeue();

            // Check if this location is the goal.
            if (current.Equals(goal))
            {
                break;
            }

            // Check each of the neighbouring locations.
            foreach (var neighbour in graph.Neighbours(current))
            {
                // Calculate the new cost of moving to the neighbouring location.
                int newCost = costSoFar[current] + graph.Cost(neighbour);

                // Make sure we haven't already visited that neighbour unless
                // the new cost is better by visiting that neighbour again.
                if (!costSoFar.ContainsKey(neighbour) || newCost < costSoFar[neighbour])
                {
                    // Add the neighbour with the cost.
                    costSoFar.Add(neighbour, newCost);

                    // Calculate and add the neighbour at the given priority.
                    int priority = newCost + Heuristic(neighbour, goal);
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

        // Construct the path.
        List<Location> path = new List<Location>();
        path.Add(goal);

        Location previous = cameFrom[goal];
        while ((previous.x != start.x) || (previous.y != start.y))
        {
            path.Add(previous);
            previous = cameFrom[previous];
        }

        path.Add(start);

        Clear();
        return path;
    }

    /// <summary>
    /// Clear the internal structures.
    /// </summary>
    private void Clear()
    {
        cameFrom.Clear();
        costSoFar.Clear();
    }
}