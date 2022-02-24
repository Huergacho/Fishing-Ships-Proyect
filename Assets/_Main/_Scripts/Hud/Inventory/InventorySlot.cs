using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField]private ItemScriptableObject _item;
    public ItemScriptableObject Item => _item;
    [SerializeField] private Button clearButton;
    private InventoryHud _controller;

    private void Start()
    {
        clearButton.onClick.AddListener(ClearSlot);
        if(_item == null)
        icon.enabled = false;
        HudManager.Instance.PierShop.OnSell += OnSellItem;
    }
    public void AssingController(InventoryHud controllerAssigned)
    {
        _controller = controllerAssigned;
    }
    public void AddItem(ItemScriptableObject newItem)
    {
        icon.enabled = true;
        _item = newItem;
        icon.sprite = newItem.ShowImage;

    }
    public void ClearSlot()
    {
        _controller.RemoveFromInventory(_item); 
        _item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
    private void OnSellItem(ItemScriptableObject itemSelled)
    {
        if (itemSelled == _item)
        {
            ClearSlot();
        }
    }
    public bool isSlotEmpty()
    {
        if(_item != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
