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
    [SerializeField] private Transform pointer;
    private bool canMove;
    private Vector3 targetPoint;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.player = this;
        pointer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MoveAtMousePos();
        MoveMouseIndicator();
        UpdateMousePosition();
    }
    Ray CalculateMousePos()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    void UpdateMousePosition()
    {
        
        if(Physics.Raycast(CalculateMousePos(), out RaycastHit hitInfo))
        {
            target = hitInfo.point;
            target.y = transform.position.y;
        }
    }
    void MoveMouseIndicator()
    {
        mouseIndicator.position = target;  
    }
   public void AddToInventory(FishScriptableObject fishToAdd)
    {
        fished.Add(fishToAdd);
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
        if (Input.GetMouseButton(1))
        {
            PointMovePosition();
            canMove = true;
            targetPoint = target;
        }
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
    private void PointMovePosition()
    {
        pointer.position = targetPoint;
        pointer.gameObject.SetActive(true);
        StartCoroutine(RippleEffect());

    }
    IEnumerator RippleEffect()
    {
        yield return new WaitForSeconds(3f);
        pointer.gameObject.SetActive(false);

    }
}
