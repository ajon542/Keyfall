using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ILevelGenerator
{
    List<string>[,] GenerateLevel(int width, int length);
}
