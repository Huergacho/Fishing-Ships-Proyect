using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerModel : BaseActor
{
    [SerializeField] private Transform mouseIndicator;
    [SerializeField] private float distanceToFish;
    [SerializeField] private List<FishScriptableObject> fishes = new List<FishScriptableObject>();
    public float DistanceToFish => distanceToFish;
    private Rigidbody _rb;
    private FishPond actualFishItem;
    public event Action<FishPond> OnPondAssign;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    protected override void Start()
    {
        GameManager.instance.player = this;
        base.Start();
    }
    public void SuscribeEvents(PlayerStateMachine controller)
    {
        controller.onMove += Move;
        controller.onIdle += Idle;
        controller.onFish += Fish;
        controller.onMovePointer += MovePointer;
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
    }
    #endregion

}
