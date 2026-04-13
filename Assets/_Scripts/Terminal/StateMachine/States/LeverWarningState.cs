using UnityEngine;

public class LeverWarningState : BaseState
{
	public LeverWarningState(TerminalStateMachine manager) : base(manager) { }


	public override bool HandleInput(ButtonType button, TerminalType terminal)
	{
		if (terminal != TerminalType.Lever) return false;
	
		manager.SetState(TerminalState.Warning);
		return true;
		
	}

	public override void OnEnter()
	{
		manager.TurnOffConveyor();
	}


}
