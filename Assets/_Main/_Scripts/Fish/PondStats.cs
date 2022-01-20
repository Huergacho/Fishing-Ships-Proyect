using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "Fishes",menuName ="PondsCaracteristics",order = 2)]
public class PondStats : ScriptableObject
{
    [SerializeField] private int maxFishes;
    public int MaxFishes => maxFishes;
}
