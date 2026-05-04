using UnityEngine;

[CreateAssetMenu(fileName = "Toggle Button Identity SO", menuName = "ScriptableObject/Interactable/Identity/Toggle Button")]
public class ToggleButtonInteractionIdentitySO : InteractionIdentitySO
{
    public ButtonEventSO buttonEvent;
    public ButtonData buttonData;
    public AnimationClip pressInanimation;
    public AnimationClip pressOutanimation;
    
}
