using UnityEngine;

/// <summary>
/// This module enables highlight of objects on hovering
/// </summary>
public class HighlightModule
{
    private readonly HighlightHandler _highlightHandler;

    private bool _canHighlight = true;


    public HighlightModule(GameObject owner, HighlightModuleConfigSO config)
    {
        _highlightHandler = new HighlightHandler(owner, config);
    }

    public void HandleSignal(InteractionSignal signal)
    {
        switch (signal.InteractionAction)
        {
            case InteractionSignalType.PickedUp:
                SetEnabled(false);
                break;

            case InteractionSignalType.Dropped:
            case InteractionSignalType.Placed:
                SetEnabled(true);
                break;
        }
    }


    private void SetEnabled(bool enabled)
    {
        _canHighlight = enabled;

        if (!enabled)
        {
            _highlightHandler.SetHighlight(false);
        }
        else
        {
            _highlightHandler.SetHighlight(true);
        }
    }

    public void OnHoverEnter()
    {
        if (!_canHighlight) return;
        _highlightHandler.SetHighlight(true);

    }
    public void OnHoverExit()
    {
        _highlightHandler.SetHighlight(false);

    }
}
