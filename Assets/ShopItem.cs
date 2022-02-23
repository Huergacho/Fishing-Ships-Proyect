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
    [SerializeField] private Button infoButton;
    [SerializeField] private GameObject infoObject;
    [SerializeField] private TextMeshProUGUI infoText;

    private PierShop _controller;
    private bool infoState;
    // Start is called before the first frame update
    void Start()
    {
        infoButton?.onClick.AddListener(ShowInfo);
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
    public void AssignController(PierShop controller)
    {
        _controller = controller;
    }
    void SellItem()
    {
        if(itemStats != null)
        {
            _controller.Sell(itemStats.ItemValue, itemStats);
        }
    }

    void ShowInfo()
    {
        if(infoState == true)
        {
        infoObject.SetActive(false); 
        }
        else
        {
            infoObject.SetActive(true);
        }
        
    }
}
