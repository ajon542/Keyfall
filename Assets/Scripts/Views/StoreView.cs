using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Generic store view.
/// </summary>
/// <remarks>
/// There only needs to be a single store view where the data comes from multiple
/// store models. We enter a store, the store model send the appropriate data and
/// the store view is updated accordingly.
/// </remarks>
public class StoreView : IGameView
{
    /// <summary>
    /// The store name text object must be present on the canvas object.
    /// </summary>
    [SerializeField]
    private Text storeNameText;

    /// <summary>
    /// The store funds text object must be present on the canvas object.
    /// </summary>
    [SerializeField]
    private Text storeFundsText;

    /// <summary>
    /// The item description text object must be present on the canvas object.
    /// </summary>
    [SerializeField]
    private Text descriptionText;

    /// <summary>
    /// This provides the parent object to be able to attach the runtime generated inventory list.
    /// </summary>
    [SerializeField]
    private GameObject inventoryListParent;

    /// <summary>
    /// The button prefab to be generated. This represents a single item in the store.
    /// </summary>
    [SerializeField]
    private GameObject listItemPrefab;

    /// <summary>
    /// Keep track of the store data.
    /// </summary>
    private StoreInitializeMsg storeData;

    /// <summary>
    /// Perform some basic error checking.
    /// </summary>
    private void Start()
    {
        if (storeNameText == null)
        {
            Debug.LogError("Please attach a store name text object");
        }

        if (storeFundsText == null)
        {
            Debug.LogError("Please attach a store funds text object");
        }

        if (inventoryListParent == null)
        {
            Debug.LogError("Please attach a store inventory list parent");
        }

        if (listItemPrefab == null)
        {
            Debug.LogError("Please attach a list item prefab");
        }
    }

    /// <summary>
    /// The store initialization comes from the store model.
    /// </summary>
    /// <param name="msg">The message containing all the information about the store, including store name and items etc.</param>
    [RecvMsgMethod]
    public void HandleStoreInitializeMsg(StoreInitializeMsg msg)
    {
        storeData = msg;

        storeNameText.text = msg.StoreName;
        storeFundsText.text = msg.StoreFunds.ToString();

        foreach (Item item in msg.StoreItems)
        {
            GameObject listItem = (GameObject)Instantiate(listItemPrefab);

            listItem.GetComponentInChildren<Text>().text = item.Name;

            listItem.transform.SetParent(inventoryListParent.transform);
        }

        DisplayDescription();
    }

    private void DisplayDescription()
    {
        descriptionText.text = storeData.StoreItems[0].Description;
    }
}
