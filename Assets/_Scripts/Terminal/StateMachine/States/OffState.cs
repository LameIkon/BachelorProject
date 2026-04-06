using UnityEngine;

public class OffState : BaseState
{
    public OffState(TerminalStateMachine manager) : base(manager){}

    public override void OnEnter()
    {
        manager.TurnOffConveyor();
        manager.TryCompleteQuest(QuestID.StopMachine);
    }

    public override void HandleInput(ButtonType button, TerminalType terminal)
    {
        if (terminal == TerminalType.Main && button == ButtonType.Start) 
        {
            manager.SetState(manager.RunningState);
        }

    }

    public override void OnExit()
    {
        Debug.Log("Exited off State");
    }
}