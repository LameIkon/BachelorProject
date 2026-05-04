using UnityEngine;

[CreateAssetMenu(fileName = "Button Identity SO", menuName = "ScriptableObject/Interactable/Identity/Button")]
public class ButtonInteractionIdentitySO : InteractionIdentitySO
{
    public ButtonEventSO buttonEvent;
    public ButtonData buttonData;
    public AnimationClip animation;
    
}
