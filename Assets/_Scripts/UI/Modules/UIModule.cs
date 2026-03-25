using UnityEngine;

/// <summary>
/// All related to ui should use this script. It's designed mostly for dynamic ui that changes over time but also
/// for static ui that never change, to enable or disable.
/// </summary>
public class UIModule : MonoBehaviour, IUISystem
{
    [Header("Required")]
    [SerializeField] private GameObject _canvas;
    [SerializeField] private UIModuleConfigSO _config;

    [Header("Optional")]
    [SerializeField] private PageSettings _pageSettings;

    private CanvasUIModule _canvasModule;
    public PageUIModule pageModule {get; private set;}
    
    public UIType UIType => _config.uiType; 
    public UIRuleType RuleType => _config.uiRuleType;

    public bool IsOpen { get; private set;}

    private void Start()
    {
        Initialize();
        pageModule?.SetupButtons();
        _config.registerUIEvent.Raise(this);
        pageModule?.SwitchPage(0);
    }

    #region Initialize
    private void Initialize()
    {
        _canvasModule = new CanvasUIModule(_canvas);

        InitializePageSettings();

    }

    private void InitializePageSettings()
    {
        if (!_pageSettings.pageContainer) return;

        switch (_pageSettings.pageMode)
        {
            case PageMode.None:
                Debug.LogWarning($"Current mode: {PageMode.None}, has been assigned to {_pageSettings.pageContainer.name}. It will therefore not initialize any page container");
                return;
            case PageMode.MultiplePages:
                // If we have a pageSetting we will create a method to switch pages
                pageModule = new PageUIModule(_pageSettings);                
                return;

            case PageMode.OverridePage: // Override is not an option and is still under consideration for implementation   
                //_overridePageModule = new PageUIOverrideModule(_pageSettings.pageContainer);
                return;
        }

        
    }

    #endregion

    #region Methods
    public void Open()
    {
        if (IsOpen) return;

        IsOpen = true;
        _canvasModule.SetActive(true);

        if (_pageSettings.rememberPage) pageModule?.SwitchPage(0); // Reset to first page if setting set to true

        _config.updateUIEvent.Raise(this);
    }

    public void Close()
    {
        if (!IsOpen) return;

        IsOpen = false;
        _canvasModule.SetActive(false);

        _config.updateUIEvent.Raise(this);
    }
    #endregion
}
