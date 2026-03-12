using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Terminal")]
public class TerminalData : ScriptableObject
{
	[SerializeField] private TerminalStatus _status;
	[SerializeField] private TerminalType _type;

	public TerminalStatus Status { get { return _status; } }
	public TerminalType Type { get { return _type; } }


}

/// <summary>
/// Different status of the terminal.
/// </summary>
public enum TerminalStatus : byte
{
	Warning,
	Running,
	Off
}

public enum TerminalType : byte
{
	Main,
	Reset1,
	Reset2,
	Leaver
}
