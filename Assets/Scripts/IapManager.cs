using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

[System.Serializable]
public class ConsumableItem {
    public string Name;
    public string Id;
    public string desc;
    public float price;
}

[System.Serializable]
public class IapManager : MonoBehaviour, IStoreListener
{
    public TextMeshProUGUI coinTxt;
    public ConsumableItem[] listItems;
    const string COIN_2000 = "coin_2000";
    const string COIN_6000 = "coin_6000";
    const string COIN_9500 = "coin_9500";
    public GameObject panel;

    IStoreController m_StoreController;
    int totalCoins = 0;
    //public CoinData coinData;
    void Start()
    {
        coinTxt.text = GameManager.Instance.coin.ToString();
        SetupBuilder();
    }
    private void Update()
    {
        coinTxt.text=GameManager.Instance.coin.ToString();
    }

    void SetupBuilder() 
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        for (int i = 0; i <listItems.Length; i++)
        {
            builder.AddProduct(listItems[i].Id, ProductType.Consumable);
        }

        UnityPurchasing.Initialize(this, builder);
    }
    public void PanelBuySuccess()
    {
        panel.SetActive(true);
    }
    public void Consumable_BtnCoin2000_Pressed()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxButton);
        m_StoreController.InitiatePurchase(COIN_2000);
    }
    public void Consumable_BtnCoin6000_Pressed()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxButton);
        m_StoreController.InitiatePurchase(COIN_6000);
    }
    public void Consumable_BtnCoin9500_Pressed()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxButton);
        m_StoreController.InitiatePurchase(COIN_9500);
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;
        if (product.definition.id == COIN_2000)
        {
            Debug.Log("Plus 2000 coins");
            GameManager.Instance.coin += 2000;
            coinTxt.text = GameManager.Instance.coin.ToString();
            GameManager.Instance.SaveCoin();
        } else if (product.definition.id == COIN_6000)
        {
            Debug.Log("Plus 6000 coins");
            GameManager.Instance.coin += 6000;
            GameManager.Instance.SaveCoin();
            coinTxt.text = GameManager.Instance.coin.ToString();
        } else if (product.definition.id == COIN_9500) 
        {
            Debug.Log("Plus 9500 coins");
            GameManager.Instance.coin += 9500;
            coinTxt.text = GameManager.Instance.coin.ToString();
            GameManager.Instance.SaveCoin();
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }
}
