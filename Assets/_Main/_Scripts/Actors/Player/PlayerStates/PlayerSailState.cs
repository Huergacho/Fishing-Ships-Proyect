using System.Collections;
using UnityEngine;
using System;
public class PlayerSailState<T> : State<T>
{

    private Action<Vector3> _onMove;
    private T _idleInput;
    private T _interactInput;
    private T _onMenueInput;
    private PlayerInputs _playerInputs;
    private Vector3 _wayPoint;
    private Transform _model;
    public PlayerSailState(Action<Vector3> onMove, T idleInput, T interactInput,T onMenueInput, PlayerInputs playerInputs, Transform model)

    {
        _onMove = onMove;
        _model = model;
        _idleInput = idleInput;
        _interactInput = interactInput;
        _onMenueInput = onMenueInput;
        _playerInputs = playerInputs;
    }
    public override void Awake()
    {
        _wayPoint = _playerInputs.UpdateMousePosition();
        
    }
    public override void Execute()
    {
        if (_playerInputs.IsMoving())
        {
            _wayPoint = _playerInputs.UpdateMousePosition();
        }
        if (!IsOnDistace())
        {
            _parentFsm.Transition(_idleInput);
            return;
        }

        //if (!_playerInputs.IsMoving())  //TODO; Decirle que pase a idle cuando llegue a destino.
        //{
        //    _parentFsm.Transition(_idleInput);
        //    return;
        //}

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
        _onMove?.Invoke(_wayPoint);
    }
    bool IsOnDistace()
    {
        var distance = Vector3.Distance(_model.position, _wayPoint);
        if(distance >= 1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
