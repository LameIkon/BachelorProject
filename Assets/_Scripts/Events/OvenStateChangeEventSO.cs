using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Oven state Event SO", menuName = "ScriptableObject/Events/OvenStateChange")]
public class OvenStateChangeEventSO : ScriptableObject
{
    public event Action<float> OnRaise;

    public void Raise(float speed) => OnRaise?.Invoke(speed);
}
