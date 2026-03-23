using System;
using UnityEngine;

/// <summary>
/// This sends an Button event.
/// </summary>
[CreateAssetMenu(fileName = "Button Event SO", menuName = "ScriptableObject/Events/Button")]
public class ButtonEventSO : ScriptableObject
{
    public event Action<ButtonType> OnRaise;

    public void Raise(ButtonType type) => OnRaise?.Invoke(type);
}
