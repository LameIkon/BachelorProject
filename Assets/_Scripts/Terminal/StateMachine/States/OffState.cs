using UnityEngine;

public class OffState : BaseState
{
    public OffState(TerminalStateMachine manager) : base(manager){}

    public override void OnEnter()
    {
        manager.TurnOffConveyor();
        manager.TryCompleteQuest(QuestID.StopMachine);
        manager.SendState(TerminalState.Off);
    }

    public override bool HandleInput(ButtonType button, TerminalType terminal)
    {
        if (terminal == TerminalType.Main && button == ButtonType.Start) 
        {
            manager.SetState(TerminalState.Running);
            return true;
        }
        return false;

    }

    public override void OnExit()
    {
        Debug.Log("Exited off State");
    }
}