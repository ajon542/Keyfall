using System;
using UnityEngine;

public class DiagonalHeuristic : IPathHeuristic
{
    public int Heuristic(Vector3Int a, Vector3Int b)
    {
        int dx = Math.Abs(a.x - b.x);
        int dy = Math.Abs(a.y - b.y);
        return Math.Max(dx, dy);
    }
}