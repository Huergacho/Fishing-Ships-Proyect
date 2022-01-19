using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private Transform mouseIndicator;
    private Vector3 target;
    [SerializeField] private List<FishScriptableObject> fished = new List<FishScriptableObject>();

    [SerializeField]private float rotationSpeed;
    [SerializeField] private float radius;
    private bool canMove;
    private Vector3 targetPoint;

    void Start()
    {
        InputController.inputControllerInstance.pointEvent += MoveListener;
        GameManager.instance.player = this;
    }
    void Update()
    {
        MoveAtMousePos();
        MoveMouseIndicator();
        UpdateMousePosition();
    }
    #region MousePosition
    Ray CalculateMousePos()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    void UpdateMousePosition()
    {
        
        if(Physics.Raycast(CalculateMousePos(), out RaycastHit hitInfo,Mathf.Infinity))
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
}
