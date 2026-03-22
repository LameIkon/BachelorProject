public class StateMachine
{
    public IState CurrentState { get; private set; }

    public void SetState(IState newState)
    {
        CurrentState?.OnExit();
        CurrentState = newState;
        CurrentState?.OnEnter();
    }

    public void HandleInput(ButtonType buttonType, TerminalType terminalType)
    {
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
