using UnityEngine;
using System;
using System.Collections.Generic;

public class TerminalManager : Singleton<TerminalManager>
{
    [SerializeField] private List<Terminal> _terminals;
    [SerializeField] private MachineStatus _machineStatus;
    private bool _isLeaverUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake() 
    {
        base.Awake();
        _terminals = new List<Terminal>();
        Terminal.OnTerminalStart += AddTerminal;
        Terminal.OnTerminalButtonPress += ChangeStatus;
    }

	private void OnDisable()
	{
        Terminal.OnTerminalButtonPress -= ChangeStatus;
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
            _machineStatus = MachineStatus.Warning;
        }
        if (!_isLeaverUp) return;

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
