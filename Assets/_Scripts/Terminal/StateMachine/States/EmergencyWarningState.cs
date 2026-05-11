using System.Collections.Generic;
using UnityEngine;

public class EmergencyWarningState : BaseState
{
	private readonly ButtonLight _resetLight;
	private readonly ButtonLight _emergencyLightLever; 
	private readonly ButtonLight _emergencyLightReset;
	private readonly ButtonLight _emergencyLightEnd;

	private readonly Dictionary<TerminalType, bool> _issues;

	//private readonly ButtonLight _leverLight;
	public EmergencyWarningState(TerminalStateMachine manager, AudioSource audioSource, 
		AudioPlayerSO audioPlayer, ButtonLight resetLight, ButtonLight frontLightEmerg, 
		ButtonLight endLightEmerg, ButtonLight resetLightEmerg) : base(manager, audioSource, audioPlayer)
	{
		_resetLight = resetLight;
		_emergencyLightEnd = endLightEmerg;
		_emergencyLightLever = frontLightEmerg;
		_emergencyLightReset = resetLightEmerg;

		_issues = new Dictionary<TerminalType, bool>()
		{
			{ TerminalType.Reset1, false },
			{ TerminalType.Lever, false },
			{ TerminalType.End, false },

		};
	}


	public override bool HandleInput(ButtonType button, TerminalType terminal)
	{
		if (button != ButtonType.Emergency) return false;

		ChangeIssue(terminal);

		Debug.Log($"Emergency: {terminal}");

		//if (terminal != TerminalType.Lever) return false;

		foreach (TerminalType type in _issues.Keys) 
		{
			if (_issues[type] == true) return false; // Check if there are any issues.
		}

		Debug.Log("Emergency State: Issues fixed");
		manager.SetState(TerminalState.Warning);
		return true;
		
	}

	public override void OnEnter()
	{
		Debug.Log($"Enter Emergency State");
		_resetLight.TurnLight(true);
		//_leverLight.TurnLight(true);
		manager.TurnOffConveyor();
		manager.SendState(TerminalState.EmergencyWarning);
	}

	public override void OnExit()
	{
		//_leverLight.TurnLight(false);
		manager.TryCompleteQuest(QuestID.RemoveLeverWarning);
	}

	private void ChangeIssue(TerminalType type)
	{
		_issues[type] = !_issues[type]; // Flip the bool

		foreach (TerminalType t in _issues.Keys)
		{
			Debug.Log($"Emergency Waring: Terminal: {t}, Issue {_issues[t]}");
		}

		if (type == TerminalType.Lever) _emergencyLightLever.ToggleLight();

	}
}