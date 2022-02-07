using System.Collections;
using UnityEngine;
using System;
public class FSM<T>
{
    private IState<T> _currentState;
    public IState<T> CurrentState => _currentState;

    public FSM(IState<T> initialState)
    {
        SetState(initialState);
    }

    private void SetState(IState<T> initState)
    {
        _currentState = initState;
        _currentState.Awake();
        _currentState.parentFsm = this;
    }
    public void UpdateState()
    {
        if(_currentState != null)
        {
        _currentState.Execute();
        }
    }
    public void Transition(T stateToTransit)
    {
        IState<T> newState = _currentState.GetTransition(stateToTransit);
        if(newState != null)
        {
            _currentState.Sleep();
            _currentState = newState;
            _currentState.parentFsm = this;
            _currentState.Awake();
        }
    }
}