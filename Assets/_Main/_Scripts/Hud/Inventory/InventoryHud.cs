using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class InventoryHud : MonoBehaviour
{
    [SerializeField] private HashSet<ItemScriptableObject> _itemsStored = new HashSet<ItemScriptableObject>();
    [SerializeField] private GameObject itemSlotTemplate;
    [SerializeField] private List<InventorySlot> _itemSlots = new List<InventorySlot>();
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private GameObject model;
    [SerializeField] private int _maxSlots;
    [SerializeField] private int _currentSlots;
    public Action OnRemoveItem;
    private void Start()
    {
        _currentSlots = _maxSlots;
        InitializeSlots();
        model.SetActive(false);

        HudManager.Instance.PierShop.onShop += ForceInventoryToShow;
        HudManager.Instance.PierShop.onShop += isOnShop;
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
    void isOnShop(bool state)
    {
        foreach (var item in _itemSlots)
        {
            if (!item.isSlotEmpty())
            {
                item.CanSell(state);
            }
        }
    }
}
