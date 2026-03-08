using System.Collections.Generic;
using UnityEngine;
using System;

public class Terminal : MonoBehaviour
{

    private TerminalStatus _status;

    public Action<TerminalStatus> OnTerminalStatusChange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _status = TerminalStatus.Warning;
        // Assigns the ChangeStatus to all the Action from buttons that are children of the parent object.
        foreach (PhysicalButton button in GetComponentsInChildren<PhysicalButton>()) 
        {
            button.OnButtonClicked += ChangeStatus;
        }
    }

    void OnDisable() 
    {
        // Removes the Action again for the buttons, this ensures that there are no leaks.
        foreach (PhysicalButton button in GetComponentsInChildren<PhysicalButton>())
        {
            button.OnButtonClicked -= ChangeStatus;
        }
    }


    /// <summary>
    /// Changes the status of the terminal.
    /// </summary>
    /// <param name="type">The type of button that was pressed.</param>
    private void ChangeStatus(ButtonType type) 
    {

        // Takes the status of the terminal, then it sets it according to how it should act. Not fully implemented yet.
        switch (_status) 
        {
            case TerminalStatus.Warning:
                if (type == ButtonType.Reset) _status = TerminalStatus.Off;
                break;

            case TerminalStatus.Running:
                if (type == ButtonType.Stop) _status = TerminalStatus.Off;
                break;

            case TerminalStatus.Off:
                if (type == ButtonType.Start) _status = TerminalStatus.Running;
                break;
       
        }

        OnTerminalStatusChange?.Invoke(_status);

        Debug.Log($"ButtonType pressed: {type}, Terminal Status: {_status}");
    }


}

/// <summary>
/// Different status of the terminal.
/// </summary>
public enum TerminalStatus 
{
    Warning,
    Running,
    Off
}

