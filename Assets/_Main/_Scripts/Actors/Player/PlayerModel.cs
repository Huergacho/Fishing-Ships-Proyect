using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
public class PlayerModel : BaseActor
{
    #region SerializeFields

    [SerializeField] private Transform mouseIndicator;
    [SerializeField] private float distanceToFish;
    [SerializeField]private int maxFishes;
    [SerializeField] private float boostFVIncrease;
    [SerializeField] private float fishSkill;
    [SerializeField] private Inventory _inventory;
    private NavMeshAgent navMeshAgent;
    public Inventory PlayerInventory => _inventory;
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
        navMeshAgent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
        pointerAnimator = mouseIndicator.gameObject.GetComponent<Animator>();
    }

    protected override void Start()
    {
        _mainCam = Camera.main;
        GameManager.instance.player = this;
        base.Start();
    }
    public void SuscribeEvents(PlayerController controller)
    {
        controller.onMove += Move;
        controller.onIdle += Idle;
        controller.onFish += Fish;
        controller.onMovePointer += MovePointer;

    }
    
    #region Movement

    public void Idle()
    {
        _rb.velocity = Vector3.zero;
    }
    public void Move(Vector3 destiny)
    {
        MoveAtMousePos(destiny);
    }
    public void PlayIndicator()
    {
        pointerAnimator.Play("PointAnimation");
    }
    #endregion

    #region Mouse Movement Calculation

    //private void SmoothRotation(Vector3 dest)
    //{
    //    var direction = (dest - transform.position);
    //    if (direction != Vector3.zero)
    //    {
    //        var rotDestiny = Quaternion.LookRotation(direction);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, rotDestiny, actorStats.RotSpeed * Time.deltaTime);
    //    }
    //}

    private void MoveAtMousePos(Vector3 dest)
    {
        navMeshAgent.SetDestination(dest);
        navMeshAgent.speed = speed;

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
}
