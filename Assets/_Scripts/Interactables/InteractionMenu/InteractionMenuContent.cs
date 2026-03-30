using System;
using UnityEngine;

/// <summary>
/// The purpose of this class is just to hold the required components needed for the InteractionMenu to use. 
/// </summary>
[Serializable]
public class InteractionMenuContent
{
	[Header("Events")]
	[SerializeField] private UIToggleEventSO _uiToggleEvent;
	[SerializeField] private CompendiumPageRequestEventSO _pageRequestEvent;

	[Header("Data type")]
	[SerializeField] private CompendiumID _compendiumID;

	private InteractionMenuHandler _interactionMenuHandler;

	public InteractionMenuHandler InteractionMenuHandler => _interactionMenuHandler;

	public void Initialize()
	{
		_interactionMenuHandler = new InteractionMenuHandler(_uiToggleEvent, _pageRequestEvent, _compendiumID);
	}
}
