using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HudManager : MonoBehaviour
{
    [SerializeField] private GameObject[] activeItems;
    [SerializeField] private RewardObtainText rewardObtainedText;
    public RewardObtainText RewardObtainText => rewardObtainedText;

    [SerializeField] private PierShop pierShop;
    public PierShop PierShop => pierShop;

    [SerializeField] private InventoryHud inventory;
    public InventoryHud Inventory => inventory;

    private static HudManager instance;
    public static HudManager Instance => instance;
    
    public event Action<bool> isOnMenue;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        pierShop.onShop += ShopMenue; 
        foreach(var item in activeItems)
        {
            item.SetActive(true);
        }
    }
    public void ShopMenue(bool shopMenue)
    {
        if (shopMenue)
        {
            EnterInMenu();
        }
        else
        {
            ExitMenue();
        }
    }
    public void EnterInMenu()
    {
        isOnMenue.Invoke(true);
    }
    public void ExitMenue()
    {
        isOnMenue.Invoke(false);
    }

}
