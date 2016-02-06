using UnityEngine;
using System.Collections;

public class PlayerView : IGameView
{
    private Vector3 currentPosition;

    [RecvMsgMethod]
    public void HandlePlayerPosition(PlayerPosition msg)
    {
        Debug.Log("HandlePlayerPosition");
        currentPosition = msg.Position;

        gameObject.transform.position = currentPosition;
    }

    public override void UpdateView()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            // TODO: Need a way to update the model with the information.
            currentPosition = new Vector3(currentPosition.x, 0, currentPosition.z + 1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            // TODO: Need a way to update the model with the information.
            currentPosition = new Vector3(currentPosition.x, 0, currentPosition.z - 1);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            // TODO: Need a way to update the model with the information.
            currentPosition = new Vector3(currentPosition.x - 1, 0, currentPosition.z);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            // TODO: Need a way to update the model with the information.
            currentPosition = new Vector3(currentPosition.x + 1, 0, currentPosition.z);
        }

        gameObject.transform.position = currentPosition;

        base.UpdateView();
    }
}
