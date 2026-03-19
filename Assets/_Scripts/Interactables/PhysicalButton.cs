using UnityEngine;
using System;

public sealed class PhysicalButton : MonoBehaviour, IInteractable
{

    [SerializeField] private ButtonData _buttonData;
    public event Action<ButtonType> OnButtonClicked;
    [SerializeField] private ButtonEventSO _onButtonEvent;

    #region Interactable interface

    /// <summary>
    /// The button invokes an event with the button type currently assigned to it.
    /// </summary>
    public void Interact(Transform transform)
    {
        OnButtonClicked?.Invoke(_buttonData.Type);
        _onButtonEvent.Raise(_buttonData.Type);
    }

    #endregion
}


