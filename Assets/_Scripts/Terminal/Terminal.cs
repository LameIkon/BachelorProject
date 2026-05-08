using UnityEngine;

public class Terminal : MonoBehaviour
{
    [Header("Terminal Events")]
    [SerializeField] private TerminalEventSO _onTerminalEvent;
    [SerializeField] private TerminalStartEventSO _onTerminalStartEvent;
    [SerializeField] private ButtonEventSO _onButtonEvent;
    [SerializeField] protected TerminalStateEventSO _terminalStateEvent;


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
    }

    protected virtual void OnDisable()
    {
        _onButtonEvent.OnRaise -= ChangeStatus;
    }


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



