using System.Collections.Generic;
using UnityEngine;

public interface IWeightedGraph
{
    int Cost(Vector3Int node);
    List<Vector3Int> Neighbors(Vector3Int node);
}