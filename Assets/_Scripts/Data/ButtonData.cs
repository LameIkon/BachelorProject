using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Button")]
public class ButtonData : ScriptableObject
{
	[SerializeField] private ButtonType _buttonType;

	public ButtonType Type { get { return _buttonType; } }

}

/// <summary>
/// The different types a button can be.
/// </summary>
public enum ButtonType : byte
{
	Reset,
	Start,
	Stop,
	SpeedUp,
	SpeedDown
}