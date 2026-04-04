using UnityEngine;
using UnityEngine.UI;

public class ToggleUIButton : MonoBehaviour
{
    [SerializeField] private UIToggleEventSO _toggleEvent;

    [Header("Optional")]
    [SerializeField, Tooltip("The UI system to open, if no UI assigned it will try seach for UIMOdule in its parent for UI system")] 
    private UIModule _uiModule;

    private void Awake()
    {

        if (_uiModule == null)
        {
            _uiModule = GetComponentInParent<UIModule>();
        }

        if (_uiModule == null) return;

        if (TryGetComponent(out Button button))
        {
            button.onClick.AddListener(() => _toggleEvent.Raise(_uiModule.UIType));
        }
    }
}
