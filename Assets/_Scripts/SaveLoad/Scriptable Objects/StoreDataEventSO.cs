using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Store Data Event SO", menuName = "ScriptableObject/SaveLoad/Store Data")]
public class StoreDataEventSO : ScriptableObject
{
    public event Action<InteractionEvent> OnRaise;

    public void Raise(InteractionEvent context) => OnRaise?.Invoke(context);
}
