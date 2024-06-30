using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScreen : MonoBehaviour
{
    public static ShopScreen Instance;

    [SerializeField] private List<ItemShop> itemExplodeShops;
    [SerializeField] private List<ItemShop> itemFloorShops;

    private string explodeString = "exploreShop";
    private string floorString = "floorShop";
    private void Awake()
    {
        Instance = this;
        //LoadPrefs();
    }
    private void Start()
    {
        GetExplodeShop();
        GetFloorShop();
    }
    public void GetExplodeShop()
    {
        for (int i = 0; i < itemExplodeShops.Count; i++)
        {
            itemExplodeShops[i].Setup(i, true);
        }
    }
    public void GetFloorShop()
    {
        for (int i = 0; i < itemFloorShops.Count; i++)
        {
            itemFloorShops[i].Setup(i, false);
        }
    }
}
