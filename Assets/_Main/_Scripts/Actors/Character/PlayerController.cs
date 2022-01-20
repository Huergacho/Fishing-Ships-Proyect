using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : BaseActor
{
    [SerializeField] private Transform mouseIndicator;
    [SerializeField] private LayerMask pointerContactLayers;
    [SerializeField] private LayerMask fishLayerMask;
    private Vector3 target;
    [SerializeField] private List<FishScriptableObject> fished = new List<FishScriptableObject>();
    [SerializeField]private float rotationSpeed;
    private Vector3 targetPoint;
    public static PlayerController instance;
    public Action<FishPond> OnPondDetection;
    [SerializeField] private float distanceToFish;
    private bool wantToInteract;
    private void Awake()
    {
        instance = this;
    }

    protected override void Start()
    {
        base.Start();
        InputController.inputControllerInstance.pointEvent += MoveListener;
        InputController.inputControllerInstance.interactEvent += DetectFishPond;
        GameManager.instance.player = this;
    }
    void Update()
    {
        MoveAtMousePos();
        MoveMouseIndicator();
        UpdateMousePosition();
        if (wantToInteract)
        {
            DetectFishPond();
        }
    }
    #region MousePosition
    Ray CalculateMousePos()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    void UpdateMousePosition()
    {
        
        if(Physics.Raycast(CalculateMousePos(), out RaycastHit hitInfo,Mathf.Infinity,pointerContactLayers))
        {
            target = hitInfo.point;
            target.y = transform.position.y;
        }
    }
    #endregion
    #region Movement
    void MoveMouseIndicator()
    {
        mouseIndicator.position = target;  
    }
    private void SmoothRotation(Vector3 target)
    {
        var direction = (target - transform.position);
        if(direction != Vector3.zero)
        {
        var rotDestiny = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotDestiny,rotationSpeed * Time.deltaTime);
        }
    }
    private void MoveAtMousePos()
    {
        var distance = target - transform.position;
        if (distance.magnitude < 0.05)
        {
            canMove = false;
        }
        if (canMove)
        {    
            SmoothRotation(targetPoint);
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
        }
    }
    #endregion
    public void AddToInventory(FishScriptableObject fishToAdd)
    {
        fished.Add(fishToAdd);
    }
    private void MoveListener()
    {
        canMove = true;
        targetPoint = target;
    }
    private void DetectFishPond()
    {

            var hit = Physics.SphereCastAll(mouseIndicator.position, 10f, mouseIndicator.position, 3f, fishLayerMask);
            var distance = Vector3.Distance(transform.position, mouseIndicator.position);
            if (distance <= distanceToFish)
             {
            wantToInteract = false;
                foreach (var item in hit)
                {
                    var fishItem = item.collider.gameObject.GetComponent<FishPond>();
                    OnPondDetection(fishItem);
                    return;
                }
            }
            else
            {
                wantToInteract = true;
            MoveAtMousePos();
            }
    }
}
