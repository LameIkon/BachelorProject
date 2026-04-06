using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TerminalStateMachine : MonoBehaviour
{
    [SerializeField] private TerminalEventSO _terminalEvent;
    [SerializeField] private TerminalStartEventSO _terminalStartEvent;
    [SerializeField] private OvenStateChangeEventSO _ovenstateChangeEvent;
    [SerializeField] private QuestCompleteEventSO _questCompleteEvent;

    [SerializeField] private List<Terminal> _terminals;
    //[SerializeField] private MachineStatus _machineStatus;

    private StateMachine _stateMachine;

    [SerializeField] private float _machineSpeed = 0f;


    // Terminal States
    public RunningState RunningState { get; private set; }
    public OffState OffState { get; private set; }
    public WarningState WarningState { get; private set; }
    public LeverWarningState LeverWarningState { get; private set; }

    private void Awake() 
    {
        //base.Awake();
        _terminals = new List<Terminal>();
        //_isResetTermialInWarning = false;
        CreateStateMachine();
    }

    private void Start()
    {
        SetState(OffState);
    }

	private void OnEnable()
	{
        _terminalStartEvent.OnRaise += AddTerminal;
        _terminalEvent.OnRaise += ChangeStatus;
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
        if (terminalType == TerminalType.Lever)
        {
            if (_stateMachine.CurrentState != LeverWarningState)
            {
                SetState(LeverWarningState);
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
        LeverWarningState = new LeverWarningState(this);
    }


    public void SetState(BaseState newState)
    {
        _stateMachine.SetState(newState);
    }

	/// <summary>
	/// Changes the speed up and down, though a boolean. <c>True</c> turns the speed up by one and <c>false</c> turns it down by one.
	/// </summary>
	/// <param name="up"><c>True</c> for up and <c>false</c> for down</param>
	public void ChangeSpeed(bool up)
    {
        float amount = up ? 0.01f : -0.01f;

        _machineSpeed = Mathf.Clamp(_machineSpeed + amount, 0f, 0.25f);
        Debug.Log($"Machine Speed: {_machineSpeed}");
        _ovenstateChangeEvent.Raise(_machineSpeed);
    }

    public void TurnOnConveyor()
    {
        _ovenstateChangeEvent.Raise(_machineSpeed);
    }

    public void TurnOffConveyor() 
    {
        _ovenstateChangeEvent.Raise(0);
    }


    public float GetSpeed() => _machineSpeed;

    public void TryCompleteQuest(QuestID questID) 
    {
        _questCompleteEvent.Raise(questID);
    }
    #endregion 

}
