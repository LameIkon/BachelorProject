using UnityEngine;

public class WarningState : BaseState
{
    public WarningState(TerminalStateMachine manager, AudioSource audioSource, AudioPlayerSO audioPlayer) : base(manager, audioSource, audioPlayer) {}

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

    public override bool HandleInput(ButtonType button, TerminalType terminal)
    {
        Debug.Log("Try fix issue");

        if (terminal == TerminalType.Reset1 && button == ButtonType.Reset)
        {
            if (_isResetTerminalPressed) return false; // Nothing new happened

            _isResetTerminalPressed = true;
            return true;
        }
        if (terminal == TerminalType.Main && button == ButtonType.Reset && _isResetTerminalPressed) 
        {
            manager.SetState(TerminalState.Off);
            return true;
        }

        return false;

    }
}