using TMPro;
using UnityEngine;

public class HighlightHandler
{
    private readonly Material[] _materials;
	private readonly float _highlightScale;

    public HighlightHandler(GameObject gameObject)
    {
        _materials = gameObject.GetComponent<MeshRenderer>().materials;

        if (_materials.Length > 1) _highlightScale = _materials[1].GetFloat("_OutlineScale"); // Material must have more than 1 to work

		SetHighlight(false);
    }


    /// <summary>
	/// Indicate if you are hovering over an interactable 
	/// </summary>
	/// <param name="state">boolean to check state. True will activate and false to deactivate highlight</param>
    public void SetHighlight(bool state)
    {
        if (_materials.Length > 1) _materials[1].SetFloat("_OutlineScale", state ? _highlightScale : 0f);
    }
}

