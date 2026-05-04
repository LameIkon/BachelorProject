using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class LightManager 
{
	private readonly ButtonLight? _warningLight;
	private readonly ButtonLight? _onLight;

	public LightManager(ButtonLight warningLight, ButtonLight onLight) 
	{
		if(warningLight != null) _warningLight = warningLight;
		if(onLight != null) _onLight = onLight;
	}

	private void TurnWarning(bool on) 
	{
		if (_warningLight == null) return;
		_warningLight.TurnLight(on);
	}

	private void TurnOnLight(bool on) 
	{
		if (_onLight == null) return;
		_onLight.TurnLight(on);
	}

	public void TurnWarning() 
	{
		TurnWarning(true);
		TurnOnLight(false);
	}

	public void TurnOnLight() 
	{
		TurnWarning(false);
		TurnOnLight(true);
	}

	public void TurnOffAllLights() 
	{
		TurnWarning(false);
		TurnOnLight(false);
	}



}
