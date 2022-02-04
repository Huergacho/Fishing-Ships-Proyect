using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<T>
{
    public FSM<T> parentFsm { get; set; }
    //Se ejecuta cuando arranca el estado
    void Awake();

    //Es el Update del Estado
    void Execute();

    //Cuando se termina el estado, para borrar o modificar variables al transisionar
    void Sleep();

    //Para agregar estados, se necesita un estimulo "Input" y la transision a agregar "IState"
    void AddTransition(T input,IState<T> transitionToAdd);


    void RemoveTransition(IState<T> transitionToRemove);

    IState<T> GetTransition(T input);
}
