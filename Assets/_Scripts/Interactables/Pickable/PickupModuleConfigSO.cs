using UnityEngine;

[CreateAssetMenu(fileName = "Pickup Interaction SO", menuName = "ScriptableObject/Interactable/pickup")]
public class PickupModuleConfigSO : InteractionBehaviourConfigSO
{
    public float followSpeed = 20;
    public float linearDamping = 10;

    /// <summary>
    /// Pickup module will contain the logic of interaction, tickable and trigger
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public override InteractionModuleResult Create(GameObject owner, InteractionIdentitySO identity, InteractionDefinitionSO definition, IInteractionEvent interactionEvent)
    {
        if (definition is not PickupInteractionDefinitionSO pickupDef)
        {
            Debug.LogError($"Expected {nameof(PickupInteractionDefinitionSO)} but got {definition?.GetType().Name}");
            return default;
        }

        PickupInteraction module = new PickupInteraction(owner, this, identity, pickupDef, interactionEvent);
        return new InteractionModuleResult
        {
            interaction = module,
            tickable = module,
            trigger = module,
        };
    }
}
