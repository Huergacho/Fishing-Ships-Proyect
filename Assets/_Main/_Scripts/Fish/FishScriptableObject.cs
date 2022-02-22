using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DefaultFish", menuName = "Fishes",order = 0)]
public class FishScriptableObject : ScriptableObject, IStorable
{
    [SerializeField] private int probability;
    public int Probability => probability;


    [SerializeField] private string fishName;
    public string ItemName => fishName;


    [SerializeField] private Sprite showImage;
    public Sprite ShowImage => showImage;


    [SerializeField] private bool canSellIt;
    public bool CanSellIt => canSellIt;


    [SerializeField] private int itemValue;
    public int ItemValue => itemValue;




}
