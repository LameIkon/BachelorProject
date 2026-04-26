using TMPro;
using UnityEngine;

public class PromptUIElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _icon;
    [SerializeField] private TextMeshProUGUI _description;

    public void SetIcon(string icon)
    {
        if (_icon == null)
        {
            Debug.LogWarning("No icon container!");
            return;
        } 
        _icon.text = icon;
    }

    public void SetDescription(string description)
    {
        if (_description == null)
        {
            Debug.LogWarning("No description container!");
            return;
        } 
        _description.text = description;
    }
}
