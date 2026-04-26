using UnityEngine;

[CreateAssetMenu(fileName = "Button definition SO", menuName = "ScriptableObject/Interactable/Definition/Button")]
public class ButtonInteractionDefinitionSO : InteractionDefinitionSO
{
    public ButtonEventSO buttonEvent;
    public ButtonData buttonData;
}
