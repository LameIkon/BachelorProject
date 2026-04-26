using UnityEngine;

[CreateAssetMenu(fileName = "Interaction Menu Module SO", menuName = "ScriptableObject/Interactable/Config/Interaction Menu Config")]
public class InteractionMenuModuleConfigSO : ScriptableObject
{
    public UIToggleEventSO uiToggleEvent;
    public CompendiumPageRequestEventSO compendiumPageEvent;
}
