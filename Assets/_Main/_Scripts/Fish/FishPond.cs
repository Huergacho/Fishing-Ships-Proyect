using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPond : MonoBehaviour
{
    [SerializeField] private PondStats pondStats;
    [SerializeField] private int fishQuantity;
    [SerializeField] public  List<FishScriptableObject> fishes = new List<FishScriptableObject>();
    private void Start()
    {
        fishQuantity = Random.Range(1, pondStats.MaxFishes);
    }
    public void TakeFishes()
    {
        fishQuantity--;
    }
    public int GetActualFishes()
    {
        return fishQuantity;
    }
}
