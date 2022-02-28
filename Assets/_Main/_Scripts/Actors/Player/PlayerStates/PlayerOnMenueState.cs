using System;
class PlayerOnMenueState<T> : State<T>
{
    private Action _onMenu;
    private T _idleInput;
    private PlayerInputs _playerInputs;
    public PlayerOnMenueState(Action onMenu, T idleInput, PlayerInputs playerInputs)
    {
        _onMenu = onMenu;
        _idleInput = idleInput;
        _playerInputs = playerInputs;
    }
    public override void Execute()
    {
        if (!_playerInputs.isOnMenue)
        {
            _parentFsm.Transition(_idleInput);
            return;
        }
        _onMenu.Invoke();
    }
} 
