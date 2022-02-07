using System.Collections.Generic;
using UnityEngine;

public class State<T> : IState<T>
{
    protected Dictionary<T, IState<T>> _transitions = new Dictionary<T, IState<T>>();
    protected FSM<T> _parentFsm;
    public FSM<T> parentFsm { get => _parentFsm ; set =>_parentFsm = value; }

    public virtual void Awake()
    {

    }

    public virtual void Execute()
    { 

    }

    public virtual void Sleep()
    {

    }

    public void AddTransition(T input, IState<T> transitionToAdd)
    {
        if (!_transitions.ContainsKey(input))
        {
            _transitions[input] = transitionToAdd;
        }
    }

    public IState<T> GetTransition(T input)
    {
        if (_transitions.ContainsKey(input))
        {
            return _transitions[input];
        }
        else
        {
            return null;
        }
    }

    public void RemoveTransition(IState<T> transitionToRemove)
    {
        if (_transitions.ContainsValue(transitionToRemove))
        {
            foreach (var item in _transitions)
            {
                if(item.Value == transitionToRemove)
                {
                    _transitions.Remove(item.Key);
                }
            }
        }
    }
}