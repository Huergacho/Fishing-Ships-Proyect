using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private LayerMask contactLayers;
    void Start()
    {
        animator = GetComponent<Animator>();
        //InputController.OnPoint += Point;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMousePosition();  
    }
    Ray CalculateMousePos()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    void UpdateMousePosition()
    {

        if (Physics.Raycast(CalculateMousePos(), out RaycastHit hitInfo, Mathf.Infinity, contactLayers))
        {
            transform.position = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
        }
    }
    void Point()
    {
        animator.Play("PointAnimation");
    }
}
