public class RunningState : BaseState
{
    public RunningState(TerminalStateMachine manager) : base(manager){}

    public override void OnEnter()
    {
        manager.TurnOnConveyor();
    }

    public override void HandleInput(ButtonType button, TerminalType terminal)
    {
        if (terminal != TerminalType.Main) return;

        switch (button)
        {
            case ButtonType.Stop:
                manager.SetState(manager.OffState);
                break;

            case ButtonType.SpeedUp:
                manager.ChangeSpeed(true);
                break;

            case ButtonType.SpeedDown:
                manager.ChangeSpeed(false);
                break;
        }
    }
}
