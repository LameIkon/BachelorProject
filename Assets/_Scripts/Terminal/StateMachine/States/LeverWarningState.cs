using UnityEngine;

public class LeverWarningState : BaseState
{
	public LeverWarningState(TerminalStateMachine manager, AudioSource audioSource, AudioPlayerSO audioPlayer) : base(manager, audioSource, audioPlayer) { }


	public override bool HandleInput(ButtonType button, TerminalType terminal)
	{
		if (terminal != TerminalType.Lever) return false;
	
		manager.SetState(TerminalState.Warning);
		return true;
		
	}

	public override void OnEnter()
	{
		manager.TurnOffConveyor();
		manager.SendState(TerminalState.LeverWarning);
	}

	public override void OnExit()
	{
		manager.TryCompleteQuest(QuestID.RemoveLeverWarning);
	}


}
