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
        print("Boton de mov");
        return Input.GetMouseButtonDown(1);

    }
    public bool isInteracting()
    {
        return Input.GetMouseButtonDown(0);
    }
    public void UpdateInputs()
    {
        IsMoving();
        isInteracting();
    }
}