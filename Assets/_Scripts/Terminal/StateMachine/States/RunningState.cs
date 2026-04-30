using UnityEngine;

public class RunningState : BaseState
{
    public RunningState(TerminalStateMachine manager, AudioSource audioSource, AudioPlayerSO audioPlayer) : base(manager, audioSource, audioPlayer) {}

    public override void OnEnter()
    {
        manager.TurnOnConveyor();
        manager.TryCompleteQuest(QuestID.StartMachine);
        manager.SendState(TerminalState.Running);
    }

    public override bool HandleInput(ButtonType button, TerminalType terminal)
    {
        if (terminal != TerminalType.Main) return false;

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

            default:
                return false;
        }

        return true;
    }
}
