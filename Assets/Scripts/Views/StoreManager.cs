using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoreManager
{
    private Store store;

    public void Initialize(Presenter presenter)
    {
        store = new Store(presenter);
        store.InitializeView();
    }
}
