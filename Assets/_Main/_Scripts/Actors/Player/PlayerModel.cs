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
        //var dest = destiny.normalized;
        //print(dest);
        //destiny *= speed;
        //destiny.y= _rb.velocity.y;
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
        //SmoothRotation(new Vector3(dest.x, 0, dest.z));
        //var distance = Vector3.Distance(transform.position, dest);


        ////if (distance >= 0.5f)
        ////{
        ////    //_rb.velocity = Vector3.MoveTowards(_rb.position, destiny, speed * Time.deltaTime);
        ////    //_rb.velocity =  dest;
        ////    _rb.position = Vector3.MoveTowards(_rb.position, new Vector3(dest.x, 0, dest.z), speed * Time.deltaTime);
        ////    //_rb.velocity = new Vector3(_rb.velocity.x * dest.x, 0, _rb.velocity.z * dest.z) * speed * Time.deltaTime;
        ////    //transform.position = Vector3.MoveTowards(transform.position, new Vector3(dest.x, 0, dest.z), speed * Time.deltaTime);  //Funcionando
        ////}


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
