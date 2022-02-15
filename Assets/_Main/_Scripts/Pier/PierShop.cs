using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PierShop : MonoBehaviour
{
    [SerializeField] private ShopStats shopStats;
    [SerializeField] private Button sellFishesButton;
    [SerializeField] private Button exitMenueButton;
    [SerializeField] private TextMeshProUGUI moneyCount;
    public event Action<int> OnSell;
    private void Start()
    {
        sellFishesButton.onClick.AddListener(SellFishes);
        exitMenueButton.onClick.AddListener(CloseMenue);
    }
    public void CloseMenue()
    {
        HudManager.Instance.ExitMenue();
        gameObject.SetActive(false);
    }
    public void SellFishes()
    {
        OnSell?.Invoke(shopStats.SellItemValue);
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
