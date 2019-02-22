using System;
using UnityEngine;

public class ManhattanHeuristic : IPathHeuristic
{
    public int Heuristic(Vector3Int start, Vector3Int goal)
    {
        int dx = Math.Abs(start.x - goal.x);
        int dy = Math.Abs(start.y - goal.y);
        return dx + dy;
    }
}