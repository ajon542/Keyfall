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

/// <summary>
/// Message to initialize a store with all the required details.
/// </summary>
public class StoreInitializeMsg : BaseMsg
{
    public string StoreName { get; set; }
    public int StoreFunds { get; set; }
    public Dictionary<string, int> StoreItems { get; set; }
}
