using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerModel : BaseActor
{
    #region SerializeFields

    [SerializeField] private Transform mouseIndicator;
    [SerializeField] private float distanceToFish;
    [SerializeField]private int maxFishes;
    [SerializeField] private int actualMoney;

    #endregion

    private int actualFishes = 0;
    private Rigidbody _rb;
    public float DistanceToFish => distanceToFish;
    #region Events

    public event Action<FishPond> OnPondAssign;

    #endregion
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

    private void InitializeHud()
    {
        HudManager.Instance.FishCounter.UpdateFishesCount(actualFishes, maxFishes);
        HudManager.Instance.PierShop.UpdateMoneyCount(actualMoney);
    }
    
    #region Movement

    public void Idle()
    {

    }

    public void Move()
    {
        MoveAtMousePos();
    }

    #endregion

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
    #endregion

    #region Fishes Controll
    public void Fish(FishPond fishPond)
    {
        if (fishPond != null)
        {
            OnPondAssign?.Invoke(fishPond);
        }
    }

    public void AddToInventory(int fishesToAdd)
    {
        //fishes.Add(fishToAdd);
        actualFishes+= fishesToAdd;
        HudManager.Instance.FishCounter.UpdateFishesCount(actualFishes,maxFishes);
        HudManager.Instance.FishCounter.AddFishes(fishesToAdd);
    }

    private void AddMoney(int moneyToAdd,int fishesSelled)
    {
        if(actualFishes != 0)
        {
            RemoveFishes(fishesSelled);
            actualMoney += moneyToAdd;
            HudManager.Instance.PierShop.UpdateMoneyCount(actualMoney);
        }
    }

    private void RemoveFishes(int fishesToRemove)
    {
        actualFishes -= fishesToRemove;
        HudManager.Instance.FishCounter.UpdateFishesCount(actualFishes, maxFishes);
        HudManager.Instance.FishCounter.RemoveFishes(fishesToRemove);
    }

    #endregion

    #region Buffs
    public void GetBoosted(bool isBoosted)
    {
        if (isBoosted)
        {
            speed = actorStats.RunSpeed;
            return;
        }
        else if (!isBoosted)
        {
            speed = actorStats.WalkSpeed;
            return;
        }
    }

    #endregion

}
