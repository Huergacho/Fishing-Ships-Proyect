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
    [SerializeField] private float boostFVIncrease;
    [SerializeField] private List<FishScriptableObject> fishesCatched = new List<FishScriptableObject>();
    private Camera _mainCam;
    public Camera MainCam => _mainCam;
    #endregion

    private Rigidbody _rb;
    private int actualFishes = 0;
    public float DistanceToFish => distanceToFish;
    public event Action<FishPond> OnPondAssign;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    protected override void Start()
    {
        //fishInventory = new Dictionary<string,int>();
        _mainCam = Camera.main;
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
        //HudManager.Instance.PierShop.OnSell += AddMoney;
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



    private void AddMoney(int moneyToAdd)
    {
        if(actualFishes != 0)
        {
            actualMoney += moneyToAdd;
            HudManager.Instance.PierShop.UpdateMoneyCount(actualMoney);
        }
    }

    private void RemoveFishes(IStorable itemToRemove)
    {
        actualFishes -= 1;
        HudManager.Instance.FishCounter.UpdateFishesCount(actualFishes, maxFishes);
        HudManager.Instance.FishCounter.RemoveFishes(1);
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
    #region InventoryControll
    public void AddToInventory(IStorable itemToAdd)
    {
        HudManager.Instance.Inventory.AddItemToInventory(itemToAdd);
        AddFishes();
    }
    private void AddFishes()
    {
        actualFishes++;
        
        HudManager.Instance.FishCounter.UpdateFishesCount(actualFishes, maxFishes);
        HudManager.Instance.FishCounter.AddFishes(1);
    }
    #endregion
}
