using System.Collections;
using UnityEngine;
using System;
public class PlayerSailState<T> : State<T>
{
    private Action _onMove;
    private T _idleInput;
    private T _interactInput;
    private PlayerInputs _playerInputs;
    //public PlayerSailState(Action onMove, T idleInput, T interactInput,PlayerInputs playerInputs)
    public PlayerSailState(Action onMove, T idleInput, PlayerInputs playerInputs)
    {
        _onMove = onMove;
        _idleInput = idleInput;
      //  _interactInput = interactInput;
        _playerInputs = playerInputs;
    }
    public override void Awake()
    {
        Debug.Log("SAILING");
    }
    public override void Execute()
    {
        //_playerInputs.UpdateInputs();
        Debug.Log("Sail State"+_playerInputs.IsMoving());
        if (!_playerInputs.IsMoving())
        {
            _parentFsm.Transition(_idleInput);
            return;
        }
        //if (_playerInputs.isInteracting())
        //{
        //    _parentFsm.Transition(_interactInput);
        //    return;
        //}

        _onMove?.Invoke();
    }
}
