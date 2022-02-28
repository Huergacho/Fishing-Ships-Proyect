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
    [SerializeField] private ItemScriptableObject _item;
    [SerializeField] private Button sellButton;
    public ItemScriptableObject Item => _item;
    [SerializeField] private Button clearButton;
    private InventoryHud _controller;

    private void Start()
    {
        sellButton?.onClick.AddListener(OnSellItem);
        clearButton?.onClick.AddListener(ClearSlot);
        WipeSlot(); 
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
        clearButton.gameObject.SetActive(true);

    }
    public void ClearSlot()
    {
        _controller.RemoveFromInventory(_item);
        WipeSlot();
    }
    private void WipeSlot()
    {
        _item = null;
        icon.sprite = null;
        icon.enabled = false;
        CanSell(false);
        clearButton.gameObject.SetActive(false);

    }
    private void OnSellItem()
    {
        if(Item == null)
        {
            return;
        }
        HudManager.Instance.PierShop.Sell(_item.ItemValue);
        ClearSlot();
    }
    public void CanSell(bool canSell)
    {
        sellButton.gameObject.SetActive(canSell);
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
