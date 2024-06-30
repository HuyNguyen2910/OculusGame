using HVDUnityBase.Base.DesignPattern;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class PurchaserMgr : PersistentSingleton<PurchaserMgr>, IStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
    public event Action<string> OnPurchaseComplete;
    public event Action<string> OnPurchaseFail;
    public const string Consume_2 = "com.shiqing.protectthecity.2item";
    public const string Consume_5 = "com.shiqing.protectthecity.6item";
    public const string Consume_10 = "com.shiqing.protectthecity.15item";
    public const string Consume_20 = "com.shiqing.protectthecity.40item";
    public const string Consume_50 = "com.shiqing.protectthecity.100item";
    public const string Consume_100 = "com.shiqing.protectthecity.220item";

    private Action m_actionCall;

    bool isInits = false;

    void Start()
    {
        InitPurchase();
    }
    public void InitPurchase(Action callBack = null)
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
        callBack?.Invoke();
    }


    public IStoreController Instance_StoreController
    {
        get { return m_StoreController; }
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(Consume_2, ProductType.Consumable);
        builder.AddProduct(Consume_5, ProductType.Consumable);
        builder.AddProduct(Consume_10, ProductType.Consumable);
        builder.AddProduct(Consume_20, ProductType.Consumable);
        builder.AddProduct(Consume_50, ProductType.Consumable);
        builder.AddProduct(Consume_100, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
        
    }

    public bool IsInitialized()
    {
        
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public string OnGetLocalPrice(string strID)
    {
        if (IsInitialized() == false)
            return "...";

        return m_StoreController.products.WithID(strID).metadata.localizedPriceString.ToString();
    }
    public string OnGetLocalPriceTitle(string strID)
    {
        if (IsInitialized() == false)
            return "...";

        return m_StoreController.products.WithID(strID).metadata.localizedTitle.ToString();
    }

    public void BuyProductID(string productId)
    {
        m_actionCall = null;
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void BuyProductID(string productId, Action actionCallback)
    {
        m_actionCall = null;
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            m_actionCall = actionCallback;
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
            
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        m_actionCall = null;
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) => {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            //AnalyticMng.Func_Send_Custom_Event("Restore Fail!");
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {

        if (String.Equals(args.purchasedProduct.definition.id, Consume_2, StringComparison.Ordinal))
        {
            OnPurchaseComplete?.Invoke(Consume_2);
            return PurchaseProcessingResult.Complete;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Consume_5, StringComparison.Ordinal))
        {
            OnPurchaseComplete?.Invoke(Consume_5);
            return PurchaseProcessingResult.Complete;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Consume_10, StringComparison.Ordinal))
        {
            OnPurchaseComplete?.Invoke(Consume_10);
            return PurchaseProcessingResult.Complete;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Consume_20, StringComparison.Ordinal))
        {
            OnPurchaseComplete?.Invoke(Consume_20);
            return PurchaseProcessingResult.Complete;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Consume_50, StringComparison.Ordinal))
        {
            OnPurchaseComplete?.Invoke(Consume_50);
            return PurchaseProcessingResult.Complete;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, Consume_100, StringComparison.Ordinal))
        {
            OnPurchaseComplete?.Invoke(Consume_100);
            return PurchaseProcessingResult.Complete;
        }

        return PurchaseProcessingResult.Pending;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        //Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        OnPurchaseFail?.Invoke(product.definition.id);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }
}
