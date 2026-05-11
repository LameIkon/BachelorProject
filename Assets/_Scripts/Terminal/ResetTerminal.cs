using System;
using UnityEngine;

public class ResetTerminal : Terminal
{
	[Header("Buttons")]
	[SerializeField] private InteractableEntity _resetButton;
	[SerializeField] private InteractableEntity _transportStopEntity;

	[Header("Lights")]
	[SerializeField] private ButtonLight _fejlKædetilspænding;
	[SerializeField] private ButtonLight _fejlUdladning;
	[SerializeField] private ButtonLight _oliemangel;


    private WorldToggleButton _emergencyButton;


    protected override void Start()
	{
		base.Start();
        GetButtonType();
		_terminalType = TerminalType.Reset1;
	}


    protected override void SetBehaviour(Quest quest)
    {
        foreach (TerminalAndButton tb in quest.TerminalBehavior) 
        {
            if (tb.TType == TerminalType.Reset1 && tb.BType == ButtonType.Emergency) 
            {
                _emergencyButton.Interact(null);
            }
        }
    }

    private void GetButtonType()
    {
        if (_transportStopEntity.InteractionAction is WorldToggleButton worldToggleButton)
        {
            _emergencyButton = worldToggleButton;
        }
    }
}
