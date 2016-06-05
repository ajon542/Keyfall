using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoreView : IGameView
{
    [SerializeField]
    private Text storeNameText;

    [SerializeField]
    private Text storeFundsText;

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
    }
}
