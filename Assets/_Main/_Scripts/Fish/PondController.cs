using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class PondController : MonoBehaviour, IFishMinigame
{
    private PlayerModel _target;
    [SerializeField] private FishPond fishPondPrefab;
    [SerializeField] private List<FishPond> fishPonds = new List<FishPond>();
    [SerializeField] private int quantityOfPonds;
    private FishPond actualFishPond;
    [SerializeField] private float spawnRadius;
    private FishMinigameController _controller;
    public event Action OnfishPondAssigned;
    private bool minigameRunning;
    [SerializeField] private LootTable fishingRoullete;
    #region StartActions
    public void Initialize()
    {
        SuscribeEvents();
        _controller = FishMinigameController.Instance;
        _target = FishMinigameController.Instance.Player;
        GenerateFishPonds();
    }
    private Vector3 GenerateFishes()
    {
        Vector3 spawnPosition = new Vector3(Random.insideUnitCircle.x * spawnRadius, 1f, Random.insideUnitCircle.y * spawnRadius);
        return spawnPosition;
    }

    private void GenerateFishPonds()
    {
        for (int i = 0; i < quantityOfPonds; i++)
        {
            var pondClone = Instantiate(fishPondPrefab, new Vector3(GenerateFishes().x, _target.transform.position.y, GenerateFishes().z), Quaternion.identity);
            pondClone.Controller = _controller;
            fishPonds.Add(pondClone);
        }
    }
    void SuscribeEvents()
    {
        FishMinigameController.Instance.IsMinigameRunning += ActionsInMinigame;
        FishMinigameController.Instance.OnFish += GetFishesFromPond;
        FishMinigameController.Instance.Player.OnPondAssign += AssignFishPond;
    }
    #endregion

    #region EndActions
    public void OnEnd()
    {
        actualFishPond.gameObject.SetActive(false);
        actualFishPond = null;
    }
    #endregion

    private void Start()
    {
        Initialize();
    }

    public void AssignFishPond(FishPond fishPond)
    {
        actualFishPond = fishPond;
        OnfishPondAssigned.Invoke();
    }
    public void GetFishesFromPond()
    {
        actualFishPond.GetFish(fishingRoullete.GetRandomItem().itemName);
        
    }
    private void Update()
    {
    }
    public float GetDistance()
    {
        if(actualFishPond != null)
        {
            var distance = Vector3.Distance(actualFishPond.transform.position, _target.transform.position);
            return distance;
        }
        else
        {
            return 0;
        }
    }
    public void AssignFishController(FishMinigameController controller)
    {
        _controller = controller;
    }
    public void ActionsInMinigame(bool isMinigameRunning)
    {
        if (isMinigameRunning)
        {
            minigameRunning = true;
            return;
        }
        else
        {
            minigameRunning = false;
            OnEnd();
          
        }
    }


}
