using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
   public class Inventory : MonoBehaviour
    {
    private int actualMoney;
    private void Start()
    {
        HudManager.Instance.PierShop.UpdateMoneyCount(actualMoney);
        HudManager.Instance.Inventory.onSelledItem += OnSell;
        HudManager.Instance.QuestController.onDeliver += OnSell;
    }
    private void OnSell(int value)
    {
        AddMoney(value);
    }
    public void AddToInventory(ItemScriptableObject itemToAdd)
    {
        HudManager.Instance.Inventory.AddItemToInventory(itemToAdd);
        HudManager.Instance.RewardObtainText.SetObtainedItem(itemToAdd.ItemName);
    }
    private void AddMoney(int moneyToAdd)
    {
        actualMoney += moneyToAdd;
        HudManager.Instance.PierShop.UpdateMoneyCount(actualMoney);
    }
}
