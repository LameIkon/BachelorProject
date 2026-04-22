using UnityEngine;
[CreateAssetMenu(fileName = "Highlight Interaction SO", menuName = "ScriptableObject/Interactable/highlight")]
public class HighlightModuleConfigSO : ScriptableObject
{
    public int materialIndex = 1;
    public float outlineScale = 1.1f;
    public string outlineProperty = "_OutlineScale";
}
