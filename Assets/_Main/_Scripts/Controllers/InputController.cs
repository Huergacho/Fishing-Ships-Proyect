using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InputController : MonoBehaviour
{
    public Action pointEvent;
    public Action interactEvent;
    public static InputController inputControllerInstance;
    private void Awake()
    {
        if (inputControllerInstance == null)
        {
            inputControllerInstance = this;
        }
        else
        {
            Destroy(inputControllerInstance.gameObject);
            inputControllerInstance = this;
        }
    }
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
            pointEvent.Invoke();
        }
        if (Input.GetMouseButtonDown(0))
        {
            interactEvent.Invoke();
        }
    }
}
