using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    private int width;
    private int length;
    private DungeonLayout[,] grid;

    private Vector2 start;
    private Vector2 goal;

    private int[,] direction = { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };

    // TODO: Remove DungeonLayout dependency, we want to make this class generic.
    // First we want to see if it works.
    public PathFinder(DungeonLayout[,] grid)
    {
        if (grid == null)
        {
            throw new ArgumentNullException("grid");
        }

        this.grid = grid;
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

    public void GeneratePath(Vector2 start, Vector2 end, List<Vector2> result)
    {
        goal = end;

        Debug.Log(ManhattanHeuristic(start));
        Debug.Log(ManhattanHeuristic(new Vector2(start.x, start.y + 1)));
        Debug.Log(ManhattanHeuristic(new Vector2(start.x, start.y - 1)));

        Debug.Log(ManhattanHeuristic(new Vector2(start.x + 1, start.y)));
        Debug.Log(ManhattanHeuristic(new Vector2(start.x - 1, start.y)));
    }

    private int ManhattanHeuristic(Vector2 node)
    {
        int dx = (int)Math.Abs(node.x - goal.x);
        int dy = (int)Math.Abs(node.y - goal.y);
        int scale = 1;

        return scale * (dx + dy);
    }
}