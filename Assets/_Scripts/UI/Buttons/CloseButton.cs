using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    [SerializeField] private UIToggleEventSO _toggleEvent;
    private UIModule _uiModule;

    private void Awake()
    {
        _uiModule = GetComponentInParent<UIModule>();

        if (_uiModule == null) return;

        if (TryGetComponent(out Button button))
        {
            //button.onClick.AddListener(() => _uiModule.Close());
            button.onClick.AddListener(() => _toggleEvent.Raise(_uiModule.UIType));
        }
    }
}
