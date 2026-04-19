using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Keybind Register Action Event SO", menuName = "ScriptableObject/Events/Keybind Register")]
public class KeybindRegisterActionEventSO : ScriptableObject
{
    public event Action<KeybindingUI> OnRaise;

    public void Raise(KeybindingUI keybinding)  => OnRaise?.Invoke(keybinding);
}
