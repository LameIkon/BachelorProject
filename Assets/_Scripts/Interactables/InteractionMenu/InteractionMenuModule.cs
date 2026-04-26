using UnityEngine;

public class InteractionMenuModule
{
    //private readonly InteractionMenuHandler _interactionMenuHandler;

    private readonly UIToggleEventSO _uiToggleEvent;
    private readonly CompendiumPageRequestEventSO _compendiumPageEvent;
    private readonly CompendiumID _compendiumID;  
    
    private bool _isSelected;

    public InteractionMenuModule(InteractionMenuModuleConfigSO config, CompendiumID id)
    {
        //_interactionMenuHandler = new InteractionMenuHandler(config.uiToggleEvent, config.compendiumPageEvent, id);

        _uiToggleEvent = config.uiToggleEvent;
        _compendiumPageEvent = config.compendiumPageEvent;
        _compendiumID = id;

        InputReader.s_TogglePopUp += OnInteract;
    }


    public void OnHoverEnter()
    {
        _isSelected = true;
    }

    public void OnHoverExit()
    {
        _isSelected = false;
    }

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