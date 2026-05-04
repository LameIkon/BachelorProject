using UnityEngine;

[CreateAssetMenu(fileName = "Conveyor Stops", menuName = "ScriptableObject/Conveyor/Stops")]
public class ConveyorStopSO : ScriptableObject
{
	[SerializeField] private TerminalState _state;
	[SerializeField] private TerminalStateEventSO _terminalStateEvent;

	public void OnStop() 
	{
		_terminalStateEvent.Raise(_state);
	}
}
