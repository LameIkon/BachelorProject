using System;
using UnityEngine;

/// <summary>
/// This sends a Terminal event.
/// </summary>
[CreateAssetMenu(fileName = "Terminal Event SO", menuName = "ScriptableObject/Events/Terminal")]
public class TerminalEventSO : ScriptableObject
{
    public event Action<ButtonType, TerminalType> OnRaise;

    public void Raise(ButtonType buttonType, TerminalType terminalType) => OnRaise?.Invoke(buttonType, terminalType);
}
