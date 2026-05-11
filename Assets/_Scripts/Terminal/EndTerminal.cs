using UnityEngine;

public class EndTerminal : Terminal
{

    [SerializeField] private InteractableEntity _emergencyEntity;

    private WorldToggleButton _emergencyButton;


    protected override void Start()
	{
		base.Start();
        GetButtonType();
		_terminalType = TerminalType.End;
	}

    protected override void SetBehaviour(Quest quest)
    {
        foreach (TerminalAndButton tb in quest.TerminalBehavior) 
        {
            if (tb.TType == TerminalType.End && tb.BType == ButtonType.Emergency) 
            {
                _emergencyButton.Interact(null);
            }
        }
    }

    private void GetButtonType()
    {
        if (_emergencyEntity.InteractionAction is WorldToggleButton worldToggleButton)
        {
            _emergencyButton = worldToggleButton;
        }
    }

}

