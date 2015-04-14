using System;
using System.Collections.Generic;
using UnityEngine;

public class Location
{
    public readonly int x, y;

    public Location(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class PathFinder
{
    private int width;
    private int length;

    private int[,] grid;

    private Location start;
    private Location goal;

    private List<Location> direction = new List<Location>{ new Location(0, -1), new Location(1, 0), new Location(0, 1), new Location(-1, 0) };

    // TODO: Remove DungeonLayout dependency, we want to make this class generic.
    // First we want to see if it works.
    public PathFinder(DungeonLayout[,] grid, int width, int length)
    {
        if (grid == null)
        {
            throw new ArgumentNullException("grid");
        }

        this.grid = new int[width, length];
        this.width = width;
        this.length = length;
    }

    // TODO: Need a priority queue.

    /*
        OPEN = priority queue containing START
        CLOSED = empty set
     
        while lowest rank in OPEN is not the GOAL:
     
            current = remove lowest rank item from OPEN
            add current to CLOSED
     
            for neighbours of current:
                cost = g(current) + movementcost(current, neighbour)
     
                if neighbour in OPEN and cost less than g(neighbour):
                    remove neighbour from OPEN, because new path is better
     
                if neighbour in CLOSED and cost less than g(neighbour): ** (this shouldn't happen)
                    remove neighbour from CLOSED
     
                if neighbour not in OPEN and neighbour not in CLOSED:
                    set g(neighbour) to cost
                    add neighbour to OPEN
                    set priority queue rank to g(neighbour) + h(neighbour)
                    set neighbour's parent to current

        reconstruct reverse path from goal to start
        by following parent pointers
     */

    public void GeneratePath(Location start, Location end, List<Location> result)
    {
        goal = end;

        PriorityQueue<Location> open = new PriorityQueue<Location>();
        List<Location> closed = new List<Location>();

        Dictionary<Location, Location> cameFrom = new Dictionary<Location, Location>();
        Dictionary<Location, int> costSoFar = new Dictionary<Location, int>();

        open.Enqueue(start, 0);
        cameFrom[start] = start;
        costSoFar[start] = 0;

        Location current;
        while(open.Count > 0)
        {
            current = open.Dequeue();
            if(current == goal)
            {
                break;
            }

            closed.Add(current);

            List<Location> neighbours = GetNeighbours(current);

            foreach (Location neighbour in neighbours)
            {
                int cost = costSoFar[current] + MovementCost(current, neighbour);

                if(!costSoFar.ContainsKey(neighbour) || cost < costSoFar[neighbour])
                {
                    costSoFar[neighbour] = cost;
                    int priority = cost + ManhattanHeuristic(neighbour);
                    open.Enqueue(neighbour, priority);
                    cameFrom[neighbour] = current;
                }
            }

        }

        //Debug.Log(ManhattanHeuristic(start));
        //Debug.Log(ManhattanHeuristic(new Vector2(start.x, start.y + 1)));
        //Debug.Log(ManhattanHeuristic(new Vector2(start.x, start.y - 1)));

        //Debug.Log(ManhattanHeuristic(new Vector2(start.x + 1, start.y)));
        //Debug.Log(ManhattanHeuristic(new Vector2(start.x - 1, start.y)));
    }

    private List<Location> GetNeighbours(Location v)
    {
        List<Location> neighbours = new List<Location>();

        foreach(Location l in direction)
        {
            Location neighbour = new Location(l.x + v.x, l.y + v.y);
            if (InBounds(neighbour) && Passable(neighbour))
            {
                neighbours.Add(neighbour);
            }
        }

        return neighbours;
    }

    private bool InBounds(Location v)
    {
        if (v.x < 0 || v.x >= width || v.y < 0 || v.y >= length)
        {
            return false;
        }
        return true;
    }

    private bool Passable(Location v)
    {
        return true;
    }

    private int MovementCost(Location current, Location neighbour)
    {
        return 1;
    }

    private int g(Location v)
    {
        return grid[v.x, v.y];
    }

    private int ManhattanHeuristic(Location v)
    {
        int dx = (int)Math.Abs(v.x - goal.x);
        int dy = (int)Math.Abs(v.y - goal.y);
        int scale = 1;

        return scale * (dx + dy);
    }
}