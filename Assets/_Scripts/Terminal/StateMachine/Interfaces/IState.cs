public interface IState
{
    void OnEnter();
    void OnUpdate();
    void OnFixedUpdate();
    void OnExit();

    void HandleInput(ButtonType button, TerminalType terminal);
}
