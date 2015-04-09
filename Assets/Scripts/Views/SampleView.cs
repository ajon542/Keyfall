using UnityEngine;
using System.Collections;

public class SampleView : IGameView
{
    [RecvMsgMethod]
    public void HandleRoomMsg(RoomMsg msg)
    {
    }
}
