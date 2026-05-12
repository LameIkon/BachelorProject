using UnityEngine;

public class RunningState : BaseState
{
    public RunningState(TerminalStateMachine manager, AudioSource audioSource, AudioPlayerSO audioPlayer) : base(manager, audioSource, audioPlayer) {}

    public override void OnEnter()
    {
        manager.TurnOnConveyor();
        audioPlayer.PlaySound(audioSource);
        manager.TryCompleteQuest(QuestID.StartMachine);
        manager.SendState(TerminalState.Running);
    }

    public override void OnExit()
    {
        manager.StartCoroutine(audioPlayer.Fadeout());
    }

    public override bool HandleInput(ButtonType button, TerminalType terminal)
    {
        if (terminal != TerminalType.Start) return false;

        switch (button)
        {
            case ButtonType.Stop:
                manager.SetState(TerminalState.Off);
                break;

            case ButtonType.SpeedUp:
                manager.TryCompleteQuest(QuestID.IncreaseSpeed);
                return TryChangeSpeed(true);

            case ButtonType.SpeedDown:
                manager.TryCompleteQuest(QuestID.DecreaseSpeed);
                return TryChangeSpeed(false);

            default:
                return false;
        }

        return true;
    }

    private bool TryChangeSpeed(bool up)
    {
        float currentSpeed = manager.GetSpeed();

        float amount = up ? manager.AdjustSpeedAmount.Max : manager.AdjustSpeedAmount.Min;

        float newSpeed = Mathf.Clamp(currentSpeed + amount, manager.MachineSpeeds.Min, manager.MachineSpeeds.Max);

        if (Mathf.Approximately(currentSpeed, newSpeed)) return false;

        manager.SetSpeed(newSpeed);

        if (up)
        {
            manager.TryCompleteQuest(QuestID.IncreaseSpeed);
        }
        else
        {
            manager.TryCompleteQuest(QuestID.DecreaseSpeed);
        }

        return true;
    }
}
