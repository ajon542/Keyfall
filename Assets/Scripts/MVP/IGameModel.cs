using UnityEngine;
using System.Collections;

public abstract class IGameModel : MonoBehaviour
{
    private Presenter presenter;

    public void Initialize(Presenter presenter)
    {
        this.presenter = presenter;
    }

    public virtual void UpdateModel()
    {
    }
}