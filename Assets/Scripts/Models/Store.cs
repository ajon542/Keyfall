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
    public List<Item> StoreItems { get; private set; }
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

        StoreItems = new List<Item>();
        StoreItems.Add(new Item { Name = "Sword", Value = 100, Description = "Longsword of the damned" });
        StoreItems.Add(new Item { Name = "Chainmain Armour", Value = 500, Description = "Armour of valor" });
        StoreItems.Add(new Item { Name = "Torch", Value = 5, Description = "Lighting the way" });
        StoreItems.Add(new Item { Name = "Boots", Value = 100, Description = "Hard leather boots" });
        StoreItems.Add(new Item { Name = "Sword", Value = 100, Description = "Longsword of the damned" });
        StoreItems.Add(new Item { Name = "Chainmain Armour", Value = 500, Description = "Armour of valor" });
        StoreItems.Add(new Item { Name = "Torch", Value = 5, Description = "Lighting the way" });
        StoreItems.Add(new Item { Name = "Boots", Value = 100, Description = "Hard leather boots" });
        StoreItems.Add(new Item { Name = "Sword", Value = 100, Description = "Longsword of the damned" });
        StoreItems.Add(new Item { Name = "Chainmain Armour", Value = 500, Description = "Armour of valor" });
        StoreItems.Add(new Item { Name = "Torch", Value = 5, Description = "Lighting the way" });
        StoreItems.Add(new Item { Name = "Boots", Value = 100, Description = "Hard leather boots" });
        StoreItems.Add(new Item { Name = "Sword", Value = 100, Description = "Longsword of the damned" });
        StoreItems.Add(new Item { Name = "Chainmain Armour", Value = 500, Description = "Armour of valor" });
        StoreItems.Add(new Item { Name = "Torch", Value = 5, Description = "Lighting the way" });
        StoreItems.Add(new Item { Name = "Boots", Value = 100, Description = "Hard leather boots" });
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
