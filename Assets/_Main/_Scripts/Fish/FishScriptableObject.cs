using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultFish", menuName = "Fishes",order = 0)]
public class FishScriptableObject : ScriptableObject
{
    [SerializeField] private int category;
    [SerializeField] private string fishName;
}
