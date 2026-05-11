using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class RangedFloat
{
	[SerializeField] private float _minValue;
	[SerializeField] private float _maxValue;

	private float Value 
	{
		get 
		{
			return Random.Range(_minValue, _maxValue);
		}
	}

	public static implicit operator float(RangedFloat rangedFloat) 
	{
		return rangedFloat.Value;
	}

	public float Min => _minValue;
	public float Max => _maxValue;

}
