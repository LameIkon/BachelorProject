using UnityEngine;

public class WarningState : BaseState
{
    public WarningState(TerminalStateMachine manager) : base(manager){}

    private bool _isResetTerminalPressed;

    public override void OnEnter()
    {
        manager.TurnOffConveyor();
        _isResetTerminalPressed = false;
        manager.SendState(TerminalState.Warning);
    }

	public override void OnExit()
	{
		manager.TryCompleteQuest(QuestID.RemoveWarning);
	}

    public override void HandleInput(ButtonType button, TerminalType terminal)
    {
        Debug.Log("Try fix issue");

        if (terminal == TerminalType.Reset1 && button == ButtonType.Reset)
        {
            _isResetTerminalPressed = true;
        }
        if (terminal == TerminalType.Main && button == ButtonType.Reset && _isResetTerminalPressed) 
        {
            manager.SetState(manager.OffState);
        }

    }
}