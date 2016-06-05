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

public class GenerateDungeonMsg : BaseMsg
{
    public List<string>[,] DungeonLayout { get; set; }
}

public class GenerateTownMsg : BaseMsg
{
    public List<string>[,] TownLayout { get; set; }
}

public class DestroyDungeonMsg : BaseMsg
{

}

public class PlayerPositionMsg : BaseMsg
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
    public List<Item> StoreItems { get; set; }
}
