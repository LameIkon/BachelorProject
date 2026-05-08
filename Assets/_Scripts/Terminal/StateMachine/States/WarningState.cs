using UnityEngine;

public class WarningState : BaseState
{
    private bool _isResetTerminalPressed;
    private readonly ButtonLight _resetLight;

    public WarningState(TerminalStateMachine manager, AudioSource audioSource, AudioPlayerSO audioPlayer, ButtonLight buttonLight) : base(manager, audioSource, audioPlayer)
    {
        _resetLight = buttonLight;
    }


    public override void OnEnter()
    {
        _resetLight.TurnLight(true);
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

            _resetLight.TurnLight(false);
            _isResetTerminalPressed = true;
            return true;
        }
        if (terminal == TerminalType.Start && button == ButtonType.Reset && _isResetTerminalPressed) 
        {
            manager.SetState(TerminalState.Off);
            return true;
        }

        return false;

    }
}