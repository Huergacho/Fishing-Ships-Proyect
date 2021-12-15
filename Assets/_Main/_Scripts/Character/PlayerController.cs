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

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.player = this;
        mouseIndicator.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveWithMouseInput();
        MoveMouseIndicator();
    }
    Ray CalculateMousePos()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    void LookAtMousePosition()
    {
        
        if(Physics.Raycast(CalculateMousePos(), out RaycastHit hitInfo))
        {
            target = hitInfo.point;
            target.y = transform.position.y;
            var distance = target - transform.position;
            if(distance.magnitude >= 0.05)
            {
                //transform.LookAt(mouseIndicator);
                SmoothRotation(target);
            }
        }
    }
    void MoveWithMouseInput()
    {
        LookAtMousePosition();

        if (Input.GetMouseButton(1))
        {
            var distance = target - transform.position;
            if (distance.magnitude >= 0.05)
                transform.position += transform.forward *  speed * Time.deltaTime;
        }
    }
    void MoveMouseIndicator()
    {
        var clampedTarget = Vector3.ClampMagnitude(target, radius);
        mouseIndicator.position = clampedTarget;
        
       
    }
   public void AddToInventory(FishScriptableObject fishToAdd)
    {
        fished.Add(fishToAdd);
    }
    private void SmoothRotation(Vector3 target)
    {
        var direction = (target - transform.position);
        var rotDestiny = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotDestiny,rotationSpeed * Time.deltaTime);
    }

}
