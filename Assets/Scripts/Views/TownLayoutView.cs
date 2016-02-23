using UnityEngine;
using System.Collections;

public class TownLayoutView : IGameView
{
    /// <summary>
    /// Gets the width of the room.
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// Gets the length of the room.
    /// </summary>
    public int Length { get; private set; }

    [RecvMsgMethod]
    public void HandleGenerateTownMsg(GenerateTown msg)
    {
        Debug.Log("Received GenerateTownMsg");

        // Keep track of the floor plan properties.
        Width = msg.Width;
        Length = msg.Length;
    }
}
