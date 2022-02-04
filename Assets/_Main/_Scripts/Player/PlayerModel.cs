using System.Collections;
using UnityEngine;
using System;
public class PlayerModel : BaseActor
{
    [SerializeField] private Transform mouseIndicator;
    public Transform MouseIndicator => mouseIndicator; 
    [SerializeField] private LayerMask pointerContactLayers;
    private Vector3 target;
    private ActorStats _stats;
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
        MoveMousePos();

    }
    public void Move()
    {
        Debug.Log("MOving");
        MoveMousePos();
        MoveAtMousePos();
    }
    #region Mouse Movement Calculation
    Ray CalculateMousePos()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    void UpdateMousePosition()
    {

        if (Physics.Raycast(CalculateMousePos(), out RaycastHit hitInfo, Mathf.Infinity, pointerContactLayers))
        {
            target = hitInfo.point;
            target.y = transform.position.y;
        }
        else
        {
            target = Vector3.zero;
        }
    }
    private void MoveMousePos()
    {
        UpdateMousePosition();
       
        mouseIndicator.position = target;
    }
    private void SmoothRotation(Vector3 target)
    {
        var direction = (target - transform.position);
        if (direction != Vector3.zero)
        {
            var rotDestiny = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotDestiny, _stats.RotSpeed * Time.deltaTime);
        }
    }
    private void MoveAtMousePos()
    {
       
        SmoothRotation(mouseIndicator.position);
        transform.position = Vector3.MoveTowards(transform.position, mouseIndicator.position, speed * Time.deltaTime);

       
    }
    #endregion

}
