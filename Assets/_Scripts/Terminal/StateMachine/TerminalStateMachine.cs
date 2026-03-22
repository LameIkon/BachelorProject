using System.Collections.Generic;
using UnityEngine;

public class TerminalStateMachine : MonoBehaviour
{
    [SerializeField] private TerminalEventSO _terminalEvent;
    [SerializeField] private TerminalStartEventSO _terminalStartEvent;
    [SerializeField] private OvenStateChangeEventSO _ovenstateChangeEvent;

    [SerializeField] private List<Terminal> _terminals;
    //[SerializeField] private MachineStatus _machineStatus;
    [SerializeField] private bool _isLeaverUp;
    [SerializeField] private bool _isResetTermialInWarning;

    private StateMachine _stateMachine;

    [SerializeField] private float _machineSpeed = 0f;


    // Terminal States
    public MachineStatus machineStatus; // Just to show in inspector the state
    public bool IsLeverUp => _isLeaverUp;
    public RunningState RunningState { get; private set; }
    public OffState OffState { get; private set; }
    public WarningState WarningState { get; private set; }

    private void Awake() 
    {
        //base.Awake();
        _terminals = new List<Terminal>();
        _isLeaverUp = true;
        //_isResetTermialInWarning = false;
        //_terminalStartEvent.OnRaise += AddTerminal;
        //_terminalEvent.OnRaise += ChangeStatus;

        CreateStateMachine();
    }

    private void Start()
    {
        SetState(OffState);
    }

    private void OnDisable()
	{
        _terminalEvent.OnRaise -= ChangeStatus;
		_terminalStartEvent.OnRaise -= AddTerminal;
        _terminals.Clear();
	}

	private void AddTerminal(Terminal terminal) 
    {
        _terminals.Add(terminal);
    }

    private void ChangeStatus(ButtonType buttonType, TerminalType terminalType)
    {
        if (terminalType == TerminalType.Leaver)
        {
            _isLeaverUp = !_isLeaverUp;

            if (!_isLeaverUp)
            {
                SetState(WarningState);
                return;
            }
        }

        _stateMachine.HandleInput(buttonType, terminalType);
    }

    # region State Machine
    private void CreateStateMachine()
    {
        _stateMachine = new StateMachine();

        RunningState = new RunningState(this);
        OffState = new OffState(this);
        WarningState = new WarningState(this);
    }


    public void SetState(BaseState newState)
    {
        _stateMachine.SetState(newState);
    }

    public void ChangeSpeed(float amount)
    {
        _machineSpeed = Mathf.Clamp(_machineSpeed + amount, 0f, 0.25f);
        _ovenstateChangeEvent.Raise(amount > 0 ? OvenStatus.Increase : OvenStatus.Decrease);
    }

    public void SetSpeed(float value)
    {
        _machineSpeed = value;
    }

    public void SetOvenState(OvenStatus status)
    {
        _ovenstateChangeEvent.Raise(status);
    }

    public float GetSpeed() => _machineSpeed;
    #endregion 

}
