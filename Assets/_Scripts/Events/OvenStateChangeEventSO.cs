using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Oven state Event SO", menuName = "ScriptableObject/Events/OvenStateChange")]
public class OvenStateChangeEventSO : ScriptableObject
{
    public event Action<OvenStatus> OnRaise;

    public void Raise(OvenStatus status) => OnRaise?.Invoke(status);
}
