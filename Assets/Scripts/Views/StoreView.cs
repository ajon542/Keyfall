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
    [SerializeField]
    private Text storeNameText;

    [SerializeField]
    private Text storeFundsText;

    [SerializeField]
    private GameObject inventoryListParent;

    [SerializeField]
    private GameObject listItemPrefab;

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
    }

    [RecvMsgMethod]
    public void HandleStoreInitializeMsg(StoreInitializeMsg msg)
    {
        storeNameText.text = msg.StoreName;
        storeFundsText.text = msg.StoreFunds.ToString();

        for(int i = 0; i < 30; ++i)
        {
            GameObject listItem = (GameObject)Instantiate(listItemPrefab);

            listItem.GetComponentInChildren<Text>().text = i.ToString();

            listItem.transform.SetParent(inventoryListParent.transform);
        }
    }
}
