using UnityEngine;
using System.Collections;

public class RoomView : IGameView
{
    [RecvMsgMethod]
    public void HandleSampleMsg(SampleMsg msg)
    {
        Debug.Log("Recevied SampleMsg");
    }

    public override void UpdateView()
    {
        PublishMsg(new RoomMsg());
    }
}
