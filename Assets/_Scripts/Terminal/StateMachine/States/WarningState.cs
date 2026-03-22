using UnityEngine;

public class WarningState : BaseState
{
    public WarningState(TerminalStateMachine manager) : base(manager){}

    public override void OnEnter()
    {
        Debug.Log("Entered Warning State");
        manager.machineStatus = MachineStatus.Warning;
        manager.SetSpeed(0);
        manager.SetOvenState(OvenStatus.Stop);
    }

    public override void HandleInput(ButtonType button, TerminalType terminal)
    {
        Debug.Log("Try fix issue");
        if (terminal == TerminalType.Reset1 && button == ButtonType.Reset && manager.IsLeverUp)
        {
            manager.SetState(manager.OffState);
        }
    }
}