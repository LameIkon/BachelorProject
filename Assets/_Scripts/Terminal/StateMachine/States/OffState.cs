using UnityEngine;

public class OffState : BaseState
{
    public OffState(TerminalStateMachine manager) : base(manager){}

    public override void OnEnter()
    {
        Debug.Log("Entered off State");
        manager.machineStatus = MachineStatus.Off;
        manager.SetOvenState(OvenStatus.Stop);
    }

    public override void HandleInput(ButtonType button, TerminalType terminal)
    {
        if (terminal != TerminalType.Main) return;

        if (button == ButtonType.Start)
        {
            manager.SetState(manager.RunningState);
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exited off State");
    }
}