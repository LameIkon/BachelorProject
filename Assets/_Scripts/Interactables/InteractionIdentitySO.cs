using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interaction Identity SO", menuName = "ScriptableObject/Interactable/Identity/Base")]
public class InteractionIdentitySO : ScriptableObject
{
    public CompendiumID compendiumID; 

    [Header("Input Prompts")]
    public List<InputPromptDataSO> prompts;
}
