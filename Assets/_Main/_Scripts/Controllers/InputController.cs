using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InputController : MonoBehaviour
{
    private bool _isMoving;
    private bool _isInteracting;
    [SerializeField] private Transform mouseIndicator;
    [SerializeField] private LayerMask pointerContactLayers;
    //public event Action<bool> isMoving;
    private void Start()
    {

    }
    private void Update()
    {
        ClickActions();
    }
    void ClickActions()
    {
        if (Input.GetMouseButton(1))
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            _isInteracting = true;
        }
        else
        {
            _isInteracting = false;
        }
    }


    Ray CalculateMousePos()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    Vector3 UpdateMousePosition()
    {

        if (Physics.Raycast(CalculateMousePos(), out RaycastHit hitInfo, Mathf.Infinity, pointerContactLayers))
        {
            mouseIndicator.position = hitInfo.point;
            mouseIndicator.position = Vector3.up * transform.position.y;
            return mouseIndicator.position;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
