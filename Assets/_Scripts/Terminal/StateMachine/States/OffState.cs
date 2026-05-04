using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;

public class OffState : BaseState
{
    // Start Sequence variables
    private readonly WaitForSeconds _waitForSeconds;
    private Coroutine _startSequence;
    private bool _canPress;


    public OffState(TerminalStateMachine manager, AudioSource audioSource, AudioPlayerSO audioPlayer) : base(manager, audioSource, audioPlayer)
    {
        _waitForSeconds = new WaitForSeconds(1);
    }

    public override void OnEnter()
    {
        _startSequence = null;
        _canPress = false;

        manager.TurnOffConveyor();
        manager.TryCompleteQuest(QuestID.StopMachine);
        manager.SendState(TerminalState.Off);
    }

    public override bool HandleInput(ButtonType button, TerminalType terminal)
    {
        if (terminal == TerminalType.Main && button == ButtonType.Start) 
        {
            if (_canPress)
            {
                manager.SetState(TerminalState.Running);
                return true;
            }
            else if (_startSequence == null)
            {
                _startSequence = manager.StartCoroutine(StartSequence());
                return true;
            }
        }
        if (_startSequence != null) 
        {
            manager.StopCoroutine(_startSequence);
            _startSequence = null;
            _canPress = false;
        }
        return false;

    }

    private IEnumerator StartSequence() 
    {
        for(int i = 3; i > 0; i--) 
        {
            Debug.Log($"Start Sequence: {i}, Coroutine: {_startSequence}");
            yield return _waitForSeconds;
        }
        _canPress = true;
		Debug.Log($"Can Start: {_canPress}, Coroutine: {_startSequence}");
		yield return _waitForSeconds;
        yield return _waitForSeconds;
        yield return _waitForSeconds;
        _canPress = false;
		Debug.Log($"Can Start: {_canPress}, Coroutine: {_startSequence}");
        _startSequence = null;
	}


    public override void OnExit()
    {
        Debug.Log("Exited off State");
    }
}