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

        InputReader.s_TogglePopUp += OnInteract;

    }

    //public void OnHoverState(bool state)
    //{
    //    _isSelected = state;

    //    if (state)
    //    {
    //        Debug.Log("Open");
    //        _uiToggleEvent.Raise(new UIRequest(UIType.ActionGuide, UIInteractionSource.UIInternal, UIAction.Open));
    //    }
    //    else if (!state)
    //    {
    //        Debug.Log("Close");
    //        _uiToggleEvent.Raise(new UIRequest(UIType.ActionGuide, UIInteractionSource.UIInternal, UIAction.Close));
    //    }

    //}

    /// <summary>
    /// Toggle interaction menu from event call
    /// </summary>
    private void OnInteract()
    {
        if (_uiToggleEvent == null) return;

        if (_isSelected)
        {
            Debug.Log("Toggle");
            _compendiumPageEvent.Raise(_compendiumID);
            _uiToggleEvent.Raise(new UIRequest(UIType.InteractionPopUp, UIInteractionSource.Hotkey));
        }
    }

    #region Cleanup
    public void Dispose()
    {
        InputReader.s_TogglePopUp -= OnInteract;
    }
    #endregion
}

