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
    public List<string>[,] DungeonLayout { get; set; }
}

public class GenerateTown : BaseMsg
{
    public List<string>[,] TownLayout { get; set; }
}

public class DestroyDungeon : BaseMsg
{

}

public class PlayerPosition : BaseMsg
{
    public Location Position { get; set; }
}
