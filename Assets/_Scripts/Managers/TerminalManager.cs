using UnityEngine;
using System;
using System.Collections.Generic;

public class TerminalManager : Singleton<TerminalManager>
{
    [SerializeField] private TerminalEventSO _terminalEvent;
    [SerializeField] private TerminalStartEventSO _terminalStartEvent;
    [SerializeField] private OvenStateChangeEventSO _ovenstateChangeEvent;

    [SerializeField] private List<Terminal> _terminals;
    [SerializeField] private MachineStatus _machineStatus;
    [SerializeField] private bool _isLeaverUp;
    [SerializeField] private bool _isResetTermialInWarning;

    [SerializeField] private float _machineSpeed;


    protected override void Awake() 
    {
        base.Awake();
        _terminals = new List<Terminal>();
        _isLeaverUp = true;
        _isResetTermialInWarning = false;
        _terminalStartEvent.OnRaise += AddTerminal;
        _terminalEvent.OnRaise += ChangeStatus;
    }

	private void OnDisable()
	{
        _terminalEvent.OnRaise -= ChangeStatus;
		_terminalStartEvent.OnRaise -= AddTerminal;
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
            _machineSpeed = 0;
            _ovenstateChangeEvent.Raise(OvenStatus.Stop);
            _isResetTermialInWarning = true;
            return; 
        }

        if (terminalType == TerminalType.Reset1) 
        {
            _isResetTermialInWarning = false;
        }

        if (_isResetTermialInWarning) return;

        if (terminalType != TerminalType.Main) return;
        switch (_machineStatus) 
        {
            case MachineStatus.Warning:
                if (buttonType == ButtonType.Reset)
                {
                    _ovenstateChangeEvent.Raise(OvenStatus.Stop);
                    _machineStatus = MachineStatus.Off;
                }
                break;

            case MachineStatus.Running:
                if (buttonType == ButtonType.Stop)
                {
                    _machineStatus = MachineStatus.Off;
                    _ovenstateChangeEvent.Raise(OvenStatus.Stop);

                }
                if (buttonType == ButtonType.SpeedDown)
                {
                    //_machineSpeed--;
                    //_machineSpeed -= 0.1f; 
                    _ovenstateChangeEvent.Raise(OvenStatus.Decrease);
                }
                if (buttonType == ButtonType.SpeedUp)
                {
                    _machineSpeed++;
                    //_machineSpeed -= 0.1f; 
                    _ovenstateChangeEvent.Raise(OvenStatus.Increase);
                }
                break;

            case MachineStatus.Off:
                if (buttonType == ButtonType.Start)
                    _machineStatus = MachineStatus.Running;
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


public enum OvenStatus
{
    Stop,
    Increase,
    Decrease
}