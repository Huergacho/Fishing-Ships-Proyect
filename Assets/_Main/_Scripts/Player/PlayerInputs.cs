using System.Collections;
using UnityEngine;

public class PlayerInputs : MonoBehaviour, Iinput
{
    private float _h;

    private float _v;

    public float GetH => _h;

    public float GetV => _v;

    public bool IsMoving()
    {
        
        return Input.GetMouseButton(1);

    }
    public bool isInteracting()
    {
        return Input.GetMouseButton(0);
    }
    public void UpdateInputs()
    {
        Debug.Log(IsMoving());
        IsMoving();
        isInteracting();
    }
}