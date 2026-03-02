using UnityEngine;
using UnityEngine.UI;

public class UIModule : MonoBehaviour, IUISystem
{
    [Header("Required")]
    [SerializeField] private GameObject _canvas;
    [SerializeField] private UIModuleConfigSO _config;

    [Header("Optional")]
    [SerializeField] private GameObject _pageContainer;

    private CanvasUIModule _canvasModule;
    private PageUIModule _pageModule;
    
    public UIType UIType => _config.uiType;

    public bool IsOpen { get; private set;}

    private void Awake()
    {
        Initialize();
    }
    private void OnEnable()
    {
        _config.toggleUIEvent.OnRaise += ToggleUI;
    }

    private void OnDisable()
    {
       _config.toggleUIEvent.OnRaise -= ToggleUI; 
    }

    private void Start()
    {
        
        _config.registerUIEvent.Raise(this);
    }

    #region Initialize
    private void Initialize()
    {
        _canvasModule = new CanvasUIModule(_canvas);

        if (!_pageContainer) return;

        _pageModule = new PageUIModule(_pageContainer);
        SetupButtons();

    }

    private void SetupButtons()
    {
        foreach (Button button in _canvas.GetComponentsInChildren<Button>(true))
        {
            if (button.TryGetComponent(out PageButton pageButton))
            {
                int pageKey = pageButton.pageIndex;
                button.onClick.AddListener(() => _pageModule.SwitchPage(pageKey));
            }
        }
    }
    #endregion

    #region Methods
    public void ToggleUI()
    {
        IsOpen = !IsOpen;
        _canvasModule.Toggle();
        _pageModule?.SwitchPage(0);

       
        _config.updateUIEvent.Raise(this);
    }

    public void Close()
    {
        if (!IsOpen) return;

        IsOpen = false;
        _canvasModule.Toggle();
        _config.updateUIEvent.Raise(this);
    }
    #endregion
}
