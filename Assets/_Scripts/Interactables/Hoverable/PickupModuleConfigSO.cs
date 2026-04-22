using UnityEngine;

[CreateAssetMenu(fileName = "Pickup Interaction SO", menuName = "ScriptableObject/Interactable/pickup")]
public class PickupModuleConfigSO : InteractionModuleConfigSO
{
    public float followSpeed = 20;
    public float linearDamping = 10;
    public bool disablePickupOnPlacement;

    /// <summary>
    /// Pickup module will contain the logic of interaction, tickable and trigger
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public override InteractionModuleResult Create(GameObject owner)
    {
        PickupInteraction module = new PickupInteraction(owner, this);
        return new InteractionModuleResult
        {
            interaction = module,
            tickable = module,
            trigger = module,
        };
    }
}
