using UnityEngine;

public abstract class Terminal : MonoBehaviour
{
    [Header("Terminal Events")]
    [SerializeField] private TerminalEventSO _onTerminalEvent;
    [SerializeField] private TerminalStartEventSO _onTerminalStartEvent;
    [SerializeField] private ButtonEventSO _onButtonEvent;
    [SerializeField] protected TerminalStateEventSO _onTerminalStateEvent;
    [SerializeField] protected QuestGiveEventSO _onQuestGiveEvent;

    protected TerminalType _terminalType;


	#region Unity Method
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	protected virtual void Start()
    {
        _onTerminalStartEvent.Raise(this);
    }

    protected virtual void OnEnable()
    {
        _onButtonEvent.OnRaise += ChangeStatus;
        _onQuestGiveEvent.OnRaise += Something;
    }

    protected virtual void OnDisable()
    {
        _onButtonEvent.OnRaise -= ChangeStatus;
        _onQuestGiveEvent.OnRaise += Something;
    }


    protected abstract void Something(Quest quest);


    #endregion

    /// <summary>
    /// Changes the status of the terminal.
    /// </summary>
    /// <param name="type">The type of button that was pressed.</param>
    private void ChangeStatus(ButtonType type)
    {
        _onTerminalEvent.Raise(type, _terminalType);
    }
}



