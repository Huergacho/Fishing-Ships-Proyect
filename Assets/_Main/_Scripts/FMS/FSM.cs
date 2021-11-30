using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T>
{
    private IState<T> _currentState;
    public IState<T> CurrentState => _currentState;

    public FSM(IState<T> initialState)
    {
        SetInitialState(initialState);
    }
    private void SetInitialState(IState<T> initState)
    {
        _currentState = initState;
        _currentState.Awake();
        _currentState.parentFSM = this;
    }
    public void UpdateState()
    {
        if(_currentState != null)
        {
            _currentState.Execute();
        }
    }
    public void Transition(T input)
    {
        IState<T> newState = _currentState.GetTransition(input);
        if(CurrentState!= null)
        {
            _currentState.Sleep();
            _currentState = newState;
            _currentState.parentFSM = this;
            _currentState.Awake();
        }
    }


}
