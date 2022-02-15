using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pier Shop Stats",menuName = "Shop",order = 0)]
class ShopStats:ScriptableObject
{
    [SerializeField]private float refillTime;
    public float RefillTime => refillTime;

    [SerializeField]private int sellItemQuantity;
    public int SellItemQuantity => sellItemQuantity;

    [SerializeField]private int sellItemValue;
    public int SellItemValue => sellItemValue;
}

