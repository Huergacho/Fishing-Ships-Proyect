using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPond : MonoBehaviour,IFishMinigame
{
    [SerializeField] private PondStats pondStats;
    private int fishQuantity;
    private PlayerModel target;
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
    public void GetFish(string fishName)
    {
        var fishObtained = Random.Range(1, fishQuantity);
        target.AddToInventory(fishObtained, fishName);
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
