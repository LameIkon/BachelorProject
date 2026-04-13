public interface IState
{
    void OnEnter();
    void OnUpdate();
    void OnFixedUpdate();
    void OnExit();

    bool HandleInput(ButtonType button, TerminalType terminal);
}
