using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Save All Data Event SO", menuName = "ScriptableObject/SaveLoad/Save All")]
public class SaveAllDataEventSO  : ScriptableObject
{
    public event Action OnRaise;

    public void Raise() => OnRaise?.Invoke();
}
