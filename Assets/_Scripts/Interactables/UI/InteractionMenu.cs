using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The interaction menu will often be enabled and disabled. The script is used to show interactables in the game and give a small
/// menu box with options.
/// </summary>
public class InteractionMenu : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UIToggleEventSO _toggleUI;

    [Header("Required Components")]
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private Button _toCompendiumButton;

    private CompendiumManager _compendiumManager;
    private CompendiumPage _currentPage;

    /// <summary>
    /// Set title whenever we open the interactionMenu
    /// </summary>
    private void OnEnable()
    {
        _title.text = _compendiumManager != null ?  _currentPage?.title : "No data found";
    }

    private void OnDisable()
    {
        
    }

    private void GoToCompendiumPage()
    {
        _toggleUI.Raise(UIType.Compendium); // Open compendium
        _currentPage?.button?.onClick.Invoke(); // Open compendium to the right page
    }

    private void Awake()
    {
        Initialize();
    }

    #region Initialize

    private void Initialize()
    {
        _toCompendiumButton.onClick.AddListener(() => GoToCompendiumPage());
    }

    #endregion
}
