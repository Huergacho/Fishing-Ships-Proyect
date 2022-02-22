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
    [SerializeField]private IStorable _item;
    [SerializeField] private Button clearButton;
    private InventoryHud _controller;

    private void Start()
    {
        clearButton.onClick.AddListener(ClearSlot);
        icon.enabled = false;
    }
    public void AssingController(InventoryHud controllerAssigned)
    {
        _controller = controllerAssigned;
    }
    public void AddItem(IStorable newItem)
    {
        _item = newItem;
        icon.sprite = newItem.ShowImage;
        icon.enabled = true;

    }
    public void ClearSlot()
    {
        _controller.RemoveFromInventory(_item);
        _item = null;
        icon.sprite = null;
        icon.enabled = false;
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
