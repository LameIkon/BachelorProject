using UnityEngine;

public class ResetTerminal : Terminal
{
	
	[Header("Buttons")]
	[SerializeField] private InteractableEntity _resetButton;
	[SerializeField] private InteractableEntity _transportStop;
	[SerializeField] private InteractableEntity _flipUpDown;

	[Header("Lights")]
	[SerializeField] private ButtonLight _fejlKædetilspænding;
	[SerializeField] private ButtonLight _fejlUdladning;
	[SerializeField] private ButtonLight _oliemangel;


	protected override void Start()
	{
		base.Start();
		_terminalType = TerminalType.Reset1;
	}


}
