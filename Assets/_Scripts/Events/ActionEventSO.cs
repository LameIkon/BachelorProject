using System;
using UnityEngine;
/// <summary>
/// Depricated script. Will probably be deleted at some point if we find no use for it.
/// </summary>
[CreateAssetMenu(fileName ="Action Event SO", menuName = "ScriptableObject/Events/Action")]
public class ActionEventSO : ScriptableObject
{
    public event Action OnRaise;

    public void Raise() => OnRaise?.Invoke();
}
