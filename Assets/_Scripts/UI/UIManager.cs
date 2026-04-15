using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("Event")]
    [SerializeField] private UISystemEventSO _registerUIEvent;
    [SerializeField] private UIToggleEventSO _uiToggleEvent;

    private readonly HashSet<IUISystem> _uiSystems = new();
    private readonly HashSet<IUISystem> _activeSystems = new();

    private readonly Dictionary<UIType, IUISystem> _systemLookup = new();

    private void OnEnable()
    {
        _registerUIEvent.OnRaise += RegisterUISystem;
        _uiToggleEvent.OnRaise += HandleToggleRequest;
        InputReader.s_ToggleEscape += HandleEscapeRequest;
    }

    private void OnDisable()
    {
        _registerUIEvent.OnRaise -= RegisterUISystem;
        _uiToggleEvent.OnRaise -= HandleToggleRequest;
        InputReader.s_ToggleEscape -= HandleEscapeRequest;
    }


    #region Register
    private void RegisterUISystem(IUISystem system) 
    {
        _uiSystems.Add(system); // Add to registrated systems

        Debug.Log(system);
        if (!_systemLookup.ContainsKey(system.UIType)) // Add a reference to it for lookup from outside by just asking for UIType
        {
            _systemLookup.Add(system.UIType, system);

            if (system.IsOpen) // If the uiModule is aleady active, add it
            {
                Debug.Log($"active system: {system}");
                _activeSystems.Add(system);
                //HandleToggleRequest(system.UIType);
            }
        }
    }

    #endregion

    #region Methods
    private void HandleToggleRequest(UIType type)
    {
        if (!_systemLookup.TryGetValue(type, out IUISystem system)) return; 

        Debug.Log("handle UI Change");

        if (system.IsOpen)
        {
            system.Close();
            _activeSystems.Remove(system);
            
            if (!_activeSystems.Any(s => s.RuleType == UIRuleType.Solo || s.RuleType == UIRuleType.Stackable))
            {
                InputReader.SetState(InputState.Game);
            }
            Debug.Log("Remove from active list");

            EvaluateOverlayState();
        }
        else
        {
            ApplyUIRules(system);
            system.Open();

            _activeSystems.Add(system);
            
            if (system.RuleType == UIRuleType.Solo || system.RuleType == UIRuleType.Stackable)
            {
                InputReader.SetState(InputState.UI);
            }
            else if (system.RuleType == UIRuleType.BetterNameLater)
            {
                InputReader.SetState(InputState.None);
            }

            Debug.Log("Add to active list");

            EvaluateOverlayState();
        }
    }

    private void ApplyUIRules(IUISystem openedSystem)
    {
        var activeSystems = _activeSystems.ToArray();

        foreach (IUISystem system in activeSystems) // Go Through all ui
        {
            if (system == openedSystem) continue; // Skip system that is the same

            switch (openedSystem.RuleType) // Our opened system type
            {
                case UIRuleType.Solo:
                    if (system.RuleType == UIRuleType.Solo || system.RuleType == UIRuleType.Stackable)  // If our opened system is an exlusive type then we will close all of that 
                    {
                        system.Close();
                        _activeSystems.Remove(system);
                    }
                    break;

                case UIRuleType.Stackable:
                    if (system.RuleType == UIRuleType.Solo) 
                    {
                        system.Close();
                        _activeSystems.Remove(system);
                    }
                    break;
                case UIRuleType.BetterNameLater:
                    system.Close();
                    _activeSystems.Remove(system);
                    break;
                default:
                    break;
            }
        }   
    }

    /// <summary>
    /// Evaluate everytime we open and close ui. Overlay will try to open when no solo or stackable ui elements are active
    /// </summary>
    private void EvaluateOverlayState()
    {
        bool hasBlockingUI = _activeSystems.Any(s => s.RuleType == UIRuleType.Solo || s.RuleType == UIRuleType.Stackable || s.RuleType == UIRuleType.BetterNameLater);

        Debug.Log(hasBlockingUI);

        foreach (IUISystem system in _uiSystems)
        {
            if (system.RuleType != UIRuleType.Overlay) continue;
            
            if (hasBlockingUI)
            {
                if (system.IsOpen)
                    system.Close();
            }
            else
            {
                if (!system.IsOpen)
                    system.Open();
            }
        }
    }


    private void HandleEscapeRequest()
    {
        IUISystem systemToRemove = null;
        foreach (IUISystem system in _activeSystems.Reverse()) // Reverse it to get the newest ui systems first
        {
            if (system.RuleType == UIRuleType.HUD || system.RuleType == UIRuleType.Overlay) continue; // don't consider the HUD or Overlay types

            // Close the newest ui and stop the process
            systemToRemove = system;
            break;
        }

        if (systemToRemove != null) // Remove the system
        {
            _uiToggleEvent.Raise(systemToRemove.UIType);
            //_activeSystems.Remove(systemToRemove);
        }
        else if (systemToRemove == null) // if there are no sytem to removen then open the pause menu
        {
            _uiToggleEvent.Raise(UIType.Pause);
        }
    }

    #endregion

}
