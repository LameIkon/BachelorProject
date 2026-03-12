using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Terminal")]
public class TerminalData : ScriptableObject
{
	[SerializeField] private TerminalType _type;

	public TerminalType Type { get { return _type; } }


}


public enum TerminalType : byte
{
	Main,
	Reset1,
	Reset2,
	Leaver
}
