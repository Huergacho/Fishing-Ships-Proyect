using System.Collections;
using UnityEngine;
using System;
public class PlayerSailState<T> : State<T>
{
    private Action _onMove;
    private T _idleInput;
    private T _interactInput;
    private T _onMenueInput;
    private PlayerInputs _playerInputs;
    public PlayerSailState(Action onMove, T idleInput, T interactInput,T onMenueInput, PlayerInputs playerInputs)
    {
        _onMove = onMove;
        _idleInput = idleInput;
        _interactInput = interactInput;
        _onMenueInput = onMenueInput;
        _playerInputs = playerInputs;
    }
    public override void Execute()
    {
        if (!_playerInputs.IsMoving())
        {
            _parentFsm.Transition(_idleInput);
            return;
        }
        if (_playerInputs.isInteracting())
        {
            _parentFsm.Transition(_interactInput);
            return;
        }
        if (_playerInputs.isOnMenue)
        {
            _parentFsm.Transition(_onMenueInput);
            return;
        }

        _onMove?.Invoke();
    }
}
