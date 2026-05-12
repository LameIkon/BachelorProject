using System;
using UnityEngine;

/// <summary>
/// This sends a Terminal event.
/// </summary>
[CreateAssetMenu(fileName = "Terminal State Event SO", menuName = "ScriptableObject/Events/TerminalState")]
public class TerminalStateEventSO : ScriptableObject
{
	public event Action<TerminalStateData> OnRaise;

	public void Raise(TerminalStateData stateData) => OnRaise?.Invoke(stateData);
}
