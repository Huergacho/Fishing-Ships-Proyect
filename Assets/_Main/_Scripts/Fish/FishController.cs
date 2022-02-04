using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class FishController : MonoBehaviour
{
    private PlayerController target;
    [SerializeField] private FishPond fishPondPrefab;
    [SerializeField] private List<FishPond> fishPonds = new List<FishPond>();
    [SerializeField] private int quantityOfPonds;
    private FishPond actualFishPond;
    [SerializeField] private float spawnRatio;
    public static FishController Instance;
    [SerializeField] private GameObject fishingMinigame;
    public static Action<bool> IsMinigameRunning;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        target = GameManager.instance.player;
        target.OnPondDetection += AssignFishPond;
        for (int i = 0; i < quantityOfPonds; i++)
        {
            var pondClone = Instantiate(fishPondPrefab, new Vector3(target.transform.position.x * Random.insideUnitCircle.x + Random.Range(1,spawnRatio), target.transform.position.y, target.transform.position.z * Random.insideUnitCircle.y + Random.Range(1, spawnRatio)), Quaternion.identity);
            fishPonds.Add(pondClone);
        }
     
    }
    public void AssignFishPond(FishPond fishPond)
    {
        actualFishPond = fishPond;
        fishingMinigame.transform.localPosition = new Vector3(actualFishPond.transform.position.x, actualFishPond.transform.position.y, actualFishPond.transform.position.z);
        StartMinigame();
    }
    public FishPond GetActualFishPond()
    {
        return actualFishPond;
    }
    public void GetFish()
    {
        var fishObtained = Random.Range(0, actualFishPond.fishes.Count);
        target.AddToInventory(actualFishPond.fishes[fishObtained]);
        actualFishPond.TakeFishes();
        if (actualFishPond != null && actualFishPond.GetActualFishes() <= 0)
        {
            End();
        }
        if (actualFishPond != null && actualFishPond.GetActualFishes() > 0)
        {
            fishingMinigame.GetComponent<ProbabilitySquare>().Initialize();
        }

    }
    public void End()
    {
  
       StartCoroutine(EndActions());
    }
    private void StartMinigame()
    {
        IsMinigameRunning?.Invoke(true);
        fishingMinigame.GetComponent<ProbabilitySquare>().Initialize();
        fishingMinigame.SetActive(true);
    }
    IEnumerator EndActions()
    {
       yield return new WaitForSeconds(2f);
        actualFishPond.gameObject.SetActive(false);
        fishingMinigame.SetActive(false);
        actualFishPond = null;
        IsMinigameRunning?.Invoke(false);
        StopCoroutine(EndActions());
    }
}
