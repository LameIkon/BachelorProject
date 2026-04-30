using System;
using UnityEngine;

[Serializable]
public class ColorReferece 
{
	[SerializeField] private bool _useConstant = true;
	[SerializeField] private Color _constantColor;
	[SerializeField] private ColorVariableSO _variable;

	public ColorReferece(ColorVariableSO variable)
	{
		_useConstant = false;
		_variable = variable;
	}

	public ColorReferece(Color color)
	{
		_useConstant = true;
		_constantColor = color;
	}

	public Color GetColor
	{
		get { return _useConstant ? _constantColor : _variable.GetColor(); }
	}

	public static implicit operator Color(ColorReferece referece)
	{
		return referece.GetColor;
	}

}
