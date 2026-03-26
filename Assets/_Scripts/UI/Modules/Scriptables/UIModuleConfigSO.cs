using UnityEngine;

[CreateAssetMenu(fileName = "UI Module Config", menuName = "ScriptableObject/Events/UI/Module Config")]
public class UIModuleConfigSO : ScriptableObject
{
    public UIType uiType;
    public UIRuleType uiRuleType;
    
    [Header("Events")]
    public UISystemEventSO updateUIEvent;
    public UISystemEventSO registerUIEvent;
    //public UIToggleEventSO toggleUIEvent;
}
