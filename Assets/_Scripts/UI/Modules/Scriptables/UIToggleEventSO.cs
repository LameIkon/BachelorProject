using System;
using UnityEngine;

[CreateAssetMenu(fileName ="UI Toggle Event SO", menuName = "ScriptableObject/Events/UI/UI Toggle")]
public class UIToggleEventSO : ScriptableObject
{
    public event Action<UIType> OnRaise;

    public void Raise(UIType uiType) => OnRaise?.Invoke(uiType);
}