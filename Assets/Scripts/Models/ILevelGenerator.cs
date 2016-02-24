using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ILevelGenerator
{
    List<TownLayout>[,] GenerateLevel(int width, int length);
}
