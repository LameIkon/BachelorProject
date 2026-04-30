using UnityEngine;
using System;
using TMPro;
using System.Collections;

public class Terminal : MonoBehaviour
{
    [SerializeField] private TerminalData _data;
    [SerializeField] private TerminalEventSO _onTerminalEvent;
    [SerializeField] private TerminalStartEventSO _onTerminalStartEvent;
    [SerializeField] private ButtonEventSO _onButtonEvent;
    [SerializeField] private TerminalStateEventSO _terminalStateEvent;
    
    public Action<bool> OnSpeedChange;
    private TextMeshProUGUI _terminalScreen;

    // Start Sequence variables
    private WaitForSeconds _waitForSeconds;
    private Coroutine _startSequence;
    private bool _canStart;


	#region Unity Method
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        Reset();
        _onTerminalStartEvent.Raise(this);
        _terminalScreen = GetComponentInChildren<TextMeshProUGUI>();
        _terminalScreen.text = gameObject.name;
        _waitForSeconds = new WaitForSeconds(1);
        _canStart = false;
    }

	private void OnEnable()
	{
        _onButtonEvent.OnRaise += ChangeStatus;
		_terminalStateEvent.OnRaise += ChangeState;
	}

	void OnDisable() 
    {
        _onButtonEvent.OnRaise -= ChangeStatus;
        _terminalStateEvent.OnRaise -= ChangeState;
    }

    private void Reset() 
    {
        if (GetComponent<AudioSource>() != null) 
        {
            gameObject.AddComponent<AudioSource>();
            GetComponent<AudioSource>().playOnAwake = false;
        }
    }

	#endregion

	/// <summary>
	/// Changes the status of the terminal.
	/// </summary>
	/// <param name="type">The type of button that was pressed.</param>
	private void ChangeStatus(ButtonType type)
    {
        Debug.Log($"Terminal: {gameObject.name}, Button type: {type}, Terminal type: {_data.Type}");

        if (type == ButtonType.Start)
        {
            if (!_canStart && _startSequence == null) _startSequence = StartCoroutine(StartSequence());
            if(_canStart) 
            { 
                _onTerminalEvent.Raise(type, _data.Type); 
                _startSequence = null;
                _canStart = false;
            }

		}
        else
        {
            _onTerminalEvent.Raise(type, _data.Type);
        }
    }


    private void ChangeState(TerminalState terminalState) 
    {
        if (_terminalScreen == null) return;
        
        _terminalScreen.text = gameObject.name + "\n" + terminalState.ToString();
    
    }

    private IEnumerator StartSequence() 
    {
        for(int i = 3; i > 0; i--) 
        {
            Debug.Log($"Start Sequence: {i}, Can Start: {_canStart}, Coroutine: {_startSequence}");
            yield return _waitForSeconds;
        }
        _canStart = true;
		Debug.Log($"Can Start: {_canStart}, Coroutine: {_startSequence}");
		yield return _waitForSeconds;
        _canStart = false;
		Debug.Log($"Can Start: {_canStart}, Coroutine: {_startSequence}");
        _startSequence = null;
	}

}



