using System;
using UnityEngine;

/// <summary>
/// Depricated script. Will probably be deleted at some point if we find no use for it.
/// </summary>
[CreateAssetMenu(fileName = "Button Event SO", menuName = "ScriptableObject/Events/Button")]
public class ButtonEventSO : ScriptableObject
{
    public event Action<ButtonType> OnRaise;

    public void Raise(ButtonType type) => OnRaise?.Invoke(type);
}
