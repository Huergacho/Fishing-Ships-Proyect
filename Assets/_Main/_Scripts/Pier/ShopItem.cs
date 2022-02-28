using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopItem : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private ItemScriptableObject itemStats;
    [SerializeField] private TextMeshProUGUI infoText;
    private PierShop _controller;
    // Start is called before the first frame update
    void Start()
    {
        buyButton?.onClick.AddListener(BuyItem);
        sellButton?.onClick.AddListener(SellItem);
    }

    public void AssignItem(ItemScriptableObject newItem)
    {
        itemStats = newItem;
        icon.sprite = itemStats.ShowImage;
        infoText.text = $"{itemStats.ItemValue}";
    }
    void BuyItem()
    {
        
    }
    private void SellItem()
    {
        _controller.Sell(itemStats);
    }
    public void AssignController(PierShop controller)
    {
        _controller = controller;
    }
}
