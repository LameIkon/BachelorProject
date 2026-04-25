using UnityEngine;

/// <summary>
/// This script handle rendering control
/// </summary>
public class HighlightHandler
{
    private readonly Material _targetMaterial;
    private readonly string _property; // The name of the outline
    private readonly float _highlightScaleValue;

    public HighlightHandler(GameObject owner, HighlightModuleConfigSO config)
    {
        Renderer renderer = owner.GetComponent<Renderer>();

        if (renderer == null)
        {
            Debug.LogWarning($"{owner.name} has no Renderer for highlight.");
            return;
        }

        Material[] materials = renderer.materials;

        if (materials.Length <= config.materialIndex)
        {
            Debug.LogWarning($"{owner.name} missing material index {config.materialIndex}");
            return;
        }

        _targetMaterial = materials[config.materialIndex];
        _property = config.outlineProperty;

        _highlightScaleValue = config.outlineScale;

		SetHighlight(false);
    }


    /// <summary>
	/// Indicate if you are hovering over an interactable 
	/// </summary>
	/// <param name="state">boolean to check state. True will activate and false to deactivate highlight</param>
    public void SetHighlight(bool state)
    {
        if (_targetMaterial == null) return;

        _targetMaterial.SetFloat(_property, state ? _highlightScaleValue : 0f);
    }
}

