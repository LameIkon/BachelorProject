using UnityEngine;

public class OnHighlightUtility
{
    private readonly Material[] _materials;
	private readonly float _highlightScale;

    private readonly UIToggleEventSO _uiToggleEvent;

    public bool isSelected;

    public OnHighlightUtility(GameObject gameObject, UIToggleEventSO uiToggleEvent)
    {
        _materials = gameObject.GetComponent<MeshRenderer>().materials;
        _uiToggleEvent = uiToggleEvent;

        _highlightScale = _materials[1].GetFloat("_OutlineScale");
		SetHighlight(false);

        InputReader.s_TogglePopUp += TogglePopUp;
    }

    /// <summary>
	/// Indicate if you are hovering over an interactable 
	/// </summary>
	/// <param name="state">boolean to check state. True will activate and false to deactivate highlight</param>
    public void SetHighlight(bool state)
    {
        _materials[1].SetFloat("_OutlineScale", state ? _highlightScale : 0f);
    }

    private void TogglePopUp()
    {
        Debug.Log($"{isSelected} and event is {_uiToggleEvent}");
        if (isSelected && _uiToggleEvent != null)
        {
            Debug.Log("Toggle");
            _uiToggleEvent.Raise(UIType.InteractionPopUp);
        }
    }
}


