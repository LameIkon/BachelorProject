using UnityEngine;
using System;

public class Terminal : MonoBehaviour
{

    [SerializeField] private TerminalData _data;
    [SerializeField] private TerminalEventSO _onTerminalEvent;
    [SerializeField] private TerminalStartEventSO _onTerminalStartEvent;
    [SerializeField] private ButtonEventSO _onButtonEvent;
    
    public Action<bool> OnSpeedChange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _onTerminalStartEvent.Raise(this);
        /*
        // Assigns the ChangeStatus to all the Action from buttons that are children of the parent object.
        foreach (PhysicalButton button in GetComponentsInChildren<PhysicalButton>()) 
        {
            _onButtonEvent.OnRaise += ChangeStatus;
        }
        */
        _onButtonEvent.OnRaise += ChangeStatus;
    }

    void OnDisable() 
    {
        /*
        // Removes the Action again for the buttons, this ensures that there are no leaks.
        foreach (PhysicalButton button in GetComponentsInChildren<PhysicalButton>())
        {
            _onButtonEvent.OnRaise -= ChangeStatus;
        }
        */
        _onButtonEvent.OnRaise -= ChangeStatus; 
    }


    /// <summary>
    /// Changes the status of the terminal.
    /// </summary>
    /// <param name="type">The type of button that was pressed.</param>
    private void ChangeStatus(ButtonType type) 
    {
        _onTerminalEvent.Raise(type, _data.Type);
    }

    

}



