using UnityEngine;

[CreateAssetMenu(fileName = "Lever Identity SO", menuName = "ScriptableObject/Interactable/Identity/Lever")]
public class LeverInteractionIdentitySO : InteractionIdentitySO
{
    public ButtonEventSO buttonEvent;
    public ButtonData buttonData;
    public AnimationClip pullLeftAnimation;
    public AnimationClip pullRightAnimation;
}

