using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HudManager : MonoBehaviour
{
    #region Menues
    [SerializeField] private RewardObtainText rewardObtainedText;
    [SerializeField] private PierShop pierShop;
    [SerializeField] private InventoryHud inventory;
    [SerializeField] private QuestController questController;
    #endregion

    #region references
    public RewardObtainText RewardObtainText => rewardObtainedText;
    public PierShop PierShop => pierShop;
    public InventoryHud Inventory => inventory;
    public QuestController QuestController => questController;
    public static HudManager Instance => instance;
    #endregion

    [SerializeField] private GameObject[] activeItems;
    private static HudManager instance;
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
