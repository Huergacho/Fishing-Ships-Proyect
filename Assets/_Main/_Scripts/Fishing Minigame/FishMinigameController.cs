using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FishMinigameController : MonoBehaviour, IFishMinigame
{
    [SerializeField] public float distanceToDetectPlayer;
    [SerializeField] private GameObject _fishIndicator;
    [SerializeField] private GameObject model;
    [SerializeField] private PondController pondController;
    private PlayerModel _player;
    public PlayerModel Player => _player;
    private bool isRunning;
    bool isAlreadyFishing;
    public event Action<bool> IsMinigameRunning;
    public static FishMinigameController Instance;
    public event Action OnFish;
    public event Action OnMinigameRestart;
    [SerializeField] private float timeToEnd;
    private void Awake()
    {
        Instance = this;
        
    }
    private void Start()
    {
        Initialize();
    }
    private void Update()
    {
        if (isRunning)
        {
            CheckForPlayerDistance();
        }
    }
    public void AssingPond()
    {

        if (isAlreadyFishing) { return; }
        if (!isAlreadyFishing)
        {
            isRunning = true;
            model.gameObject.SetActive(true);
            IsMinigameRunning?.Invoke(true);
            isAlreadyFishing = true;
            return;
        }

    }
    public void Fish()
    {
        OnFish?.Invoke();
    }
    void CheckForPlayerDistance()
    {
        if(pondController.GetDistance() >= distanceToDetectPlayer)
        {
            OnEnd();
        }
    }
    void AssingPlayer()
    {
        _player = GameManager.instance.player;
    }

    public void OnEnd()
    {
        StartCoroutine(EndActions());
        isRunning = false;
    }

    public void ActionsInMinigame(bool isMinigameRunning)
    {
        if (isMinigameRunning)
        {
            isRunning = true;
        }
    }
    public void RestartMinigame()
    {
        OnMinigameRestart.Invoke();
    }

    public void Initialize()
    {
        AssingPlayer();
        SuscribeEvents();
        model.SetActive(false);
    }
    private void SuscribeEvents()
    {
        pondController.OnfishPondAssigned += AssingPond;
    }
    IEnumerator EndActions()
    {
        IsMinigameRunning?.Invoke(false);
        yield return new  WaitForSeconds(timeToEnd);
        isAlreadyFishing = false;
        model.SetActive(false);
        StopCoroutine(EndActions());
    }
}
