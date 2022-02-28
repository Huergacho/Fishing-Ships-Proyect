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
    [SerializeField] private float fishSkill;
    public float FishSkill => fishSkill;
    private Camera _mainCam;
    public Camera MainCam => _mainCam;
    #endregion

    private Rigidbody _rb;
    private Animator pointerAnimator;
    public float DistanceToFish => distanceToFish;
    public event Action<FishPond> OnPondAssign;
    public event Action<int, int> UpdateFishCount;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        pointerAnimator = mouseIndicator.gameObject.GetComponent<Animator>();
    }

    protected override void Start()
    {
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
        HudManager.Instance.Inventory.onSelledItem += OnSell;
    }

    private void InitializeHud()
    {
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
    public void PlayIndicator()
    {
        pointerAnimator.Play("PointAnimation");
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
        PlayIndicator();
        if (fishPond != null)
        {
            OnPondAssign?.Invoke(fishPond);
        }
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

    private void OnSell(int value)
    {
        AddMoney(value);
    }
    public void AddToInventory(ItemScriptableObject itemToAdd)
    {
        HudManager.Instance.Inventory.AddItemToInventory(itemToAdd);
        HudManager.Instance.RewardObtainText.SetObtainedItem(itemToAdd.ItemName);
    }
    private void AddMoney(int moneyToAdd)
    {
        actualMoney += moneyToAdd;
        HudManager.Instance.PierShop.UpdateMoneyCount(actualMoney);
    }

    
    #endregion
}
