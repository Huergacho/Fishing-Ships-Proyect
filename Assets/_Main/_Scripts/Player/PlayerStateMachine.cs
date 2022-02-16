using System.Collections;
using UnityEngine;
using System;
public class PlayerStateMachine : MonoBehaviour
{
    private FSM<PlayerStates> _fsm;
    private PlayerModel _playerModel;
    private PlayerInputs _playerInputs;
    public event Action onMove;
    //public event Action onNormalSailing;
    //public event Action onFastSailing;
    public event Action onIdle;
    public event Action onInteract;
    public event Action onMenue;
    public event Action<FishPond> onFish;
    public event Action<Vector3> onMovePointer;
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
        onMovePointer?.Invoke(_playerInputs.UpdateMousePosition());
    }
    private void OnMenueCommand()
    {
        onMenue?.Invoke();
    }
    private void OnFishingCommand()
    {
        onFish?.Invoke(_playerInputs.DetectFishPond(_playerInputs.UpdateMousePosition(), _playerModel.DistanceToFish));
        _fsm.Transition(PlayerStates.Idle);
    }
    private void OnIdleCommand()
    {
        onIdle?.Invoke();
        onMovePointer?.Invoke(_playerInputs.UpdateMousePosition());
    }
    private void FsmInit()
    {
        var idle = new PlayerIdleState<PlayerStates>(OnIdleCommand, PlayerStates.Sailing,PlayerStates.Fishing,PlayerStates.OnMenue ,_playerInputs);
        var sail = new PlayerSailState<PlayerStates>(OnMoveCommand, PlayerStates.Idle,PlayerStates.Fishing, PlayerStates.OnMenue, _playerInputs);
        var fish = new PlayerFishingState<PlayerStates>(OnFishingCommand, PlayerStates.Sailing,PlayerStates.Idle, _playerInputs);
        var menue = new PlayerOnMenueState<PlayerStates>(OnMenueCommand, PlayerStates.Idle, _playerInputs);
        //idle 
        idle.AddTransition(PlayerStates.Sailing, sail);
        idle.AddTransition(PlayerStates.Fishing, fish);
        idle.AddTransition(PlayerStates.OnMenue, menue);
        //sail
        sail.AddTransition(PlayerStates.Idle, idle);
        sail.AddTransition(PlayerStates.Fishing, fish);
        sail.AddTransition(PlayerStates.OnMenue, menue);

        //Fishing
        fish.AddTransition(PlayerStates.Sailing,sail);
        fish.AddTransition(PlayerStates.Idle, idle);

        //Menue
        menue.AddTransition(PlayerStates.Idle, idle);

        _fsm = new FSM<PlayerStates>(idle);
    }
    private void Update()
    {
        _fsm.UpdateState();
    }
}