using System.Collections;
using UnityEngine;
using System;

public class PlayerFishingState<T> : State<T> 
{
    private Action _onFish;
    private T _movingInput;
    private T _idleInput;
    private PlayerInputs _playerInputs;
    public PlayerFishingState(Action onFish, T movingInput, T idleInput, PlayerInputs playerInputs)
    {
        _onFish = onFish;
        _movingInput = movingInput;
        _idleInput = idleInput;
        _playerInputs = playerInputs;
    }
    public override void Execute()
    {
        if (_playerInputs.IsMoving())
        {
            _parentFsm.Transition(_movingInput);
            return;
        }
        if (!_playerInputs.isInteracting())
        {
            _parentFsm.Transition(_idleInput);
        }
        _onFish.Invoke();
    }
}
