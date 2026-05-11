using UnityEngine;

public class LeverTerminal : Terminal
{
	[Header("Lever")]
	[SerializeField] private InteractableEntity _leverEntity;
	[SerializeField] private InteractableEntity _emergencyEntity;

    private WorldToggleButton _emergencyButton;
    private WorldLever _lever;

	protected override void Start()
	{
		base.Start();
        GetButtonType();
		_terminalType = TerminalType.Lever;
	}


    protected override void SetBehaviour(Quest quest)
    {
        foreach (TerminalAndButton tb in quest.TerminalBehavior)
        {
            if (tb.TType == TerminalType.Lever)
            {
                if (tb.BType == ButtonType.Emergency)
                {
                    _emergencyButton.Interact(null);
                    continue;
                }
                if (tb.BType == ButtonType.Lever)
                {
                    _lever.Interact(null);
                }
            }
        }
    }

    private void GetButtonType()
    {
        if (_emergencyEntity.InteractionAction is WorldToggleButton worldToggleButton)
        {
            _emergencyButton = worldToggleButton;
        }
        if (_leverEntity.InteractionAction is WorldLever lever)
        {
            _lever = lever;
        }
    }

}
