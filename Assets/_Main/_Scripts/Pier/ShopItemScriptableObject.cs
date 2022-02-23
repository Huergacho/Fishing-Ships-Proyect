using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu (fileName = "ShopItem",menuName = "Shop Items", order = 0)]
public class ShopItemScriptableObject : ScriptableObject, IStorable
{
    [SerializeField] private string itemName;
    public string ItemName => itemName;


    [SerializeField] private Sprite icon;
    public Sprite ShowImage => icon;


    [SerializeField] private int itemValue;
    public int ItemValue => itemValue;

    [SerializeField] private GameObject objectReference;

    public GameObject ObjectReference => objectReference;

}
