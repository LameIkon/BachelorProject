using UnityEngine;

[CreateAssetMenu(fileName = "Interaction Identity SO", menuName = "ScriptableObject/Interactable/Identity data")]
public class InteractionIdentitySO : ScriptableObject
{
    public PickableType type;
    public Sprite icon;
    public string displayName;
}
