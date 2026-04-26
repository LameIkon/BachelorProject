using UnityEngine;

[CreateAssetMenu(fileName = "Pickup Definition SO", menuName = "ScriptableObject/Interactable/Definition/Pickup")]
public class PickupInteractionDefinitionSO : InteractionDefinitionSO
{
    public PickableType type;
    public bool disablePickupOnPlacement;
    public float weight;
}
