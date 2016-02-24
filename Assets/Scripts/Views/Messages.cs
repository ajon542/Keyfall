using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The base class for all messages.
/// </summary>
public class BaseMsg
{
}

public class SampleMsg : BaseMsg
{
}

public class RoomMsg : BaseMsg
{
}

public class GenerateDungeon : BaseMsg
{
    public List<TownLayout>[,] DungeonLayout { get; set; }

    public int Width { get; set; }
    public int Length { get; set; }
}

public class GenerateTown : BaseMsg
{
    public List<TownLayout>[,] TownLayout { get; set; }

    public int Width { get; set; }
    public int Length { get; set; }
}

public class DestroyDungeon : BaseMsg
{

}

public class PlayerPosition : BaseMsg
{
    public Location Position { get; set; }
}
