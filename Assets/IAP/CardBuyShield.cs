using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBuyShield : MonoBehaviour
{
    public Text txtNumber, txtPrice;
    public Button btnBuy;

    public string sku;
    private void Start()
    {
        Init();
    }
    void Init()
    {
        txtPrice.text = $"Price {price()}$";
        txtNumber.text = $"x {number()}";
    }
    public string price()
    {
        switch (sku)
        {
            case "price_2":
                return "1.99";
            case "price_5":
                return "4.99";
            case "price_10":
                return "9.99";
            case "price_20":
                return "19.99";
            case "price_50":
                return "49.99";
            case "price_100":
                return "99.99";
            case "price_200":
                return "199.99";
            default:
                return "0.00";
        }
    }
    public string number()
    {
        switch (sku)
        {
            case "price_2":
                return "4";
            case "price_5":
                return "11";
            case "price_10":
                return "22";
            case "price_20":
                return "45";
            case "price_50":
                return "120";
            case "price_100":
                return "250";
            case "price_200":
                return "500";
            default:
                return "0";
        }
    }
}
