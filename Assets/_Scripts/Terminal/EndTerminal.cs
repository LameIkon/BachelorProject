using UnityEngine;

public class EndTerminal : Terminal
{

    [SerializeField] private InteractableEntity _emergencyStop;


    protected override void Start()
	{
		base.Start();
		_terminalType = TerminalType.End;
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

