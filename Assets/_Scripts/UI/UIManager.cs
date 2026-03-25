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
    }

    private void OnDisable()
    {
        _registerUIEvent.OnRaise -= RegisterUISystem;
        _uiToggleEvent.OnRaise -= HandleToggleRequest;
    }


    #region Register
    private void RegisterUISystem(IUISystem system) 
    {
        _uiSystems.Add(system); // Add to registrated systems

        if (!_systemLookup.ContainsKey(system.UIType)) // Add a reference to it for lookup from outside by just asking for UIType
        {
            _systemLookup.Add(system.UIType, system);
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
            
            InputReader.SetState(InputState.Game);
            Debug.Log("Remove from active list");
        }
        else
        {
            ApplyUIRules(system);
            system.Open();

            _activeSystems.Add(system);
            
            InputReader.SetState(InputState.UI);
            Debug.Log("Add to active list");
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

                default:
                    break;
            }
        }   
    }
    #endregion

}
