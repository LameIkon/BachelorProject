using UnityEngine;
using UnityEngine.UI;

public class InspectStartTerminalCloseButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Awake()
    {
        _button.onClick.AddListener(() =>  InspectStartTerminal.Raise(false)); 
    }

}
