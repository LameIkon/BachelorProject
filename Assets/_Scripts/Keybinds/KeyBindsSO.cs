using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "DefaultKeyRebinds", menuName = "ScriptableObject/Input/Default Key Rebinds")]

public class KeyBindsSO : ScriptableObject
{
    [Serializable]
    public class DefaultBinding
    {
        public InputActionAsset actionAsset;

        [Tooltip("Format: ActionMap/ActionName (e.g. Player/Inventory)")]
        public string actionPath;

        public int bindingIndex;

        [Tooltip("Key (e.g. w, space, escape)")]
        public string key;
    }

    public List<DefaultBinding> defaultBindings;
}
