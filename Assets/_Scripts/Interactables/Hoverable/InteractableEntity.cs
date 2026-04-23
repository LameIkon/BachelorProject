using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractableEntity : MonoBehaviour, IHoverable, IInteractable
{
    [Header("Interaction Module")]
    [SerializeField] private InteractionModuleConfigSO _interactionConfig;
    [SerializeField] private InteractionIdentitySO _interactionIdentity;

    [Header("Optional Modules")]
    [SerializeField] private HighlightModuleConfigSO _highlightConfig;
    [SerializeField] private InteractionMenuModuleConfigSO _menuConfig;


	private HighlightModule _hoverModule;
    private InteractionMenuModule _interactionMenuModule;
    
    // Some may be null. It depends on interactionConfig for what it's purpose is
    private IInteractionAction _interactionAction;
    private ITickableModule _tickable;
    private ITriggerModule _triggerable;
    private IInteractionSignalSource _signalSource;


    private List<IHoverable> _hoverables;

    private void Awake()
    {
        if (_highlightConfig != null) _hoverModule = new HighlightModule(gameObject, _highlightConfig);
        //if (_interactionConfig != null) _interactionMenuModule = new InteractionMenuModule();

        if (_interactionConfig != null)
        {
            InteractionModuleResult result = _interactionConfig.Create(gameObject, _interactionIdentity);

            _interactionAction = result.interaction;
            _tickable = result.tickable;
            _triggerable = result.trigger;

            _signalSource = _interactionAction as IInteractionSignalSource;
            if (_signalSource != null)
            {
                _signalSource.OnRaise += HandleAction;
            }
        }
    }

    private void OnDisable()
    {
        if (_signalSource != null) _signalSource.OnRaise -= HandleAction;
    }

    public void Interact(Transform holdPoint = null) => _interactionAction?.Interact(holdPoint);

    public void OnHoverEnter() => _hoverModule?.OnHoverEnter();     
    public void OnHoverExit() => _hoverModule?.OnHoverExit();

    private void FixedUpdate() => _tickable?.Tick();

    private void OnTriggerEnter(Collider other) => _triggerable?.OnTriggerEnterContext(other);
    private void OnTriggerExit(Collider other) => _triggerable?.OnTriggerExitContext(other);

    /// <summary>
    /// For modules to communicate with each other
    /// </summary>
    /// <param name="signal"></param>
    private void HandleAction(InteractionSignal signal)
    {
        switch (signal.InteractionAction)
        {
            case InteractionSignalType.PickedUp:
                _hoverModule?.SetEnabled(false);
                break;

            case InteractionSignalType.Dropped:
                _hoverModule?.SetEnabled(true);
                break;
            case InteractionSignalType.Placed:
                _hoverModule?.SetEnabled(true);
                break;
        }
    }
}

public interface ITickableModule
{
    void Tick();
}

public interface IInteractionAction
{
    void Interact(Transform transform);
}

public interface ITriggerModule
{
    void OnTriggerEnterContext(Collider other);
    void OnTriggerExitContext(Collider other);
}

public struct InteractionModuleResult
{
    public IInteractionAction interaction;
    public ITickableModule tickable;
    public ITriggerModule trigger;
}

public enum InteractionSignalType
{
    PickedUp,
    Dropped,
	Placed,
}

public struct InteractionSignal
{
    public InteractionSignalType InteractionAction;
}

public interface IInteractionSignalSource
{
    event Action<InteractionSignal> OnRaise;
}

//public interface IPickableIdentity
//{
//    PickableType Type { get; }
//    Transform Transform { get; }
//}