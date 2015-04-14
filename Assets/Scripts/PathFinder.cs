using System;
using System.Collections.Generic;

public class PathFinder
{
    public Dictionary<Location, Location> cameFrom = new Dictionary<Location, Location>();
    public Dictionary<Location, int> costSoFar = new Dictionary<Location, int>();

    public static int Heuristic(Location a, Location b)
    {
        return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }

    private Location start;
    private Location goal;

    public PathFinder(IWeightedGraph<Location> graph, Location start, Location goal)
    {
        this.start = start;
        this.goal = goal;

        var frontier = new PriorityQueue<Location>();
        frontier.Enqueue(start, 0);

        cameFrom.Add(start, start);
        costSoFar.Add(start, 0);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            if (current.Equals(goal))
            {
                break;
            }

            foreach (var next in graph.Neighbours(current))
            {
                int newCost = costSoFar[current] + graph.Cost(current, next);

                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    costSoFar.Add(next, newCost);
                    int priority = newCost + Heuristic(next, goal);
                    frontier.Enqueue(next, priority);
                    cameFrom.Add(next, current);
                }
            }
        }
    }

    public List<Location> GetPath()
    {
        if (!cameFrom.ContainsKey(goal))
        {
            // Could not find a path.
            return null;
        }

        List<Location> path = new List<Location>();
        path.Add(goal);

        Location previous = cameFrom[goal];
        while ((previous.x != start.x) || (previous.y != start.y))
        {
            path.Add(previous);
            previous = cameFrom[previous];
        }
        path.Add(start);
        return path;
    }
}