using UnityEngine;

public class LeverTerminal : Terminal
{
	[Header("Lever")]
	[SerializeField] private InteractableEntity _lever;

	protected override void Start()
	{
		base.Start();
		_terminalType = TerminalType.Lever;
	}

}
