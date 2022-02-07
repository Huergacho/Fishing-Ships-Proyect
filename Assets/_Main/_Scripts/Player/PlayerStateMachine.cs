using System.Collections;
using UnityEngine;
using System;
public class PlayerStateMachine : MonoBehaviour
{
    private FSM<PlayerStates> _fsm;
    private PlayerModel _playerModel;
    private PlayerInputs _playerInputs;
    public event Action onMove;
    public event Action onIdle;
    public event Action onInteract;
    public void Awake()
    {
        _playerModel = GetComponent<PlayerModel>();
        _playerInputs = GetComponent<PlayerInputs>();
        FsmInit();
    }
    public void Start()
    {
        _playerModel.SuscribeEvents(this);
    }
    private void OnMoveCommand()
    {
        onMove?.Invoke();
        Debug.Log("MovingShip");
    }
    private void OnIdleCommand()
    {
        Debug.Log("Idle"); 
            onIdle?.Invoke();
    }
    private void FsmInit()
    {
        //var idle = new playeridlestate<playerstates>(onidlecommand, playerstates.sailing, playerstates.interact, _playerinputs);
        //var sail = new playersailstate<playerstates>(onmovecommand, playerstates.idle, playerstates.interact, _playerinputs);
        var idle = new PlayerIdleState<PlayerStates>(OnIdleCommand, PlayerStates.Sailing ,_playerInputs);
        var sail = new PlayerSailState<PlayerStates>(OnMoveCommand, PlayerStates.Idle, _playerInputs);

        //idle 
        idle.AddTransition(PlayerStates.Sailing, sail);

        //sail
        sail.AddTransition(PlayerStates.Idle, idle);

        _fsm = new FSM<PlayerStates>(idle);
    }
    private void Update()
    {
        _fsm.UpdateState();
    }
}