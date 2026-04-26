using UnityEngine;

[CreateAssetMenu(fileName = "Input Prompt Module SO", menuName = "ScriptableObject/Interactable/Config/Input Prompt Config")]
public class InputPromptModuleConfigSO : ScriptableObject
{
    public UIToggleEventSO uiToggleEvent;
    public InputPromptProvideEventSO inputPromptProvideEvent;
}
