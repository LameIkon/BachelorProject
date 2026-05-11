using UnityEngine;

public class StartTerminal : Terminal
{

	[Header("Buttons")]
	[SerializeField] private InteractableEntity _startButton;
	[SerializeField] private InteractableEntity _stopButton;
	[SerializeField] private InteractableEntity _resetButton;
	[SerializeField] private InteractableEntity _speedUpButton;
	[SerializeField] private InteractableEntity _speedDownButton;

	protected override void Start()
	{
		base.Start();
		_terminalType = TerminalType.Start;
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
