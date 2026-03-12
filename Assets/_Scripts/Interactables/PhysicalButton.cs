using UnityEngine;
using System;

public sealed class PhysicalButton : MonoBehaviour, IInteractable
{

    [SerializeField] private ButtonData _buttonData;
    public Action<ButtonType> OnButtonClicked;

    #region Interactable interface

    /// <summary>
    /// The button invokes an event with the button type currently assigned to it.
    /// </summary>
    public void Interact()
    {
        OnButtonClicked?.Invoke(_buttonData.Type);
    }

    #endregion
}


