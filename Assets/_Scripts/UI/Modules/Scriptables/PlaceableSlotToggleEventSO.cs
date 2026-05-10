using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Placeable Slot Toggle Event SO", menuName = "ScriptableObject/Placeable Slot Toggle")]
public class PlaceableSlotToggleEventSO : ScriptableObject
{
    public event Action<PickableType, bool> OnRaise;

    public void Raise(PickableType type, bool state) => OnRaise?.Invoke(type, state);
}