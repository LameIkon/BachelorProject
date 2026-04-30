using UnityEngine;

[CreateAssetMenu(fileName = "Color Variable", menuName = "ScriptableObject/Variable/Color")]
public class ColorVariableSO : ScriptableObject 
{
	[SerializeField] private Color _color;

	public void SetColor(Color color)
	{
		_color = color;
	}

	public void SetColor(ColorVariableSO variable)
	{
		_color = variable._color;
	}

	public Color GetColor() 
	{
		return _color;
	}

	public static implicit operator Color(ColorVariableSO variable) => variable._color;

}
