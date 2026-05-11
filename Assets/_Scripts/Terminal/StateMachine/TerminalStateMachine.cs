using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TerminalStateMachine : Singleton<TerminalStateMachine>
{
    [Header("Events")]
	[SerializeField] private StoreDataEventSO _storeDataEvent;
    [SerializeField] private TerminalEventSO _terminalEvent;
    [SerializeField] private TerminalStartEventSO _terminalStartEvent;
    [SerializeField] private OvenStateChangeEventSO _ovenstateChangeEvent;
    [SerializeField] private QuestCompleteEventSO _questCompleteEvent;
    [SerializeField] private QuestGiveEventSO _questGiveEvent;
    [SerializeField] private TerminalStateEventSO _terminalStateEvent;

    [Header("Something")]
    [SerializeField] private List<Terminal> _terminals;
    private AudioSource _audioSource;

    [Header("Audios")]
    [SerializeField] private AudioPlayerSO _offStateAudioPlayer;
    [SerializeField] private AudioPlayerSO _runningStateAudioPlayer;
    [SerializeField] private AudioPlayerSO _warningStateAudioPlayer;
    [SerializeField] private AudioPlayerSO _leverWarningStateAudioPlayer;


    //[SerializeField] private MachineStatus _machineStatus;
    [Header("Lights")]
    [SerializeField] private ButtonLight _resetLight;
    [SerializeField] private ButtonLight _leverLight;
    [SerializeField] private ButtonLight _emergencyLightLever; 
	[SerializeField] private ButtonLight _emergencyLightReset;
	[SerializeField] private ButtonLight _emergencyLightEnd;


    private StateMachine _stateMachine;
    private TerminalState _currentState;


    [SerializeField] private float _machineSpeed = 0f;
    [SerializeField, RangedFloat(0,100f)] private RangedFloat _machineSpeeds;
    [SerializeField, RangedFloat(-100f,100f)] private RangedFloat _adjustSpeedAmount;

    // Issues tracking
    private bool HasIssue => _activeIssues.Count > 0;
    private readonly HashSet<MachineIssue> _activeIssues = new();
    private readonly HashSet<TerminalType> _activeEmergencyButtons = new();
    private Dictionary<TerminalType, ButtonLight> _emergencyIssues;

    // Terminal States
    public RunningState RunningState { get; private set; }
    public OffState OffState { get; private set; }
    public WarningState WarningState { get; private set; }
    public LeverWarningState LeverWarningState { get; private set; }
    public EmergencyWarningState EmergencyWarningState { get; private set; }

    protected override void Awake() 
    {
        base.Awake();

        _audioSource = GetComponent<AudioSource>();
        _terminals = new List<Terminal>();

        CreateStateMachine();

        _emergencyIssues = new Dictionary<TerminalType, ButtonLight>()
		{
			{ TerminalType.Reset1, _emergencyLightReset },
			{ TerminalType.Lever, _emergencyLightLever},
			{ TerminalType.End, _emergencyLightEnd },
		};

    }

	private void OnEnable()
	{
        _terminalStartEvent.OnRaise += AddTerminal;
        _terminalEvent.OnRaise += ChangeStatus;
        _questGiveEvent.OnRaise += SetLevelState;
	}

	private void OnDisable()
	{
        _terminalEvent.OnRaise -= ChangeStatus;
		_terminalStartEvent.OnRaise -= AddTerminal;
        _questGiveEvent.OnRaise -= SetLevelState;
        _terminals.Clear();
	}

	private void AddTerminal(Terminal terminal) 
    {
        _terminals.Add(terminal);
    }

    private void ChangeStatus(ButtonType buttonType, TerminalType terminalType)
    {
        bool success = ProcessInput(buttonType, terminalType);

        InteractionEvent context = new InteractionEvent
        {
            eventType = EventType.Button,
            buttonType = buttonType,
            buttonAction = success ? ButtonOutcome.Success : ButtonOutcome.Fail
        };

        _storeDataEvent.Raise(context);
    }

    private bool ProcessInput(ButtonType buttonType, TerminalType terminalType)
    {
        Debug.Log("process Input");
        if (buttonType == ButtonType.Lever)
        { 
            ToggleLever();
            return true;
        }
        if (buttonType == ButtonType.Emergency)
        {
            ToggleEmergency(terminalType);
            return true;
        }
        if (HasIssue) return false;
        return _stateMachine.HandleInput(buttonType, terminalType);
    }

    #region Issues
    private void ToggleLever()
    {   
        Debug.Log("Toggle Lever");
        if (_activeIssues.Contains(MachineIssue.Lever))
        {
            _activeIssues.Remove(MachineIssue.Lever);
        }
        else
        {
            _activeIssues.Add(MachineIssue.Lever);
        }
        
        RefreshIssues();

        //if (_stateMachine.CurrentState != LeverWarningState)
        //{
        //    SetState(TerminalState.LeverWarning);
        //    return false; 
        //}
        //return true;
    }

    private void ToggleEmergency(TerminalType terminal)
    {
        // First handle emergency list by toggling
        if (_activeEmergencyButtons.Contains(terminal))
        {
            _activeEmergencyButtons.Remove(terminal);
        }
        else
        {
            _activeEmergencyButtons.Add(terminal);
        }

        // Lastly check if we have any ongoing emergency
        if (_activeEmergencyButtons.Count > 0)
        {
            _activeIssues.Add(MachineIssue.Emergency);
        }
        else
        {
            _activeIssues.Remove(MachineIssue.Emergency);
        }

        RefreshIssues();
    }

    private void RefreshIssues()
    {
        if (HasIssue)
        {
            //_resetLight.TurnLight(true);
            SetState(TerminalState.Warning);
        }

        // Toggle light for lever
        _leverLight.TurnLight(_activeIssues.Contains(MachineIssue.Lever));

        // Toggle light for emergency buttons
        foreach (var (terminal, light) in _emergencyIssues)
        {
            light?.TurnLight(_activeEmergencyButtons.Contains(terminal));
        }

        
    }
    #endregion

    #region State Machine
    private void CreateStateMachine()
    {
        _stateMachine = new StateMachine();

        RunningState = new RunningState(this, _audioSource, _runningStateAudioPlayer);
        OffState = new OffState(this, _audioSource, _offStateAudioPlayer);
        WarningState = new WarningState(this, _audioSource, _warningStateAudioPlayer, _resetLight);
        //LeverWarningState = new LeverWarningState(this, _audioSource, _leverWarningStateAudioPlayer, _resetLight, _leverLight);
        //EmergencyWarningState = new EmergencyWarningState(this, _audioSource, _warningStateAudioPlayer, _resetLight, _emergencyLightLever, _emergencyLightEnd, _emergencyLightReset);

    }

    public void SetState(TerminalState newState)
    {
        BaseState stateSwitch = null;

        //if (newState == _currentState) return;

        switch (newState) 
        {
            case TerminalState.Off:
                stateSwitch = OffState;
                break;
            case TerminalState.Running:
                stateSwitch = RunningState;
                break;
            case TerminalState.Warning:
                stateSwitch = WarningState;
                break;
            case TerminalState.LeverWarning:
                stateSwitch = LeverWarningState;
                break;
            case TerminalState.EmergencyWarning:
                stateSwitch = EmergencyWarningState;
                break;
        }

        InteractionEvent context = new InteractionEvent
        {
            eventType = EventType.Terminal,
            terminalState = newState,
        };

        _currentState = newState;

        _storeDataEvent.Raise(context);
        _stateMachine.SetState(stateSwitch);
    }


    private void SetLevelState(Quest quest) 
    {
        Debug.Log($"Try set state: {quest.MachineState}");
        SetState(quest.MachineState);
    }


	/// <summary>
	/// Changes the speed up and down, though a boolean. <c>True</c> turns the speed up by one and <c>false</c> turns it down by one.
	/// </summary>
	/// <param name="up"><c>True</c> for up and <c>false</c> for down</param>
	public void ChangeSpeed(bool up)
    {
        float amount = up ? _adjustSpeedAmount.Max : _adjustSpeedAmount.Min;

        _machineSpeed = Mathf.Clamp(_machineSpeed + amount, _machineSpeeds.Min, _machineSpeeds.Max);
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

    public void SendState(TerminalState state) 
    {
        _terminalStateEvent.Raise(state);
    }

    #endregion 

    private enum MachineIssue
    {
        Lever,
        Emergency
    }

}
