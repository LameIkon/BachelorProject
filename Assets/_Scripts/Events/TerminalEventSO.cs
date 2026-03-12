using System;
using UnityEngine;
/// <summary>
/// Depricated script. Will probably be deleted at some point if we find no use for it.
/// </summary>
[CreateAssetMenu(fileName = "Terminal Event SO", menuName = "ScriptableObject/Events/Terminal")]
public class TerminalEventSO : ScriptableObject
{
    public event Action<ButtonType, TerminalType> OnRaise;

    public void Raise(ButtonType buttonType, TerminalType terminalType) => OnRaise?.Invoke(buttonType, terminalType);
}
