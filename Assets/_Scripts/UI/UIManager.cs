using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("Event")]
    [SerializeField] private UISystemEventSO _registerUIEvent;
    [SerializeField] private UISystemEventSO _uiStateEvent;

    private readonly HashSet<IUISystem> _uiSystems = new();
    private readonly HashSet<IUISystem> _activeSystems = new();

    private void OnEnable()
    {
        _registerUIEvent.OnRaise += RegisterUISystem;
        _uiStateEvent.OnRaise += HandleUIChanged;
    }

    private void OnDisable()
    {
        _registerUIEvent.OnRaise -= RegisterUISystem;
        _uiStateEvent.OnRaise -= HandleUIChanged;
    }


    #region Register
    private void RegisterUISystem(IUISystem system) 
    {
        _uiSystems.Add(system);
    }

    #endregion

    #region Methods
    private void HandleUIChanged(IUISystem system)
    {
        Debug.Log("handle UI Change");
        if (system.IsOpen)
        {
            ApplyUIRules(system);
            _activeSystems.Add(system);
        }
        else
        {
            Debug.Log("Remove from active list");
            _activeSystems.Remove(system);
        }
    }

    private void ApplyUIRules(IUISystem openedSystem)
    {
        var activeSystems = _activeSystems.ToArray();

        foreach (IUISystem system in activeSystems) // Go Through all ui
        {
            if (system == openedSystem) continue; // Skip system that is the same

            switch (openedSystem.UIType) // Our opened system type
            {
                case UIType.Solo:
                    if (system.UIType == UIType.Solo || system.UIType == UIType.Stackable)  // If our opened system is an exlusive type then we will close all of that 
                    {
                        system.Close();
                        _activeSystems.Remove(system);
                    }
                    break;

                case UIType.Stackable:
                    if (system.UIType == UIType.Solo) 
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
