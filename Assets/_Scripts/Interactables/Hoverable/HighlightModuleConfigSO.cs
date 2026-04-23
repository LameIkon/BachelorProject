using UnityEngine;
[CreateAssetMenu(fileName = "Highlight Interaction SO", menuName = "ScriptableObject/Interactable/Highlight")]
public class HighlightModuleConfigSO : ScriptableObject
{
    public int materialIndex = 1;
    public float outlineScale = 1.1f;
    public string outlineProperty = "_OutlineScale";
}

[CreateAssetMenu(fileName = "Interaction Menu Module SO", menuName = "ScriptableObject/Interactable/Interaction Menu")]
public class InteractionMenuModuleConfigSO : ScriptableObject
{
    //public
}
