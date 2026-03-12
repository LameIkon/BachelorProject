using System;
using UnityEngine;

/// <summary>
/// Depricated script. Will probably be deleted at some point if we find no use for it.
/// </summary>
[CreateAssetMenu(fileName = "Terminal Start Event SO", menuName = "ScriptableObject/Events/TerminalStart")]
public class TerminalStartEventSO : ScriptableObject
{
    public event Action<Terminal> OnRaise;

    public void Raise(Terminal terminal) => OnRaise?.Invoke(terminal);
}
