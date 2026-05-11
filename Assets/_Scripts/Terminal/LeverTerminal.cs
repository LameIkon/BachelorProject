using UnityEngine;

public class LeverTerminal : Terminal
{
	[Header("Lever")]
	[SerializeField] private InteractableEntity _lever;
	[SerializeField] private InteractableEntity _emergencyStop;

	protected override void Start()
	{
		base.Start();
		_terminalType = TerminalType.Lever;
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
