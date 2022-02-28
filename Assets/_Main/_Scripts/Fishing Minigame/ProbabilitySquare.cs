using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilitySquare : MonoBehaviour,IFishMinigame
{
    [SerializeField] private Transform[] barriers;
    [SerializeField] private GameObject indicator;
    private void Start()
    {
        SuscribeEvents();
        Initialize();

    }
    public void Initialize()
    {
        SetRandomPos(); 
    }

    private void SuscribeEvents()
    {
        FishMinigameController.Instance.IsMinigameRunning += ActionsInMinigame;
        FishMinigameController.Instance.OnMinigameRestart += Restart;
    }
    void Restart()
    {
        Initialize();
    }
    void SetRandomPos()
    {
        float spawn = Random.Range(barriers[0].transform.localPosition.y, barriers[1].transform.localPosition.y);
        transform.localPosition = new Vector3(transform.localPosition.x, spawn, transform.localPosition.z);
    }
    public void ActionsInMinigame(bool isMinigameRunning)
    {
        if (isMinigameRunning)
        {
            Initialize();
            
            return;
        }
        else
        {
            OnEnd();
            return;
        }
        
    }
    public void OnEnd()
    {
    }

}
