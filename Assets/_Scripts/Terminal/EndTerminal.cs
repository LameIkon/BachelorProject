using UnityEngine;

public class EndTerminal : Terminal
{

	protected override void Start()
	{
		base.Start();
		_terminalType = TerminalType.End;
	}




}
