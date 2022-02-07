using System.Collections;
using UnityEngine;
using System;
public class PlayerModel : BaseActor
{
    [SerializeField] private Transform mouseIndicator;
    public Transform MouseIndicator => mouseIndicator;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    protected override void Start()
    {
        base.Start();
    }
    public void SuscribeEvents(PlayerStateMachine controller)
    {
        controller.onMove += Move;
        //controller.onInteract += onInteract;
        controller.onIdle += Idle;
    }
    public void Idle()
    {
    }
    public void Move()
    {
        MoveAtMousePos();
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
    #endregion

}
