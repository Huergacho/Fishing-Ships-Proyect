using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float rotSpeed;
    [SerializeField] private Transform mouseIndicator;
    private Vector3 target;
    [SerializeField] private List<FishScriptableObject> fished = new List<FishScriptableObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.player = this;

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
                transform.LookAt(target);
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
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }
    void MoveWithKeys()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += direction * speed * Time.deltaTime;
    }
    void MoveMouseIndicator()
    {
        mouseIndicator.position = target;
    }
   public void AddToInventory(FishScriptableObject fishToAdd)
    {
        fished.Add(fishToAdd);
    }
}
