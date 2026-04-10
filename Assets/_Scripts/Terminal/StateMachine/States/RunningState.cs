public class RunningState : BaseState
{
    public RunningState(TerminalStateMachine manager) : base(manager){}

    public override void OnEnter()
    {
        manager.TurnOnConveyor();
        manager.TryCompleteQuest(QuestID.StartMachine);
    }

    public override void HandleInput(ButtonType button, TerminalType terminal)
    {
        if (terminal != TerminalType.Main) return;

        switch (button)
        {
            case ButtonType.Stop:
                manager.SetState(TerminalState.Off);
                break;

            case ButtonType.SpeedUp:
                manager.ChangeSpeed(true);
                manager.TryCompleteQuest(QuestID.IncreaseSpeed);
                break;

            case ButtonType.SpeedDown:
                manager.ChangeSpeed(false);
                manager.TryCompleteQuest(QuestID.DecreaseSpeed);
                break;
        }
    }
}
