using System;
using UnityEngine;

/// <summary>
/// Really fast and ugly script to just get something done
/// </summary>
public class InspectStartTerminal : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private UIToggleEventSO _toggleUI;


    public static Action<bool> OnRaise;

    public static void Raise(bool state) => OnRaise?.Invoke(state);

    private void Awake()
    {
        _container.SetActive(false);
    }


    private void OnEnable()
    {
        OnRaise += ToggleInspectStartTerminal;
    }

    private void OnDisable()
    {
        OnRaise -= ToggleInspectStartTerminal;
    }

    private void ToggleInspectStartTerminal(bool state)
    {
        if (state)
        {
            _container.SetActive(true);
            //InputReader.SetState(InputState.None);
            _toggleUI.Raise(new UIRequest(UIType.InspectStartTerminal, UIInteractionSource.Hotkey, UIAction.Open)); // Open
        }
        else
        {
            _container.SetActive(false);
            //InputReader.SetState(InputState.Game);
            _toggleUI.Raise(new UIRequest(UIType.InspectStartTerminal, UIInteractionSource.Hotkey, UIAction.Close)); // Close
        }
    }

}
