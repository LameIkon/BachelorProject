using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data/Button")]
public class ButtonData : ScriptableObject
{
	[SerializeField] private ButtonType _buttonType;
	[SerializeField] private Color _onColor;
	[SerializeField] private Color _offColor;
	private Color _color;

	public ButtonType Type { get { return _buttonType; } }
	public Color Color { get { return _color; } }

	public void SetColor(bool _isOn) 
	{
		_color = _isOn ? _onColor : _offColor;
	}


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