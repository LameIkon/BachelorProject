using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Input Prompt Provide Event SO", menuName = "ScriptableObject/Interaction/Input Prompt Event Provider")]
public class InputPromptProvideEventSO : ScriptableObject
{
    public event Action<IEnumerable<InteractionData>> OnRaise;

    public void Raise(IEnumerable<InteractionData> hoverRequest) => OnRaise?.Invoke(hoverRequest);
}
