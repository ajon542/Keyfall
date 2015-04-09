using UnityEngine;
using System.Collections;

public enum Direction
{
    North,
    South,
    East,
    West
}

public enum DungeonLayout
{
    Unknown,
    Floor,
    NorthWall,
    SouthWall,
    EastWall,
    WestWall,
    Door,
    Stairs
}
