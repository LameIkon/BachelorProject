
using UnityEngine;

/// <summary>
/// Methods to use: OnEnter, OnExit, OnFixedUpdate, and OnUpdate. use Public Override void "Name" to get it. 
/// All are set to optional and can be used depending on the need  
/// </summary>
public abstract class BaseState : IState
{
    protected readonly TerminalStateMachine manager;
    protected readonly AudioSource audioSource;
    protected readonly AudioPlayerSO audioPlayer;

    protected BaseState(TerminalStateMachine manager, AudioSource audioSource, AudioPlayerSO audioPlayer)
    {
        this.manager = manager;
        this.audioSource = audioSource;
        this.audioPlayer = audioPlayer;
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

    public abstract bool HandleInput(ButtonType button, TerminalType terminal);
}
