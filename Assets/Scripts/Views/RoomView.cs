using UnityEngine;
using System.Collections;

public class RoomView : IGameView
{
    [RecvMsgMethod]
    public void HandleSampleMsg(SampleMsg msg)
    {
    }

    public override void UpdateView()
    {
        PublishMsg(new RoomMsg());
    }
}
