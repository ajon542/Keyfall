using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class represents the store model.
/// </summary>
/// <remarks>
/// All the current inventory and transactions are handled by this class.
/// </remarks>
public class Store
{
    public string StoreName { get; private set; }
    public int StoreFunds { get; private set; }
    public Dictionary<string, int> StoreItems { get; private set; }
    private Presenter presenter;

    public Store(Presenter presenter)
    {
        if (presenter == null)
        {
            Debug.LogError("Presenter is null");
            return;
        }

        this.presenter = presenter;

        StoreName = "Armourer";
        StoreFunds = 2345;

        StoreItems = new Dictionary<string, int>();
        StoreItems.Add("Sword", 100);
        StoreItems.Add("Chainmail Armour", 100);
        StoreItems.Add("Torch", 100);
        StoreItems.Add("Axe", 200);
    }

    /// <summary>
    /// Initialize the store view.
    /// </summary>
    /// <remarks>
    /// This method is inteded to be called each time the store is entered.
    /// The StoreInitializeMsg should be sent to the view.
    /// </remarks>
    public void InitializeView()
    {
        StoreInitializeMsg msg = new StoreInitializeMsg();
        msg.StoreName = StoreName;
        msg.StoreFunds = StoreFunds;
        msg.StoreItems = StoreItems;

        presenter.PublishMsg(msg);
    }

    public void Buy(string item)
    {
    }

    public void Sell(string item, int price)
    {
    }
}
