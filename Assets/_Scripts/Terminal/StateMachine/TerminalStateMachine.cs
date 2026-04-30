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

    private StateMachine _stateMachine;

    [SerializeField] private float _machineSpeed = 0f;


    // Terminal States
    public RunningState RunningState { get; private set; }
    public OffState OffState { get; private set; }
    public WarningState WarningState { get; private set; }
    public LeverWarningState LeverWarningState { get; private set; }

    protected override void Awake() 
    {
        base.Awake();

        _audioSource = GetComponent<AudioSource>();
        _terminals = new List<Terminal>();

        CreateStateMachine();

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
        bool shouldContinue = true;
        if (terminalType == TerminalType.Lever) { shouldContinue = HandleLever(); }

        if (!shouldContinue) 
        {
            return true;
        }

        return _stateMachine.HandleInput(buttonType, terminalType);
    }

    private bool HandleLever()
    {
        if (_stateMachine.CurrentState != LeverWarningState)
        {
            SetState(TerminalState.LeverWarning);
            return false; 
        }
        return true;
    }


    # region State Machine
    private void CreateStateMachine()
    {
        _stateMachine = new StateMachine();

        RunningState = new RunningState(this, _audioSource, _runningStateAudioPlayer);
        OffState = new OffState(this, _audioSource, _offStateAudioPlayer);
        WarningState = new WarningState(this, _audioSource, _warningStateAudioPlayer);
        LeverWarningState = new LeverWarningState(this, _audioSource, _leverWarningStateAudioPlayer);
    }

    public void SetState(TerminalState newState)
    {
        BaseState stateSwitch = null;
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
        }

        InteractionEvent context = new InteractionEvent
        {
            eventType = EventType.Terminal,
            terminalState = newState,
        };

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

    public void SendState(TerminalState state) 
    {
        _terminalStateEvent.Raise(state);
    }

    #endregion 

}
