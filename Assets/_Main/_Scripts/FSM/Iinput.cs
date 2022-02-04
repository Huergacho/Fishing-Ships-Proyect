using System;
using UnityEngine;
public interface Iinput
{
    float GetH { get; }

    float GetV { get; }

    bool IsMoving();

    void UpdateInputs();
}