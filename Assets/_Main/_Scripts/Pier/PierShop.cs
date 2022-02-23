using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PierShop : MonoBehaviour
{
    //[SerializeField] private ShopStats shopStats;
    //[SerializeField] private Button sellFishesButton;
    //[SerializeField] private Button exitMenueButton;
    //private void Start()
    //{

    //}
    //public void SellFishes()
    //{
    //    OnSell?.Invoke(shopStats.SellItemValue);
    //}
    [SerializeField] private TextMeshProUGUI moneyCount;

    [SerializeField] private List<ItemScriptableObject> itemPrefabs = new List<ItemScriptableObject>();

  //  [SerializeField] private List<ShopItem> itemsInShop = new List<ShopItem>();

    [SerializeField] private GameObject sellItemTemplate;

    [SerializeField] private Transform scrollView;

    [SerializeField] private Transform itemContainer;
    public event Action<int> OnSell;
    public event Action<int> OnBuy;

    private void Start()
    {
        InitializeShop();
    }

    private void InitializeShop()
    {
        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            var item = Instantiate(sellItemTemplate, itemContainer);
            var itemToAssign = itemPrefabs[i];
            var shp = item.GetComponent<ShopItem>();
            shp.AssignController(this);
            shp.AssignItem(itemToAssign);
        }
    }
    public void Buy(int value)
    {
        OnBuy?.Invoke(value);
    }
    public void Sell(int value, ItemScriptableObject itemToSell)
    {
        if (HudManager.Instance.Inventory.CheckForItem(itemToSell))
        {
            OnSell?.Invoke(value);
        }
        else
        {
            print("No tenes esto Padre");
        }
    }

    public void CloseMenue()
    {
        HudManager.Instance.ExitMenue();
        gameObject.SetActive(false);
    }

    public void UpdateMoneyCount(int actualMoney)
    {
        moneyCount.text = actualMoney.ToString();
    }

    private void OnEnable()
    {
        HudManager.Instance.EnterInMenu();
    }


}
