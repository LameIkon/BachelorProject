using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    [SerializeField] private UIToggleEventSO _closeUI;

    private void Awake()
    {
        if (TryGetComponent(out Button button))
        {
            button.onClick.AddListener(() => CloseUI());
        }
    }

    private void CloseUI()
    {
        _closeUI.Raise();
    }

}
