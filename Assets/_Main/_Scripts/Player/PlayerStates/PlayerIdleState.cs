using System;
using System.Collections.Generic;
using UnityEngine;
class PlayerIdleState<T> : State<T>
{
    private T _moveInput;
    private T _interactInput;
    private T _onMenueInput;
    private PlayerInputs _playerInputs;
    private Action _onIdle;
    public PlayerIdleState(Action onIdle,T moveInput,T interactInput, T onMenueInput, PlayerInputs playerInputs)
    {
        _onIdle = onIdle;
        _onMenueInput = onMenueInput;
        _moveInput = moveInput;
        _interactInput = interactInput;
        _playerInputs = playerInputs;
    }

    public override void Execute()
    {
        if (_playerInputs.isInteracting())
        {
            _parentFsm.Transition(_interactInput);
            return;
        }
        if (_playerInputs.IsMoving())
        {
            _parentFsm.Transition(_moveInput);
            return;
        }
        if (_playerInputs.isOnMenue)
        {
            _parentFsm.Transition(_onMenueInput);
            return;
        }
        _onIdle?.Invoke();

    }
}
