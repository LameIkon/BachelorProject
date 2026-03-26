using UnityEngine;

public class OnHighlightUtility
{
    private readonly Material[] _materials;
	private readonly float _highlightScale;

    public OnHighlightUtility(GameObject gameObject)
    {
        _materials = gameObject.GetComponent<MeshRenderer>().materials;

        _highlightScale = _materials[1].GetFloat("_OutlineScale");
		SetHighlight(false);
    }

    /// <summary>
	/// Indicate if you are hovering over an interactable 
	/// </summary>
	/// <param name="state">boolean to check state. True will activate and false to deactivate highlight</param>
    public void SetHighlight(bool state)
    {
        _materials[1].SetFloat("_OutlineScale", state ? _highlightScale : 0f);
    }
}


