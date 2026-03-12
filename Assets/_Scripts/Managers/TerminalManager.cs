using UnityEngine;
using System;
using System.Collections.Generic;

public class TerminalManager : Singleton<TerminalManager>
{
    [SerializeField] private List<Terminal> _terminals;
    [SerializeField] private MachineStatus _machineStatus;
    [SerializeField] private bool _isLeaverUp;
    [SerializeField] private TerminalEventSO _terminalEvent;


    protected override void Awake() 
    {
        base.Awake();
        _terminals = new List<Terminal>();
        _isLeaverUp = true;
        Terminal.OnTerminalStart += AddTerminal;
        _terminalEvent.OnRaise += ChangeStatus;
    }

	private void OnDisable()
	{
        _terminalEvent.OnRaise -= ChangeStatus;
		Terminal.OnTerminalStart -= AddTerminal;
        _terminals.Clear();
	}

	private void AddTerminal(Terminal terminal) 
    {
        _terminals.Add(terminal);
    }

    private void ChangeStatus (ButtonType buttonType, TerminalType terminalType)
    {
        if (terminalType == TerminalType.Leaver) 
        {
            _isLeaverUp = !_isLeaverUp;
        }

        if (!_isLeaverUp)
        { 
            _machineStatus = MachineStatus.Warning;
            return; 
        }

        switch (_machineStatus) 
        {
            case MachineStatus.Warning:
                break;

            case MachineStatus.Running:
                break;

            case MachineStatus.Off:
                break;

        }


		Debug.Log($"ButtonType pressed: {buttonType}, TerminalType: {terminalType}, Machine Status: {_machineStatus}");
	}

}

public enum MachineStatus 
{
    Running,
    Off,
    Warning
}
