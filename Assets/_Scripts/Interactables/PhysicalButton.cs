using UnityEngine;

public sealed class PhysicalButton : MonoBehaviour, IInteractable 
{

    [SerializeField] private ButtonData _buttonData;
    [SerializeField] private ButtonEventSO _onButtonEvent;

    #region Interactable interface

    /// <summary>
    /// The button invokes an event with the button type currently assigned to it.
    /// </summary>
    public void Interact()
    {
        _onButtonEvent.Raise(_buttonData.Type);
    }
    #endregion
}


