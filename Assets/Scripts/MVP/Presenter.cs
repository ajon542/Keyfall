using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseMsg
{ 
}

public class SampleMsg : BaseMsg
{
}

public class RoomMsg : BaseMsg
{
}

public class Presenter : MonoBehaviour
{
    private IGameModel gameModel;
    private List<IGameView> gameViews = new List<IGameView>();

    private void Awake()
    {
        gameViews = FindObjectsOfType<IGameView>().ToList();
    }

    private void Start()
    {
        foreach (IGameView gameView in gameViews)
        {
            gameView.Initialize(this);
        }

        if (gameModel != null)
        {
            gameModel.Initialize(this);
        }
    }

    private void Update()
    {
        foreach (IGameView gameView in gameViews)
        {
            gameView.UpdateView();
        }

        if (gameModel != null)
        {
            gameModel.UpdateModel();
        }
    }

    public void PublishMsg(BaseMsg msg)
    {
        foreach (IGameView gameView in gameViews)
        {
            gameView.ReceiveMsg(msg);
        }
    }
}
