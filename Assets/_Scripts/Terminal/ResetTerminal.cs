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


    protected override void Start()
	{
		base.Start();
		_terminalType = TerminalType.Reset1;
	}


    protected override void Something(Quest quest)
    {
        foreach (TerminalAndButton tb in quest.TerminalBehavior)
        {
            if (tb.TType == TerminalType.End)
            {

            }
        }
    }
}
