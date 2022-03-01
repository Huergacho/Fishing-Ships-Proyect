using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Default Item", menuName = "Item", order = 0)]
public class ItemScriptableObject : ScriptableObject, IStorable
{
    [SerializeField] private int probability;
    public int Probability => probability;


    [SerializeField] private string itemName;
    public string ItemName => itemName;


    [SerializeField] private Sprite showImage;
    public Sprite ShowImage => showImage;

    [SerializeField] private int itemValue;
    public int ItemValue => itemValue;

    [SerializeField] private int itemIndex;

    public int ItemIndex => itemIndex;

}
