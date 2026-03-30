using System;
using UnityEngine;

/// <summary>
/// A handler that depends on hover state and is used to toggle ui
/// </summary>
public class InteractionMenuHandler : IDisposable
{
    private readonly UIToggleEventSO _uiToggleEvent;
    private readonly CompendiumPageRequestEventSO _compendiumPageEvent;
    private readonly CompendiumID _compendiumID;  
    
    private bool _isSelected;

    public InteractionMenuHandler(UIToggleEventSO uiToggleEvent, CompendiumPageRequestEventSO compendiumPageRequestEventSO, CompendiumID compendiumID)
    {
        _uiToggleEvent = uiToggleEvent;
        _compendiumPageEvent = compendiumPageRequestEventSO;
        _compendiumID = compendiumID;

        InputReader.s_TogglePopUp += TogglePopUp;

    }

    public void OnHoverState(bool state)
    {
        _isSelected = state;
    }

    /// <summary>
    /// Toggle ui
    /// </summary>
    private void TogglePopUp()
    {
        if (_uiToggleEvent == null) return;

        if (_isSelected)
        {
            Debug.Log("Toggle");
            _compendiumPageEvent.Raise(_compendiumID);
            _uiToggleEvent.Raise(UIType.InteractionPopUp);
        }
    }

    #region Cleanup
    public void Dispose()
    {
        InputReader.s_TogglePopUp -= TogglePopUp;
    }
    #endregion
}

