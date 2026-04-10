using UnityEngine;

public class LeverWarningState : BaseState
{
	public LeverWarningState(TerminalStateMachine manager) : base(manager) { }


	public override void HandleInput(ButtonType button, TerminalType terminal)
	{
		if (terminal == TerminalType.Lever)
		{
			manager.SetState(TerminalState.Warning);
		}
	}

	public override void OnEnter()
	{
		manager.TurnOffConveyor();
	}


}
