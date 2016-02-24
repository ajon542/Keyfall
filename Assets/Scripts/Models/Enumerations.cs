using UnityEngine;
using System.Collections;

public enum Direction
{
    North,
    South,
    East,
    West
}

public enum StructuralItems
{
    Unknown,
    FloorConcrete,
    FloorRock,
    FloorClay,
    FloorMud,
    FloorSlime,
    FloorWood,

    // There is only north and east walls as the others will not be visible.
    WallConcreteNorth,
    WallRockNorth,
    WallSlimeNorth,
    WallConcreteEast,
    WallRockEast,
    WallSlimeEast,

    DoorNorth,
    DoorSouth,
    DoorEast,
    DoorWest,

    StairsNorth,
    StairsSouth,
    StairsEast,
    StairsWest,
}