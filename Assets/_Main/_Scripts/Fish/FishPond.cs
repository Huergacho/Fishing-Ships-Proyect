using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPond : MonoBehaviour,IFishMinigame
{
    [SerializeField] private PondStats pondStats;
    [SerializeField] private int fishQuantity;
    private PlayerModel target;
    [SerializeField] public  List<FishScriptableObject> fishes = new List<FishScriptableObject>();
    public FishMinigameController Controller;
    private void Start()
    {
        fishQuantity = Random.Range(1, pondStats.MaxFishes +1);
        SuscribeEvents();
        AssignPlayer();

    }
    public void TakeFishes()
    {
        fishQuantity--;
    }
    public void GetFish()
    {
        var fishObtained = Random.Range(0, fishQuantity);
        target.AddToInventory(fishes[fishObtained]);
        TakeFishes();
        if (fishQuantity<= 0)
        {
            Controller.OnEnd();
            OnEnd();
        }
        else
        {
            Controller.RestartMinigame();
        }
    }
    public void OnEnd()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.SetActive(false);
    }
    private void SuscribeEvents()
    {
        Controller.IsMinigameRunning += ActionsInMinigame;
    }
    private void AssignPlayer()
    {
        target = GameManager.instance.player;
    }
    public void ActionsInMinigame(bool isMinigameRunning)
    {
        if (isMinigameRunning)
        {
            return;
        }
    }

    public void Initialize()
    {
       gameObject.GetComponent<Collider>().enabled = false;
    }
}
