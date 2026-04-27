using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RangedFloat))]
public class RangedFloatPropertyDrawer : PropertyDrawer
{
    private const int _AMOUNT_OF_ITEMS = 1;
    private readonly float _spacerHeight = 20f;
    private readonly float _lineHeight = 16f;
    private string _name = string.Empty;
    private string _tooltip = string.Empty;
    private bool _cache = false;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return _AMOUNT_OF_ITEMS * _spacerHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUIUtility.labelWidth /= 4f;
        position.height = _lineHeight;
        position.width /= 4f;

        if (!_cache)
        {
            _name = property.displayName;
            _tooltip = property.tooltip;

            _cache = true;
        }

        EditorGUI.PrefixLabel(position, new GUIContent(_name,
            string.Format("Base Tooltip: {0}", _tooltip.Equals(string.Empty) ? "" : string.Format("\n\n{0}'s Tooltip:\n{1}", _name, _tooltip))));

        position.x += position.width;

        position.width *= 4f;
        position.width *= 0.375f;

        EditorGUI.PropertyField(position, property.FindPropertyRelative("_minValue"),
            new GUIContent("Min"));

        position.x += position.width;

        EditorGUI.PropertyField(position, property.FindPropertyRelative("_maxValue"),
            new GUIContent("Max"));

    }
}