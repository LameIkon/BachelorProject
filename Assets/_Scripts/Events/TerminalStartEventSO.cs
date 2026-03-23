using System;
using UnityEngine;

/// <summary>
/// This sends a Terminal start event.
/// </summary>
[CreateAssetMenu(fileName = "Terminal Start Event SO", menuName = "ScriptableObject/Events/TerminalStart")]
public class TerminalStartEventSO : ScriptableObject
{
    public event Action<Terminal> OnRaise;

    public void Raise(Terminal terminal) => OnRaise?.Invoke(terminal);
}
