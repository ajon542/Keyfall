using System.Collections.Generic;
using UnityEngine;

public interface IGraph
{
    List<Vector3Int> Neighbors(Vector3Int node);
}