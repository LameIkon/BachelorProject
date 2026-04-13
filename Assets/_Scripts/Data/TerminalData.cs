using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Terminal")]
public class TerminalData : ScriptableObject
{
	[SerializeField] private TerminalType _type;
	[SerializeField] private AudioClip _buttonPressSound;

	public TerminalType Type { get { return _type; } }
	public AudioClip ButtonSound => _buttonPressSound;

}


public enum TerminalType : byte
{
	Main,
	Reset1,
	Reset2,
	Lever
}
