using UnityEngine;

/// <summary>
/// Abstract base class for a game model.
/// </summary>
public abstract class IGameModel : MonoBehaviour
{
    /// <summary>
    /// Reference to the presenter object.
    /// </summary>
    protected Presenter presenter;

    /// <summary>
    /// Initialize the game model.
    /// </summary>
    /// <param name="presenter">The game presenter.</param>
    public virtual void Initialize(Presenter presenter)
    {
        this.presenter = presenter;
    }

    /// <summary>
    /// Update the model.
    /// </summary>
    public virtual void UpdateModel()
    {
    }
}