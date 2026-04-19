using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeybindsManager : Singleton<KeybindsManager>
{
    [Header("Events")]
    [SerializeField] private KeybindRegisterActionEventSO _keybindRegisterEvent;
    [SerializeField] private RegisterSaveDataEventSO _saveDataEvent;
    [SerializeField] private LoadDataEventSO _loadDataEvent;

    [Header("References")]
    [SerializeField] private InputActionAsset _actionAsset;
    
    private List<KeybindingUI> _keybindEntries = new ();
    private const string _displayWaitingforAction = "Select a key";

    private void OnEnable()
    {
        _keybindRegisterEvent.OnRaise += RegisterKeybinds;
        //EventManager.TryUpdateKeybind += StartRebinding;
    }

    private void OnDisable()
    {
        _keybindRegisterEvent.OnRaise -= RegisterKeybinds;
        //EventManager.TryUpdateKeybind -= StartRebinding;
    }

    private void Start()
    {
        LoadRebinds();
    }

    //public void StartRebinding(KeybindingUI ui) // Called from button
    //{
    //    ui.textField.text = _displayWaitingforAction;
    //    _actionAsset.Disable();

    //    InputAction action = ui.action;

    //    action.PerformInteractiveRebinding(ui.bindingIndex)
    //        .WithControlsExcluding("<Keyboard>/escape") // Exclude
    //        .WithControlsExcluding("<keyboard>/anyKey") // Exclude
    //        .OnMatchWaitForAnother(0.1f)
    //        .OnComplete(_ =>
    //        {
    //            action.Dispose();
    //            _actionAsset.Enable();
    //            ui.UpdateUI();
    //            SaveRebinds();

    //        })
    //        .Start();
    //}

    #region Save & Load

    public void SaveRebinds() 
    {
        string json = _actionAsset.SaveBindingOverridesAsJson();

        //_saveDataEvent.Save(new GameSettingsData()
        //{
        //    inputRebindsJson = json
        //});

        Debug.Log("Saved rebinds");
    }

    public void LoadRebinds()
    {
        _actionAsset.Disable();
        _actionAsset.RemoveAllBindingOverrides();

        //ControlsSettingsData data = _loadDataEvent.Load<ControlsSettingsData>(ControlsSettingsData.FileName, ControlsSettingsData.FolderPath);
        //if (data != null)
        //{
        //    _actionAsset.LoadBindingOverridesFromJson(data.inputRebindsJson);
        //}

        _actionAsset.Enable();
        UpdateAllUI();
    }
    #endregion

    #region UI
    private void RegisterKeybinds(KeybindingUI keybind)
    {
        _keybindEntries.Add(keybind);

        //keybind.UpdateUI(); // Ensure it gets updated
    }

    private void UpdateAllUI()
    {
        foreach (KeybindingUI ui in _keybindEntries)
        {
            //ui.UpdateUI();
        }
    }
    #endregion

    #region Default Keys
    public void ApplyDefaultKeys() // Called from button
    {
        _actionAsset.RemoveAllBindingOverrides();
        UpdateAllUI();
        SaveRebinds();
    }

    //public void ResetKeyBinding(KeybindingUI ui) // called from button
    //{
    //    InputAction action = ui.action;
    //    action.RemoveBindingOverride(ui.bindingIndex);

    //    ui.UpdateUI();
    //    SaveRebinds();
    //}
    #endregion
}



public class ControlsSettingsData : ISavableData
{
    // Input rebinding data
    public string inputRebindsJson;

    private readonly string FileName = "Controls";

    public string SaveFileName => FileName;

    public List<string> SavePath  => new List<string>()
    {
        "Settings",
        FileName
    };
}