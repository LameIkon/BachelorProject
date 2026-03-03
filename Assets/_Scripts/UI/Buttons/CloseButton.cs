using UnityEngine;

public class CloseButton : MonoBehaviour
{
    [SerializeField] private UIToggleEventSO _closeUI;

    public void CloseUI()
    {
        _closeUI.Raise();
    }
}
