using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyBindingUtility
{
    //private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;
    
    //private const string displayWaitingforAction = "Select a key";

    ////private List<KeybindingUI> _keybinds; // List of rebinding items
    //private KeyBindsSO _defaultKeybinds;
    //private KeyBindsSO _currentKeybinds;

    //private Dictionary<GameObject, KeyBindsSO> _keybinds;


    //public KeyBindingUtility(KeyBindsSO defaultKeyRebinds, KeyBindsSO currentKeyRebinds)
    //{
    //    _defaultKeybinds = defaultKeyRebinds;
    //    _currentKeybinds = currentKeyRebinds;

    //    _keybinds = new Dictionary<GameObject, KeyBindsSO>();
    //}

    //public void StartRebinding(int index) // Called from button
    //{
    //    KeybindingUI currentRebindingKey = _keybinds[index];
    //    EventManager.RaiseSwitchActionMap(ActionMap.None);

    //    TextMeshPro text = currentRebindingKey.keyToRebind.GetComponentInChildren<TextMeshPro>();
    //    text.text = displayWaitingforAction;
    //    //currentRebindingKey.startRebindingButton.SetActive(false);
    //    //currentRebindingKey.waitingForInput.SetActive(true);

    //    //_rebindingOperation = currentRebindingKey.actionReference.action.PerformInteractiveRebinding(currentRebindingKey.actionReference)
    //    //    .WithControlsExcluding("<Keyboard>/escape") // Exclude
    //    //    .WithControlsExcluding("<keyboard>/anyKey") // Exclude
    //    //    .OnMatchWaitForAnother(0.1f)
    //    //    .OnComplete(operation => RebindComplete(currentRebindingKey))
    //    //    .Start();
    //}

    //private void RebindComplete(KeybindingUI rebindingKey)
    //{
    //    EventManager.RaiseOnUpdateKeybind(rebindingKey);

    //    _rebindingOperation.Dispose();

    //    //rebindingKey.keyToRebind.SetActive(true);
    //    //rebindingKey.waitingForInput.SetActive(false);

    //    EventManager.RaiseSwitchActionMap(ActionMap.Player);

    //    SaveRebinds();
    //}

    //public void SaveRebinds() 
    //{
    //    foreach (KeybindingUI key in _keybinds)
    //    {
    //        EventManager.RaiseOnUpdateKeybind(key);
    //    }
    //    //string rebinds = _playerController.PlayerInput.actions.SaveBindingOverridesAsJson();
    //    //PlayerPrefs.SetString(_rebindsKey, rebinds);
    //    //PlayerPrefs.Save();
    //    //saveSystem.SaveData();

    //    Debug.Log("Saved rebinds");
    //}

    //private void LoadRebinds()
    //{
    //    //ApplyDefaultKeys();
    //    //string rebinds = PlayerPrefs.GetString(_rebindsKey, string.Empty);

    //    //if (string.IsNullOrEmpty(rebinds)) // If no stored rebinds
    //    //{
    //    //    foreach (var rebindingItem in _rebindingItems)
    //    //    {
    //    //        UpdateBindingText(rebindingItem); // Display default settings
    //    //    }
    //    //    //return; // Return if no rebinds found
    //    //}

    //    // Else
    //    //_playerController.PlayerInput.actions.LoadBindingOverridesFromJson(rebinds); // Load stored rebinds from JSON
    //    //foreach (var rebindingItem in _rebindingItems)
    //    //{
    //    //    UpdateBindingText(rebindingItem); // Display rebinds
    //    //}
    //}

    //#region Default Keys
    //public void ApplyDefaultKeys()
    //{
    //    foreach (var binding in _defaultKeybinds.defaultBindings)
    //    {
    //        var action = binding.actionAsset.FindAction(binding.actionPath);
    //        action.ApplyBindingOverride(binding.bindingIndex, $"<Keyboard>/{binding.key.ToLower()}");
    //    }

    //    //foreach (var rebindingItem in _rebindingItems) // Afterwards update UI
    //    //{
    //    //    EventManager.RaiseOnUpdateKeybind(rebindingItem);
    //    //    EventManager.RaiseOnSaveSettings();
    //    //}
    //}
    //#endregion
}