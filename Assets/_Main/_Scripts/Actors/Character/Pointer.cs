using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        InputController.inputControllerInstance.pointEvent += Point;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Point()
    {
        animator.Play("PointAnimation");
    }
}
