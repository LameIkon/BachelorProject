using System;
using UnityEngine;

[CreateAssetMenu(fileName ="UI Hover data Event SO", menuName = "ScriptableObject/Events/UI/UI hover interaction data")]
public class UIHoverDataEventSO : ScriptableObject
{
    public event Action<InteractionData> OnRaise;

    public void Raise(InteractionData hoverRequest) => OnRaise?.Invoke(hoverRequest);
}
