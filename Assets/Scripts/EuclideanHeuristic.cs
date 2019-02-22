using System;
using UnityEngine;

public class EuclideanHeuristic : IPathHeuristic
{
    public int Heuristic(Vector3Int start, Vector3Int goal)
    {
        int D = 5;

        int dx = Math.Abs(start.x - goal.x);
        int dy = Math.Abs(start.y - goal.y);
        return D*(int)Math.Sqrt(dx*dx + dy*dy);
    }
}