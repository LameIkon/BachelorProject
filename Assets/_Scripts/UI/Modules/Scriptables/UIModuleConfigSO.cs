using UnityEngine;

[CreateAssetMenu(fileName = "UI Module Config", menuName = "ScriptableObject/Events/UI/Module Config")]
public class UIModuleConfigSO : ScriptableObject
{
    private const string UI_EVENT_TOOLTIP = "Event is only required if we got a UIManager in the scene to handle opening and closing of UI";


    public UIType uiType;
    public UIRuleType uiRuleType;
    
    [Header("Events")]
    [Tooltip(UI_EVENT_TOOLTIP)]
    public UISystemEventSO updateUIEvent;
    [Tooltip(UI_EVENT_TOOLTIP)]
    public UISystemEventSO registerUIEvent;
}
