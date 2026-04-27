using System;
using UnityEngine;

/// <summary>
/// This module enables highlight of objects on hovering
/// </summary>
public class HighlightModule : IDisposable
{
    private readonly HighlightHandler _highlightHandler;

    private bool _canHighlight = true;

    private Action<InteractionSignal> _moduleCommunicatorEvent;


    public HighlightModule(GameObject owner, HighlightModuleConfigSO config, Action<InteractionSignal> action)
    {
        _highlightHandler = new HighlightHandler(owner, config);
        _moduleCommunicatorEvent = action;
        _moduleCommunicatorEvent += HandleSignal;
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
        _moduleCommunicatorEvent -= HandleSignal;
    }
}
