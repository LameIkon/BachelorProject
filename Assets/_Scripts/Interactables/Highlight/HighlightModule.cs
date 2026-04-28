using System;
using UnityEngine;

/// <summary>
/// This module enables highlight of objects on hovering
/// </summary>
public class HighlightModule : IDisposable
{
    private readonly HighlightHandler _highlightHandler;

    private bool _canHighlight = true;

    private readonly IInteractionEvent _interactionEvent;


    public HighlightModule(GameObject owner, HighlightModuleConfigSO config, IInteractionEvent interactionEvent)
    {
        _highlightHandler = new HighlightHandler(owner, config);
        _interactionEvent = interactionEvent;

        _interactionEvent.raiseModuleComunicator += HandleSignal;
    }

    public void HandleSignal(InteractionSignal signal)
    {
        Debug.Log("Handle interaction");
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

    public void Dispose()
    {
        _interactionEvent.raiseModuleComunicator -= HandleSignal;
    }
}
