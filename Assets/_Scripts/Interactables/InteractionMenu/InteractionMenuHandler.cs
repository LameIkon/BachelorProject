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
    private bool _isUIOpen;

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

        // Try Open
        if (state && !_isUIOpen)
        {
            _uiToggleEvent.Raise(new UIRequest(UIType.ActionGuide, UIInteractionSource.UIInternal));
            _isUIOpen = true;
        }
        else if (!state && _isUIOpen)
        {
            _uiToggleEvent.Raise(new UIRequest(UIType.ActionGuide, UIInteractionSource.UIInternal));
            _isUIOpen = false;    
        }
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
            _uiToggleEvent.Raise(new UIRequest(UIType.InteractionPopUp, UIInteractionSource.UIInternal));
        }
    }

    #region Cleanup
    public void Dispose()
    {
        InputReader.s_TogglePopUp -= TogglePopUp;
    }
    #endregion
}

