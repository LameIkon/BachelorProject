using UnityEngine;

[CreateAssetMenu(fileName = "Interaction Menu Module SO", menuName = "ScriptableObject/Interactable/Interaction Menu")]
public class InteractionMenuModuleConfigSO : ScriptableObject
{
    public UIToggleEventSO uiToggleEvent;
    public CompendiumPageRequestEventSO compendiumPageEvent;
}
