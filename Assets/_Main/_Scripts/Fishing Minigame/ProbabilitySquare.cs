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

    }
    public void Initialize()
    {
        float spawn = Random.Range(barriers[0].transform.localPosition.y, barriers[1].transform.localPosition.y);
        transform.localPosition = new Vector3(transform.localPosition.x, spawn, transform.localPosition.z);
    }
    private void SuscribeEvents()
    {
        FishMinigameController.Instance.IsMinigameRunning += ActionsInMinigame;
        FishMinigameController.Instance.OnMinigameRestart += Initialize;
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
