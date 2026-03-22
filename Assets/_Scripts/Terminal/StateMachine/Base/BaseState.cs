/// <summary>
/// Methods to use: OnEnter, OnExit, OnFixedUpdate, and OnUpdate. use Public Override void "Name" to get it. 
/// All are set to optional and can be used depending on the need  
/// </summary>
public abstract class BaseState : IState
{
    protected TerminalStateMachine manager;

    protected BaseState(TerminalStateMachine manager)
    {
        this.manager = manager;
    }


    public virtual void OnEnter()
    {
        // Not implemented
    }

    public virtual void OnExit()
    {
        // Not implemented
    }

    public virtual void OnUpdate()
    {
        // Not implemented
    }
    public virtual void OnFixedUpdate()
    {
        // Not implemented
    }

    public abstract void HandleInput(ButtonType button, TerminalType terminal);
}
