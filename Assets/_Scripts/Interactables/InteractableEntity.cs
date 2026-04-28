using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class InteractableEntity : MonoBehaviour, IInteractionEvent
{
    [Header("Required")]
    [SerializeField] private InteractionIdentitySO _interactionIdentity;
    
    [Header("Optional Behaviours")]
    //[SerializeField] private InteractionDefinitionSO _interactionDefinition;
    [SerializeField] private InteractionBehaviourConfigSO _interactionBehaviourConfig;

    [Header("Optional Modules")]
    [SerializeField] private HighlightModuleConfigSO _highlightConfig;
    [SerializeField] private InteractionMenuModuleConfigSO _menuConfig;
    [SerializeField] private InputPromptModuleConfigSO _inputPromptConfig;

    public event Action<InteractionSignal> raiseModuleComunicator;

    // modules created if neseccary configs is available
    private HighlightModule _highlightModule;
    private InteractionMenuModule _interactionMenuModule;
    private InputPromptModule _inputPromptModule;

    // Some may be null. It depends on interactionConfig for what it's purpose is
    private IInteractionAction _interactionAction;
    private ITickableModule _tickable;
    private ITriggerModule _triggerable;

    // Dispose events
    private List<IDisposable> _dispsables = new();

    // Audio Source
    private AudioSource _audioSource;

    // Getters
    public InputPromptModule InputPromptModule => _inputPromptModule;

    private void Awake()
    {
        // Try create highlight
        if (_highlightConfig != null)
        {
            _highlightModule = new HighlightModule(gameObject, _highlightConfig, raiseModuleComunicator);
            _dispsables.Add(_highlightModule);
            //raiseModuleComunicator += _highlightModule.HandleSignal;
        }

        // Try create interaction menu
        if (_menuConfig != null && _interactionIdentity != null)
        {
            _interactionMenuModule = new InteractionMenuModule(_menuConfig, _interactionIdentity.compendiumID, raiseModuleComunicator);
            _dispsables.Add(_interactionMenuModule);
        }

        // Try create input prompt display
        if (_interactionIdentity.prompts.Count > 0) _inputPromptModule = new InputPromptModule(_interactionIdentity.prompts, _inputPromptConfig);

        // Get AudioSource
        _audioSource = GetComponent<AudioSource>();

        // Create core interaction (eg. should it be a button or an item)
        if (_interactionBehaviourConfig != null)
        {
            InteractionModuleResult result = _interactionBehaviourConfig.Create(gameObject, _interactionIdentity, this, _audioSource);

            _interactionAction = result.interaction;
            _tickable = result.tickable;
            _triggerable = result.trigger;
        }
    }

    private void OnDisable()
    {
        foreach (IDisposable disposable in _dispsables)
        {
            disposable.Dispose();
        }
        
        //if (_highlightModule != null) raiseModuleComunicator -= _highlightModule.HandleSignal;
    }

    public void Interact(Transform holdPoint = null) => _interactionAction?.Interact(holdPoint);

    public void OnHoverEnter()
    {
        _highlightModule?.OnHoverEnter();     
        _inputPromptModule?.OnHoverEnter();
        _interactionMenuModule?.OnHoverEnter();
    }
    public void OnHoverExit()
    {
        _highlightModule?.OnHoverExit();
        _inputPromptModule?.OnHoverExit();
        _interactionMenuModule?.OnHoverExit();
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