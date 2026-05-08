using System.Collections.Generic;
using UnityEngine;

public class EmergencyWarningState : BaseState
{
	private readonly ButtonLight _resetLight;
	private readonly ButtonLight _emergencyLightLever; 
	private readonly ButtonLight _emergencyLightReset;
	private readonly ButtonLight _emergencyLightEnd;

	private readonly Dictionary<TerminalType, ButtonLight> _issues;

	//private readonly ButtonLight _leverLight;
	public EmergencyWarningState(TerminalStateMachine manager, AudioSource audioSource, 
		AudioPlayerSO audioPlayer, ButtonLight resetLight, ButtonLight frontLightEmerg, 
		ButtonLight endLightEmerg, ButtonLight resetLightEmerg) : base(manager, audioSource, audioPlayer)
	{
		_resetLight = resetLight;
		_emergencyLightEnd = endLightEmerg;
		_emergencyLightLever = frontLightEmerg;
		_emergencyLightReset = resetLightEmerg;

		_issues = new Dictionary<TerminalType, ButtonLight>()
		{
			{ TerminalType.Reset1, _emergencyLightReset },
			{ TerminalType.Lever, _emergencyLightLever},
			{ TerminalType.End, _emergencyLightEnd },

		};
	}


	public override bool HandleInput(ButtonType button, TerminalType terminal)
	{
		if (button != ButtonType.Emergency) return false;

		Something(terminal);

		//if (terminal != TerminalType.Lever) return false;
		//manager.SetState(TerminalState.Warning);
		return true;
		
	}

	public override void OnEnter()
	{
		_resetLight.TurnLight(true);
		//_leverLight.TurnLight(true);
		manager.TurnOffConveyor();
		manager.SendState(TerminalState.LeverWarning);
	}

	public override void OnExit()
	{
		//_leverLight.TurnLight(false);
		manager.TryCompleteQuest(QuestID.RemoveLeverWarning);
	}

	private void Something(TerminalType type)
	{
		if (_issues.ContainsKey(type))
		{
			 _issues[type].ToggleLight(); // Flip the bool
		}

	}
}