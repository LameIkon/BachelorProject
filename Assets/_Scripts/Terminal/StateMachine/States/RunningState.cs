public class RunningState : BaseState
{
    public RunningState(TerminalStateMachine manager) : base(manager){}

    public override void OnEnter()
    {
        manager.machineStatus = MachineStatus.Running;
    }

    public override void HandleInput(ButtonType button, TerminalType terminal)
    {
        if (terminal != TerminalType.Main) return;

        switch (button)
        {
            case ButtonType.Stop:
                manager.SetState(new OffState(manager));
                manager.SetSpeed(0);
                manager.SetOvenState(OvenStatus.Stop);
                break;

            case ButtonType.SpeedUp:
                manager.ChangeSpeed(0.01f);
                break;

            case ButtonType.SpeedDown:
                manager.ChangeSpeed(-0.01f);
                break;
        }
    }
}
