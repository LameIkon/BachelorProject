using UnityEngine;

public class LeverWarningState : BaseState
{
	public LeverWarningState(TerminalStateMachine manager) : base(manager) { }


	public override void HandleInput(ButtonType button, TerminalType terminal)
	{
		if (terminal == TerminalType.Lever)
		{
			manager.SetState(manager.WarningState);
		}
	}

	public override void OnEnter()
	{
		manager.TurnOffConveyor();
	}


}
