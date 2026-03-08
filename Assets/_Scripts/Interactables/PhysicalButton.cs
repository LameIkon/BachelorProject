using UnityEngine;
using System;

public sealed class PhysicalButton : MonoBehaviour, IInteractable
{

    [SerializeField] private ButtonType _buttonType;
    public Action<ButtonType> OnButtonClicked;

    #region Interact interface

    /// <summary>
    /// The button invokes an event with the button type currently assigned to it.
    /// </summary>
    public void Interact()
    {
        OnButtonClicked?.Invoke(_buttonType);
    }

    #endregion
}

/// <summary>
/// The different types a button can be.
/// </summary>
public enum ButtonType : byte
{
    Reset,
    Start,
    Stop
}
