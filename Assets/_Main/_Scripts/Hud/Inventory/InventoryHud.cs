﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class InventoryHud : MonoBehaviour
{
    [SerializeField] private List<ItemScriptableObject> _itemsStored = new List<ItemScriptableObject>();
    [SerializeField] private GameObject itemSlotTemplate;
    [SerializeField] private List<InventorySlot> _itemSlots = new List<InventorySlot>();
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private GameObject model;
    [SerializeField] private int _maxSlots;
    [SerializeField] private int _currentSlots;
    public event Action<int> onSelledItem;

    public Action OnRemoveItem;
    private void Start()
    {
        _currentSlots = _maxSlots;
        InitializeSlots();
        model.SetActive(false);

        HudManager.Instance.PierShop.onShop += ForceInventoryToShow;
        //HudManager.Instance.PierShop.onShop += isOnShop;
        HudManager.Instance.PierShop.OnSell += SellItem;
    }

    public void InitializeSlots()
    {
        for (int i = 0; i < _currentSlots; i++)
        {
            var slot = Instantiate(itemSlotTemplate, itemSlotContainer);
            slot.GetComponent<InventorySlot>().AssingController(this);
            _itemSlots.Add(slot.GetComponent<InventorySlot>());
        }
    }
    public void AddItemToInventory(ItemScriptableObject itemToAdd)
    {
        bool assigned = false;
        if(_maxSlots >= _itemsStored.Count)
        {
            
            for (int i = 0; i < _itemSlots.Count; i++)
            {
                var item = _itemSlots[i];
                if (item.isSlotEmpty() && assigned == false)
                {
                    item.AddItem(itemToAdd);
                    _itemsStored.Add(itemToAdd);
                    assigned = true;
                    return;
                }
            }
        }
        else
        {
            print("No Hay Lugar");
            return;
        }
    }
    public void RemoveFromInventory(ItemScriptableObject itemToRemove)
    {
        if(itemToRemove != null)
        {
            _itemsStored.Remove(itemToRemove);
            OnRemoveItem?.Invoke();
        }
    }
    public void ShowInventory()
    {
        if(model.activeInHierarchy == true)
        {
            model.SetActive(false);
        }
        else
        {
            model.SetActive(true);
        }
    }
    public void ForceInventoryToShow(bool stateToForce)
    {
        model.SetActive(stateToForce);
    }
    public bool CheckForItem(ItemScriptableObject itemToCheckFor)
    {
        if (_itemsStored.Contains(itemToCheckFor))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void SellItem(ItemScriptableObject itemToSell)
    {
        bool selled = false;
        if (CheckForItem(itemToSell) && selled == false)
        {
            for (int i = 0; i < _itemSlots.Count; i++)
            {
                var itemSlot = _itemSlots[i];
                if(itemSlot.Item != itemToSell)
                {
                    continue;
                }
                else
                {
                    itemSlot.OnSellItem();
                    onSelledItem?.Invoke(itemToSell.ItemValue);
                    selled = true;
                    return;
                }
            }
        }
    }
}
