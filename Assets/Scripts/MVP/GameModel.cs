using UnityEngine;
using System.Collections;

/// <summary>
/// Concrete implementation of the IGameModel abstract base class.
/// </summary>
public class GameModel : IGameModel
{
    public override void UpdateModel()
    {
        presenter.PublishMsg(new SampleMsg());
    }
}