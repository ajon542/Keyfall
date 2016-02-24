using UnityEngine;
using System.Collections;

public class PlayerView : IGameView
{
    [RecvMsgMethod]
    public void HandlePlayerPosition(PlayerPosition msg)
    {
        gameObject.transform.position = new Vector3((float)msg.Position.x, 0.5f, (float)msg.Position.y);
    }

    public override void UpdateView()
    {
        base.UpdateView();
    }
}
