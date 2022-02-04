using System;
using System.Collections.Generic;
using UnityEngine;
class PlayerIdleState<T> : State<T>
{
    private T _moveInput;
    private T _interactInput;
    private PlayerInputs _playerInputs;
    private Action _onIdle;
    public PlayerIdleState(Action onIdle,T moveInput, T interactInput,PlayerInputs playerInputs)
    {
        _onIdle = onIdle;
        _moveInput = moveInput;
        _interactInput = interactInput;
        _playerInputs = playerInputs;
    }

    public override void Execute()
    {
        //if (_playerInputs.isInteracting())
        //{
        //    _parentFsm.Transition(_interactInput);
        //    return;
        //}
        _playerInputs.UpdateInputs();
        if (_playerInputs.IsMoving())
        {
            _parentFsm.Transition(_moveInput);
            return;
        }
        _onIdle?.Invoke();

    }
}
