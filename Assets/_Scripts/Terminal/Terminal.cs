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


    [Header("Lights")]
    [SerializeField] private ButtonLight _onLight;
    [SerializeField] private ButtonLight _warningLight;

    public Action<bool> OnSpeedChange;
    private TextMeshProUGUI _terminalScreen;
    private LightManager _lightManager;

    #region Unity Method
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Reset();
        _onTerminalStartEvent.Raise(this);
        _terminalScreen = GetComponentInChildren<TextMeshProUGUI>();
        _terminalScreen.text = gameObject.name;
        _lightManager = new LightManager(_warningLight, _onLight);
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

    //private void Reset() 
    //{
    //    if (GetComponent<AudioSource>() != null) 
    //    {
    //        gameObject.AddComponent<AudioSource>();
    //        GetComponent<AudioSource>().playOnAwake = false;
    //    }
    //}

    #endregion

    /// <summary>
    /// Changes the status of the terminal.
    /// </summary>
    /// <param name="type">The type of button that was pressed.</param>
    private void ChangeStatus(ButtonType type)
    {
        ChangeLightsInternaly(type);
        _onTerminalEvent.Raise(type, _data.Type);
    }


    private void ChangeState(TerminalState terminalState)
    {
        ChangeLightsExtern(terminalState);
        if (_terminalScreen == null) return;

        _terminalScreen.text = gameObject.name + "\n" + terminalState.ToString();
    }

    public void ChangeLightsExtern(TerminalState terminalState)
    {
        switch (terminalState)
        {
            case TerminalState.Warning:
                _lightManager.TurnWarning();
                break;
            case TerminalState.LeverWarning:
                _lightManager.TurnWarning();
                break;
            case TerminalState.Running:
                _lightManager.TurnOnLight();
                break;
            case TerminalState.Off:
                _lightManager.TurnOffAllLights();
                break;
        }

    }

    public void ChangeLightsInternaly(ButtonType type)
    {
        switch (type)
        {
            case ButtonType.Reset:
            case ButtonType.Stop:
                _lightManager.TurnOffAllLights();
                break;
        }
    }

}



