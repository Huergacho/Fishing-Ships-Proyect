using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private ItemScriptableObject _item;
    [SerializeField] private TextMeshProUGUI stackText;
    [SerializeField] private Button clearButton;
    public ItemScriptableObject Item => _item;
    private InventoryHud _controller;
    [SerializeField]private int quantity = 0;
    public int Quantity => quantity;

    private void Start()
    {
        clearButton?.onClick.AddListener(RemoveItem);
        if(Item == null)
        {
            WipeSlot(); 
        }
    }
    public void AssingController(InventoryHud controllerAssigned)
    {
        _controller = controllerAssigned;
    }
    public void Stack()
    {
        if (_item != null)
        {
            quantity++;
        }
        stackText.text =  $"{quantity}";
    }
    public void Destack()
    {
        if (_item == null)
        {
            return;
        }
        else
        {
            CheckQuantity();
        }
    }
    public void AddItem(ItemScriptableObject newItem)
    {
        icon.enabled = true;
        _item = newItem;
        icon.sprite = newItem.ShowImage;
        clearButton.gameObject.SetActive(true);
        Stack();

    }
    public void RemoveItem()
    {
        _controller.RemoveFromInventory(this.Item, 1);
    }
    private void WipeSlot()
    {
        _item = null;
        icon.sprite = null;
        icon.enabled = false;
        stackText.text = string.Empty;
        quantity = 0;
        clearButton.gameObject.SetActive(false);

    }
    public void OnSellItem()
    {
        if(Item == null)
        {
            return;
        }
        RemoveItem();
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
    private void CheckQuantity()
    {
        if(quantity <= 1)
        {
            WipeSlot();
        }
        if(quantity >= 2)
        {
            quantity--;
            stackText.text = $"{quantity}";
        }

    }
}
