using UnityEngine;
[CreateAssetMenu(fileName = "Highlight Interaction Module SO", menuName = "ScriptableObject/Interactable/Highlight")]
public class HighlightModuleConfigSO : ScriptableObject
{
    public int materialIndex = 1;
    public float outlineScale = 1.1f;
    public string outlineProperty = "_OutlineScale";
}
