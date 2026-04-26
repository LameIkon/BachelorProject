using UnityEngine;

[CreateAssetMenu(fileName = "Pickup Identity SO", menuName = "ScriptableObject/Interactable/Identity/Pickup")]
public class PickupInteractionIdentitySO : InteractionIdentitySO
{
    public PickableType type;
    public bool disablePickupOnPlacement;
    public float weight;
}
