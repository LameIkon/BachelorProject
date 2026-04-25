using TMPro;
using UnityEngine;

public class ActionGuideUI : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private InputPromptProvideEventSO _uiHoverDataEvent;

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _icon;
    [SerializeField] private TextMeshProUGUI _description;

    private void OnEnable()
    {
        _uiHoverDataEvent.OnRaise += UpdateDisplay;
    }

    private void OnDisable()
    {
        _uiHoverDataEvent.OnRaise -= UpdateDisplay;
    }

    private void UpdateDisplay(InteractionData data)
    {
        if (_icon == null || _description == null)
        {
            Debug.LogError("ActionGuideUI: string references not assigned.");
            return;
        }

        _icon.text = data.icon != string.Empty ? data.icon : "X!";
        _description.text = data.description != string.Empty ? data.description : "No description!";
    }
}
