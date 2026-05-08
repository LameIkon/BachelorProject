using UnityEngine;
using TMPro;

public class MainTerminal : Terminal
{

	private TextMeshProUGUI _gui;

	protected override void Start()
	{
		base.Start();
		_terminalType = TerminalType.Main;
		_gui = GetComponentInChildren<TextMeshProUGUI>();
		WriteMessage(string.Empty);
		Debug.Log($"{this}: Is on");
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		_onTerminalStateEvent.OnRaise += WriteTerminalError;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		_onTerminalStateEvent.OnRaise -= WriteTerminalError;
	}


	private void WriteTerminalError(TerminalState terminalState) 
	{
		switch (terminalState) 
		{
			case TerminalState.LeverWarning:
				WriteMessage("Lever Warning");
				break;
			case TerminalState.Warning:
				WriteMessage("Warning");
				break;
			case TerminalState.Off:
				WriteMessage("Machine is off");
				break;
			default:
				WriteMessage(string.Empty);
				break;
		
		}
	
	}

	private void WriteMessage(string message) 
	{
		_gui.text = string.Concat("Main terminal: \n" ,message);
		Debug.Log($"{this}: {message}");
	}

}
