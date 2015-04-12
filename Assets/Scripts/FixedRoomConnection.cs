using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// TODO: In order to connect two rooms we could implement an A* search algorithm.
// The A* search algorithm could include a heuristic to make sure the path does
// not change direction too frequently. However, for simplicity we will go with
// fixed room connections first. If the fixed room connection is not sufficient,
// for example if there is another room blocking the path we can then employ an A* search.
// There are many techniques to generate the levels, this is just an exploration
// to see what levels can be generated using this simple method.

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

public class FixedRoomConnection
{
    public static void Generate(RoomConnection type, Vector2 start, Vector2 end, List<Vector2> result)
    {
        if(type == RoomConnection.NorthSouth)
        {
            GenerateNorthSouth(start, end, result);
        }
        else if (type == RoomConnection.EastWest)
        {
            GenerateEastWest(start, end, result);
        }
    }

    private static void GenerateNorthSouth(Vector2 start, Vector2 end, List<Vector2> result)
    {
        int x = (int)start.x;
        int y = (int)start.y;
        int endY = (int)end.y;

        y++;
        while(y < endY)
        {
            result.Add(new Vector2(x, y));
            y++;
        }
    }

    private static void GenerateEastWest(Vector2 start, Vector2 end, List<Vector2> result)
    {
        int x = (int)start.x;
        int y = (int)start.y;
        int endX = (int)end.x;

        x++;
        while (x < endX)
        {
            result.Add(new Vector2(x, y));
            x++;
        }
    }
}

