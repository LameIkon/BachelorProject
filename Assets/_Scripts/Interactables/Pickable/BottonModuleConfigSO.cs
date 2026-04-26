using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Button Interaction SO", menuName = "ScriptableObject/Interactable/Button")]
public class BottonModuleConfigSO : InteractionBehaviourConfigSO
{
    /// <summary>
    /// Button module will contain the logic of interaction
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public override InteractionModuleResult Create(GameObject owner, InteractionIdentitySO identity, InteractionDefinitionSO definition,  IInteractionEvent interactionEvent)
    {
        if (definition is not ButtonInteractionDefinitionSO buttonDef)
        {
            Debug.LogError($"Expected {nameof(ButtonInteractionDefinitionSO)} but got {definition?.GetType().Name}");
            return default;
        }

        WorldButton module = new WorldButton(owner, this, buttonDef);
        return new InteractionModuleResult
        {
            interaction = module,
        };
    }
}
