using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerModel : BaseActor
{
    [SerializeField] private Transform mouseIndicator;
    [SerializeField] private float distanceToFish;
    [SerializeField] private List<FishScriptableObject> fishes = new List<FishScriptableObject>();
    private int actualFishes = 0;
    [SerializeField]private int maxFishes;
    public float DistanceToFish => distanceToFish;
    private Rigidbody _rb;
    //private FishPond actualFishItem;
    public event Action<FishPond> OnPondAssign;
    [SerializeField] private int actualMoney;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    protected override void Start()
    {
        GameManager.instance.player = this;
        InitializeHud();
        base.Start();
    }
    public void SuscribeEvents(PlayerStateMachine controller)
    {
        controller.onMove += Move;
        controller.onIdle += Idle;
        controller.onFish += Fish;
        controller.onMovePointer += MovePointer;
        HudManager.Instance.PierShop.OnSell += AddMoney;
    }
    public void Idle()
    {
    }
    public void Move()
    {
        MoveAtMousePos();
    }
    public void Fish(FishPond fishPond)
    {
        if(fishPond != null)
        {
            OnPondAssign?.Invoke(fishPond);
        }
    }
    #region Mouse Movement Calculation
    private void SmoothRotation()
    {
        var direction = (mouseIndicator.position - transform.position);
        if (direction != Vector3.zero)
        {
            var rotDestiny = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotDestiny, actorStats.RotSpeed * Time.deltaTime);
        }
    }
    private void MoveAtMousePos()
    {
        SmoothRotation();
        transform.position = Vector3.MoveTowards(transform.position, mouseIndicator.position, speed * Time.deltaTime); 
    }
    public void MovePointer(Vector3 target)
    {
        mouseIndicator.position = target;
    }
    public void AddToInventory(FishScriptableObject fishToAdd)
    {
        fishes.Add(fishToAdd);
        actualFishes++;
        HudManager.Instance.FishCounter.UpdateFishesCount(actualFishes,maxFishes);
    }
    private void AddMoney(int moneyToAdd)
    {
        if(actualFishes != 0)
        {
            actualMoney += moneyToAdd;
            HudManager.Instance.PierShop.UpdateMoneyCount(actualMoney);
        }
    }
    private void InitializeHud()
    {
        HudManager.Instance.FishCounter.UpdateFishesCount(actualFishes, maxFishes);
        HudManager.Instance.PierShop.UpdateMoneyCount(actualMoney);
    }
    #endregion

}
