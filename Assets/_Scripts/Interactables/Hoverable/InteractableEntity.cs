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
    [SerializeField] private InputPromptDataSO _inputPromptConfig;

    public event Action<InteractionSignal> raiseModuleComunicator;

    // modules created if neseccary configs is available
    private HighlightModule _highlightModule;
    private InteractionMenuModule _interactionMenuModule;
    private InputPromptModule _inputPromptModule;

    // Some may be null. It depends on interactionConfig for what it's purpose is
    private IInteractionAction _interactionAction;
    private ITickableModule _tickable;
    private ITriggerModule _triggerable;


    private void Awake()
    {
        // Try create highlight
        if (_highlightConfig != null)
        {
            _highlightModule = new HighlightModule(gameObject, _highlightConfig);
            raiseModuleComunicator += _highlightModule.HandleSignal;
        }

        // Try create interaction menu
        if (_menuConfig != null) _interactionMenuModule = new InteractionMenuModule(_menuConfig, _interactionIdentity.compendiumID);

        // Try create input prompt display
        if (_interactionIdentity.prompts.Count > 0) _inputPromptModule = new InputPromptModule(_interactionIdentity.prompts);

        // Create core interaction (eg. should it be a button or an item)
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
        if (_highlightModule != null) raiseModuleComunicator -= _highlightModule.HandleSignal;
    }

    public void Interact(Transform holdPoint = null) => _interactionAction?.Interact(holdPoint);

    public void OnHoverEnter()
    {
        _highlightModule?.OnHoverEnter();     
        _interactionMenuModule?.OnHoverEnter();
    }
    public void OnHoverExit()
    {
        _highlightModule?.OnHoverExit();
        _interactionMenuModule?.OnHoverEnter();
    }

    private void FixedUpdate() => _tickable?.Tick();

    private void OnTriggerEnter(Collider other) => _triggerable?.OnTriggerEnterContext(other);
    private void OnTriggerExit(Collider other) => _triggerable?.OnTriggerExitContext(other);


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