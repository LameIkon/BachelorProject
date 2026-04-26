using UnityEngine;

[CreateAssetMenu(fileName = "Button Interaction SO", menuName = "ScriptableObject/Interactable/Button")]
public class BottonModuleConfigSO : InteractionBehaviourConfigSO
{
    /// <summary>
    /// Button module will contain the logic of interaction
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public override InteractionModuleResult Create(GameObject owner, InteractionIdentitySO identity,  IInteractionEvent interactionEvent)
    {
        if (identity is not ButtonInteractionIdentitySO buttonDef)
        {
            Debug.LogError($"Expected {nameof(ButtonInteractionIdentitySO)} but got {identity?.GetType().Name}");
            return default;
        }

        WorldButton module = new WorldButton(owner, this, buttonDef);
        return new InteractionModuleResult
        {
            interaction = module,
        };
    }
}
