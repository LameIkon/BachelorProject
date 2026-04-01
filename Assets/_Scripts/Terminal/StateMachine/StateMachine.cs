using UnityEngine;
public class StateMachine
{
    public IState CurrentState { get; private set; }

    public void SetState(IState newState)
    {
        CurrentState?.OnExit();
        Debug.Log($"Exit State: {CurrentState}");
        CurrentState = newState;
        CurrentState?.OnEnter();
        Debug.Log($"Enter State: {CurrentState}");
    }

    public void HandleInput(ButtonType buttonType, TerminalType terminalType)
    {
        Debug.Log($"Input Termnials {buttonType}, {terminalType}");
        CurrentState?.HandleInput(buttonType, terminalType);
    }

    public void Update() // Not implemented
    {
        CurrentState?.OnUpdate();
    }

    public void FixedUpdate() // Not Implemented
    {
        CurrentState?.OnFixedUpdate();
    }
}
