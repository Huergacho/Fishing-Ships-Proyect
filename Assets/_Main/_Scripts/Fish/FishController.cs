using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    [SerializeField]private PlayerController target;
    [SerializeField]private List<FishScriptableObject> fishes = new List<FishScriptableObject>();
    [SerializeField] private string contactLayers;
    [SerializeField] private int fishQuantity;
    [SerializeField] private int maxFishes;
    private void Start()
    {
        fishQuantity = Random.Range(1, maxFishes);
        target = GameManager.instance.player;
    }
    private void Update()
    {
        if(fishQuantity <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void GetFish()
    {
        var fishToGet = Random.Range(0, fishes.Count);
        target.AddToInventory(fishes[fishToGet]);
        fishQuantity--;
    }
}
