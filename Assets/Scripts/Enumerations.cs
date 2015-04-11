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

/// <summary>
/// Type of connection between rooms.
/// </summary>
/// <remarks>
/// 
/// NorthSouth
/// [ ]
///  x
///  x
/// [ ]
/// 
/// NorthWest
///  x x [ ]
///  x
/// [ ]
/// 
/// EastSouth
///      [ ]
///       x
/// [ ] x x
/// 
/// EastWest
/// [ ] x x [ ]
/// 
/// EastNorth
/// [ ] x x
///       x
///      [ ]
///      
/// SouthWest
///      [ ]
///       x
/// [ ] x x
/// 
/// </remarks>
public enum RoomConnection
{
    NorthSouth,
    NorthWest,
    EastSouth,
    EastWest,
    EastNoth,
    SouthWest
}
