using System;
using UnityEngine;

/// <summary>
/// This sends a Terminal event.
/// </summary>
[CreateAssetMenu(fileName = "Terminal State Event SO", menuName = "ScriptableObject/Events/TerminalState")]
public class TerminalStateEventSO : ScriptableObject
{
	public event Action<TerminalState> OnRaise;

	public void Raise(TerminalState state) => OnRaise?.Invoke(state);
}
