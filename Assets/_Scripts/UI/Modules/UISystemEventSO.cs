using System;
using UnityEngine;

/// <summary>
/// Used both to register and to update ui
/// </summary>

[CreateAssetMenu(fileName ="UI System Event SO", menuName = "ScriptableObject/Events/UI/UI System")]
public class UISystemEventSO : ScriptableObject
{
    public event Action<IUISystem> OnRaise;
    public void Raise(IUISystem system) => OnRaise?.Invoke(system);
}
