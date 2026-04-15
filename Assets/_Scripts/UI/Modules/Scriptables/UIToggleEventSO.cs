using System;
using UnityEngine;

[CreateAssetMenu(fileName ="UI Toggle Event SO", menuName = "ScriptableObject/Events/UI/UI Toggle")]
public class UIToggleEventSO : ScriptableObject
{
    public event Action<UIRequest> OnRaise;

    public void Raise(UIRequest uiRequest) => OnRaise?.Invoke(uiRequest);
}
