using UnityEngine;

[RequireComponent(typeof(AudioSource))]
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
    [SerializeField] private bool _setCanvasActiveAtStart;
    [SerializeField] private PageSettings _pageSettings;
    [SerializeField] private AudioPlayerSO _audioPlayer;

    private CanvasUIModule _canvasModule;
    public PageUIModule pageModule {get; private set;}
    
    public UIType UIType => _config.uiType; 
    public UIRuleType RuleType => _config.uiRuleType;

    private AudioSource _audioSource;

    public bool IsOpen { get; private set;}

    private void Awake()
    {
        Initialize();
    }


    private void Start()
    {
        _config.registerUIEvent.Raise(this);    
    }

    #region Initialize
    private void Initialize()
    {     
        _canvasModule = new CanvasUIModule(_canvas, _setCanvasActiveAtStart);
        IsOpen = _setCanvasActiveAtStart;
        InitializePageSettings();
        _audioSource = GetComponent<AudioSource>();
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
                pageModule = new PageUIModule(_pageSettings, this.gameObject);     
                pageModule?.SwitchPage(0);
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

        if (_audioPlayer != null) 
        {
            _audioPlayer.PlaySound(_audioSource);
        }
    }

    public void Close()
    {
        if (!IsOpen) return;

        IsOpen = false;
        _canvasModule.SetActive(false);

        _config.updateUIEvent.Raise(this);

        if (_audioPlayer != null)
        {
            _audioPlayer.PlaySound(_audioSource);
        }
    }
    #endregion
}
