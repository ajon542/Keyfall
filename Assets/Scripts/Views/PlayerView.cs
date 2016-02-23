using UnityEngine;
using System.Collections;

public class PlayerView : IGameView
{
    private Vector3 currentPosition = new Vector3(0.5f, 0.5f, 0.5f);

    [RecvMsgMethod]
    public void HandlePlayerPosition(PlayerPosition msg)
    {
        Debug.Log("HandlePlayerPosition");
        currentPosition = msg.Position;

        gameObject.transform.position = currentPosition;
    }

    public override void UpdateView()
    {
        base.UpdateView();
    }
}
