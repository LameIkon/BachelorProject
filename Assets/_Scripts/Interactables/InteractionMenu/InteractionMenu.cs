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
    [SerializeField] private CompendiumPageProviderSO _pageProvider;
    [SerializeField] private CompendiumPageRequestEventSO _request;
    [SerializeField] private StoreDataEventSO _storeDataEvent;

    [Header("Required Components")]
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private Button _toCompendiumButton;

    private CompendiumPage _currentPage;

    private void Awake()
    {
        _toCompendiumButton.onClick.AddListener(() => GoToCompendiumPage());
    }

    private void OnEnable()
    {
        _request.OnRaise += SetPage;
    }


    private void OnDisable()
    {
        _request.OnRaise -= SetPage;
        //_currentPage = null;
    }

    public void SetPage(CompendiumID id)
    {
        _currentPage = _pageProvider?.GetPage(id);
        _title.text = _currentPage != null ?  _currentPage?.title : "No data found";
    }

    private void GoToCompendiumPage()
    {
        Debug.Log("Try go to compendium");
        if (_currentPage == null) return;
        _toggleUI.Raise(UIType.Compendium); // Open compendium
        _currentPage.button?.onClick?.Invoke(); // Open compendium to the right page

        InteractionEvent interactionEvent = new InteractionEvent()
        {
            eventType = EventType.Compendium,
            compendiumOutcome = CompendiumOpenMethod.InteractionMenu,
        };

        _storeDataEvent?.Raise(interactionEvent);
    }
}
