using Oculus.Platform;
using Oculus.Platform.Models;
using System;
using System.Collections.Generic;
using UnityEngine;

public class OculusIAP : MonoBehaviour
{
    public string[] skus;

    [Header("CoinData")]
    private int CoinsCollected = 0;
    // Dictionary to keep track of SKUs being consumed
    private Dictionary<UInt64, string> skuDictionary = new Dictionary<UInt64, string>();
    void Start()
    {
        ///Đầu tiên gọi hàm này
        Core.AsyncInitialize().OnComplete(InitCallback);
        //GetPrices();
        //GetPurchases();

    }

    /// <summary>
    /// khởi tạo core flatfom xong
    /// </summary>
    /// <param name="msg"></param>
    private void InitCallback(Message<Oculus.Platform.Models.PlatformInitialize> msg)
    {
        if (msg.IsError)
        {
            Debug.LogError("Error initializing Oculus Platform: " + msg.GetError().Message);
            // Consider retrying initialization or disabling IAP functionality
        }
        else
        {
            Debug.Log("Oculus Platform initialized successfully.");
            Entitlements.IsUserEntitledToApplication().OnComplete(EntitlementCheckCallback);
        }
    }
    private void EntitlementCheckCallback(Message msg)
    {
        if (msg.IsError)
        {
            Debug.LogError("User not entitled to application, cannot proceed.");
            // Application.Quit();
        }
        else
        {
            Debug.Log("User is entitled.");
            GetPrices();
            GetPurchases();
        }
    }
    private void GetPrices()
    {
        IAP.GetProductsBySKU(skus).OnComplete(GetPricesCallback);
    }

    private void GetPricesCallback(Message<ProductList> msg)
    {
        if (msg.IsError) return;
        foreach (var prod in msg.GetProductList())
        {
            //availableItems.text += $"{prod.Name} - {prod.FormattedPrice} \n";
        }
    }
    private void GetPurchases()
    {
        IAP.GetViewerPurchases().OnComplete(GetPurchasesCallback);
    }
    private void GetPurchasesCallback(Message<PurchaseList> msg)
    {
        if (msg.IsError) return;
        foreach (var purch in msg.GetPurchaseList())
        {
            // purchasedItems.text += $"{purch.Sku}-{purch.GrantTime} \n";
            //  AllocateCoins(purch.Sku);
            ConsumePurchase(purch.Sku);
        }
        CoinPurchaseDeductionCheck();
    }
    private void ConsumePurchase(string skuName)
    {
        //cosume without adding coins 
        //IAP.ConsumePurchase(skuName).OnComplete(ConsumePurchaseCallback);
        var request = IAP.ConsumePurchase(skuName);
        skuDictionary[request.RequestID] = skuName;
        request.OnComplete(ConsumePurchaseCallback);
    }

    private void ConsumePurchaseCallback(Message msg)
    {
        if (msg.IsError)
        {
            Debug.LogError("Error consuming purchase: " + msg.GetError().Message);
        }
        else
        {
            if (skuDictionary.TryGetValue(msg.RequestID, out var sku))
            {
                Debug.Log($"Purchase consumed successfully for SKU: {sku}");
                AllocateCoins(sku); // Call AllocateCoins for each consumable purchase
                skuDictionary.Remove(msg.RequestID);
            }
            else
            {
                Debug.Log("Purchase consumed successfully, but SKU not found in dictionary.");
            }
        }
    }

    public void AllocateCoins(string input)
    {
        switch (input)
        {
            case "price_2":
                GameCtrl.Instance.Live = GameCtrl.Instance.Live + 4;
                break;
            case "price_5":
                GameCtrl.Instance.Live = GameCtrl.Instance.Live + 11;
                break;
            case "price_10":
                GameCtrl.Instance.Live = GameCtrl.Instance.Live + 22;
                break;
            case "price_20":
                GameCtrl.Instance.Live = GameCtrl.Instance.Live + 45;
                break;
            case "price_50":
                GameCtrl.Instance.Live = GameCtrl.Instance.Live + 120;
                break;
            case "price_100":
                GameCtrl.Instance.Live = GameCtrl.Instance.Live + 250;
                break;
            //case "price_200":
            //    GameCtrl.Instance.Live = GameCtrl.Instance.Live + 500;
            //    break;
            default:
                CoinsCollected = CoinsCollected + 0;
                break;
        }

        //CoinsText.text = "Coins : " + CoinsCollected.ToString(); ;

    }
    public void CoinPurchaseDeductionCheck()
    {
        string data = PlayerPrefs.GetString("PurchasedItem", "0");
        if (!string.IsNullOrEmpty(data))
        {
            string[] items = data.Split(new string[] { "@@" }, StringSplitOptions.None);

            foreach (string item in items)
            {
                int value;
                if (int.TryParse(item, out value))
                {
                    Debug.Log("Retrieved value: " + value);


                    //CoinsCollected = CoinsCollected - airCraftPrice[value];
                }
                else
                {
                    Debug.LogError("Failed to parse item to integer: " + item);
                }
            }
        }
        else
        {
            Debug.Log("No data found in PlayerPrefs for 'PurchasedItem'.");
        }
    }

    public void BuyCoin(string skuName)
    {
        IAP.LaunchCheckoutFlow(skuName).OnComplete(BuyCoinCallBack);
    }
    private void BuyCoinCallBack(Message<Purchase> msg)
    {
        if (msg.IsError) return;

        //purchasedItems.text = string.Empty;
        GetPurchases();
    }
}