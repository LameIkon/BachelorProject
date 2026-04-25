using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Input Prompt Provide Event SO", menuName = "ScriptableObject/Interaction/Input Prompt Event Provider")]
public class InputPromptProvideEventSO : ScriptableObject
{
    public event Action<InteractionData> OnRaise;

    public void Raise(InteractionData hoverRequest) => OnRaise?.Invoke(hoverRequest);
}
