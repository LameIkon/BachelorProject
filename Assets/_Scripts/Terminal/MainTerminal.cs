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
		WriteMessage("Machine is off");
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

    private void WriteTerminalError(TerminalStateData terminalData) 
	{
		string textToWrite = string.Empty;

		switch (terminalData.state)
		{
			case TerminalState.Warning:
				textToWrite = $"Warning\n";

				if (terminalData.leverIssue != string.Empty)
				{
					textToWrite += $"{terminalData.leverIssue}\n";
				}
				if (terminalData.EmergencyIssue != string.Empty)
				{
					textToWrite += $"{terminalData.EmergencyIssue}\n";
				}
				break;
			case TerminalState.Off:
				textToWrite = "Machine is off";
				break;
			case TerminalState.Running:
				textToWrite = "Machine is running";
				break;
			default:
				textToWrite = "This should never be reached";
				break;
		}
		WriteMessage(textToWrite);

	}

	private void WriteMessage(string message) 
	{
		_gui.text = string.Concat("Main terminal: \n" ,message);
		Debug.Log($"{this}: {message}");
	}

}
