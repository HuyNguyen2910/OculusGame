using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemShop : MonoBehaviour
{
    [SerializeField] private Button useButton;
    [SerializeField] private TextMeshProUGUI useText;
    [SerializeField] private int index;
    [SerializeField] private bool isExplodeShop;

    private string useString = "Use";
    private string usingString = "Using";

    private void Start()
    {
        useButton.onClick.AddListener(Use);
    }
    public void Setup(int idx, bool isExplode)
    {
        index = idx;
        isExplodeShop = isExplode;
        SetIsUsing();
    }
    public void SetIsUsing()
    {
        if ((isExplodeShop && UserData.Instance.currentExplode == index) || (!isExplodeShop && UserData.Instance.currentFloor == index))
        {
            SetUsing(true);
        }
        else
        {
            SetUsing(false);
        }
    }    
    private void SetUsing(bool isUsing)
    {
        useText.text = isUsing ? usingString : useString;
        useButton.interactable = isUsing ? false : true;
    }
    private void Use()
    {
        string shop = isExplodeShop ? Define.EXPLODE_SHOP : Define.FLOOR_SHOP;
        UserData.Instance.SaveCurrentShop(shop, index);
        if (isExplodeShop)
        {
            ShopScreen.Instance.GetExplodeShop();
        }
        else
        {
            ShopScreen.Instance.GetFloorShop();
        }
    }    
}
