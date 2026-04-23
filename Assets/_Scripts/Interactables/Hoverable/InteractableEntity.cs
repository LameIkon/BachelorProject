using System;
using UnityEngine;

public class InteractableEntity : MonoBehaviour, IHoverable, IInteractable, IInteractionEvent
{
    [Header("Interaction Module")]
    [SerializeField] private InteractionModuleConfigSO _interactionConfig;
    [SerializeField] private InteractionIdentitySO _interactionIdentity;

    [Header("Optional Modules")]
    [SerializeField] private HighlightModuleConfigSO _highlightConfig;
    [SerializeField] private InteractionMenuModuleConfigSO _menuConfig;

    public event Action<InteractionSignal> raiseModuleComunicator;

    private HighlightModule _hoverModule;
    private InteractionMenuModule _interactionMenuModule;
    
    // Some may be null. It depends on interactionConfig for what it's purpose is
    private IInteractionAction _interactionAction;
    private ITickableModule _tickable;
    private ITriggerModule _triggerable;


    private void Awake()
    {
        if (_highlightConfig != null)
        {
            _hoverModule = new HighlightModule(gameObject, _highlightConfig);
            raiseModuleComunicator += _hoverModule.HandleSignal;
        }
        if (_menuConfig != null) _interactionMenuModule = new InteractionMenuModule(_menuConfig, _interactionIdentity.compendiumID);

        if (_interactionConfig != null)
        {
            InteractionModuleResult result = _interactionConfig.Create(gameObject, _interactionIdentity, this);

            _interactionAction = result.interaction;
            _tickable = result.tickable;
            _triggerable = result.trigger;
        }
    }

    private void OnDisable()
    {
        if (_hoverModule != null) raiseModuleComunicator -= _hoverModule.HandleSignal;
    }

    public void Interact(Transform holdPoint = null) => _interactionAction?.Interact(holdPoint);

    public void OnHoverEnter()
    {
        _hoverModule?.OnHoverEnter();     
        _interactionMenuModule?.OnHoverEnter();
    }
    public void OnHoverExit()
    {
        _hoverModule?.OnHoverExit();
        _interactionMenuModule?.OnHoverEnter();
    }

    private void FixedUpdate() => _tickable?.Tick();

    private void OnTriggerEnter(Collider other) => _triggerable?.OnTriggerEnterContext(other);
    private void OnTriggerExit(Collider other) => _triggerable?.OnTriggerExitContext(other);

    /// <summary>
    /// For modules to communicate with each other
    /// </summary>
    /// <param name="signal"></param>
    //private void HandleAction(InteractionSignal signal)
    //{
    //    switch (signal.InteractionAction)
    //    {
    //        case InteractionSignalType.PickedUp:
    //            _hoverModule?.SetEnabled(false);
    //            break;

    //        case InteractionSignalType.Dropped:
    //            _hoverModule?.SetEnabled(true);
    //            break;
    //        case InteractionSignalType.Placed:
    //            _hoverModule?.SetEnabled(true);
    //            break;
    //    }
    //}

    public void Raise(InteractionSignal signal) => raiseModuleComunicator?.Invoke(signal);
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

public interface IInteractionEvent
{
    event Action<InteractionSignal> raiseModuleComunicator;

    void Raise(InteractionSignal signal);
}