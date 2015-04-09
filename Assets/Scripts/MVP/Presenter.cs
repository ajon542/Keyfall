using System.Linq;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The game presenter is the abstraction between the view and model.
/// </summary>
public class Presenter : MonoBehaviour
{
    /// <summary>
    /// The game model to be hooked up via the unity editor.
    /// </summary>
    public IGameModel gameModel;

    /// <summary>
    /// The list of game views in the scene.
    /// </summary>
    private List<IGameView> gameViews = new List<IGameView>();

    /// <summary>
    /// Find all objects of type IGameView and keep a reference to them.
    /// </summary>
    private void Awake()
    {
        gameViews = FindObjectsOfType<IGameView>().ToList();
    }

    /// <summary>
    /// Initialize all the game views and the game model.
    /// </summary>
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

    /// <summary>
    /// Update all the game views and the game model.
    /// </summary>
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

    /// <summary>
    /// Publish a message to all the game views.
    /// </summary>
    /// <param name="msg">The message to publish.</param>
    public void PublishMsg(BaseMsg msg)
    {
        foreach (IGameView gameView in gameViews)
        {
            gameView.ReceiveMsg(msg);
        }
    }
}
