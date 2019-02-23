using UnityEngine;

public interface IPathHeuristic
{
    int Heuristic(Vector3Int start, Vector3Int goal);
}