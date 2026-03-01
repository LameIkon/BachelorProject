using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Action Event SO", menuName = "ScriptableObject/Events/Action")]
public class ActionEventSO : ScriptableObject
{
    public event Action OnRaise;

    public void Raise() => OnRaise?.Invoke();
}
