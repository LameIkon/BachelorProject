using UnityEngine;

public class LeverWarningState : BaseState
{
	private readonly ButtonLight _resetLight;
	private readonly ButtonLight _leverLight;
	public LeverWarningState(TerminalStateMachine manager, AudioSource audioSource, AudioPlayerSO audioPlayer, ButtonLight resetLight, ButtonLight leverLight) : base(manager, audioSource, audioPlayer)
	{
		_resetLight = resetLight;
		_leverLight = leverLight;
	}


	public override bool HandleInput(ButtonType button, TerminalType terminal)
	{
		if (terminal != TerminalType.Lever) return false;
	
		manager.SetState(TerminalState.Warning);
		return true;
		
	}

	public override void OnEnter()
	{
		_resetLight.TurnLight(true);
		_leverLight.TurnLight(true);
		manager.TurnOffConveyor();
		manager.SendState(TerminalState.LeverWarning);
	}

	public override void OnExit()
	{
		_leverLight.TurnLight(false);
		manager.TryCompleteQuest(QuestID.RemoveLeverWarning);
	}


}
