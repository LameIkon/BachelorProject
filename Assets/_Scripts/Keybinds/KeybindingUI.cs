using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeybindingUI : MonoBehaviour
{
    //[SerializeField] private KeybindRegisterActionEventSO _keybindRegisterEvent;

    [SerializeField] private InputActionReference _actionRef;
    public TextMeshProUGUI textField {get;  private set;}
    [SerializeField] private int bindingIndex;

    //public InputAction action {get; private set;}

    private void Awake()
    {
        Initialize();
    }

    //private void Start()
    //{
    //    _keybindRegisterEvent.Raise(this);
    //}


    //public void UpdateUI() // Called from outside
    //{
    //    textField.text = action.GetBindingDisplayString(bindingIndex).ToLower();
    //}

    //public void StartRebinding() // Called from button
    //{
    //    //EventManager.RaiseOnUpdateKeybind(this);
    //}

    #region Initialize
    private void Initialize()
    {
        textField = GetComponentInChildren<TextMeshProUGUI>();

        textField.text = _actionRef.action.GetBindingDisplayString(bindingIndex).ToLower();
    }
    #endregion
}