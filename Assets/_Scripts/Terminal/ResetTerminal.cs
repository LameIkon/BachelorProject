using System;
using UnityEngine;

public class ResetTerminal : Terminal
{
	[Header("Buttons")]
	[SerializeField] private InteractableEntity _resetButton;
	[SerializeField] private InteractableEntity _transportStop;

	[Header("Lights")]
	[SerializeField] private ButtonLight _fejlKædetilspænding;
	[SerializeField] private ButtonLight _fejlUdladning;
	[SerializeField] private ButtonLight _oliemangel;

    protected override void OnEnable()
    {
		_resetButton.raiseModuleComunicator += ResetButtonListener;
		_transportStop.raiseModuleComunicator += TransportButtonListener;
    }

    protected override void OnDisable()
    {
 		_resetButton.raiseModuleComunicator -= ResetButtonListener;
		_transportStop.raiseModuleComunicator -= TransportButtonListener;       
    }

    protected override void Start()
	{
		base.Start();
		_terminalType = TerminalType.Reset1;
	}

	private void ResetButtonListener(InteractionSignal signal)
	{
		if (_currentTerminalState != TerminalState.Warning) return;

		if (signal.InteractionAction == InteractionSignalType.ButtonPress)
		{
			Debug.Log("Reset button interaction signal");
			_fejlUdladning.TurnLight(false);
		}
	}

	private void TransportButtonListener(InteractionSignal signal)
	{
		if (signal.InteractionAction == InteractionSignalType.PressButtonIn)
		{
			Debug.Log("Press in");

			return;
		}
		if (signal.InteractionAction == InteractionSignalType.PressButtonOut)
		{
			Debug.Log("Press out");
			return;
		}
	}



}
