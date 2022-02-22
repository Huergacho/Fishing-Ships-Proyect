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
    //public event Action<int> OnSell;
    //private void Start()
    //{

    //}
    //public void SellFishes()
    //{
    //    OnSell?.Invoke(shopStats.SellItemValue);
    //}
    [SerializeField] private TextMeshProUGUI moneyCount;

    [SerializeField] private List<IStorable> itemStock = new List<IStorable>();

    [SerializeField] private GameObject sellItemTemplate;

    [SerializeField] private Transform scrollView;

    private void InitializeShop()
    {
        for (int i = 0; i < itemStock.Count; i++)
        {
            var item = Instantiate(sellItemTemplate, scrollView);
            var itemStorable = item.GetComponent<IStorable>();
            itemStock.Add(itemStorable);
        }
    }

    private void OnSell(IStorable itemToSell)
    {
       
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
