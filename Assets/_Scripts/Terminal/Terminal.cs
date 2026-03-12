using UnityEngine;
using System;

public class Terminal : MonoBehaviour
{

    [SerializeField] private TerminalData _data;

    public static Action<ButtonType, TerminalType> OnTerminalButtonPress;
    public Action<bool> OnSpeedChange;
    public static Action<Terminal> OnTerminalStart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnTerminalStart?.Invoke(this);
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
        OnTerminalButtonPress?.Invoke(type, _data.Type);
    }

    

}



