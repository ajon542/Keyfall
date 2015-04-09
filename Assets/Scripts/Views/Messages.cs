
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

public class FloorPlanMsg : BaseMsg
{
    public DungeonLayout[,] Floorplan { get; set; }

    public int Width { get; set; }
    public int Length { get; set; }
}
